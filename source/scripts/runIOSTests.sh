#!/bin/bash

#----------------------------------------------------------------
# Run automated tests on iOS for Didomi Plugin.
# Unity only builds the Xcode project (see IOSTestPlayerBuildModifier),
# then the project is built and run on a simulator with xcodebuild / simctl.
# Optional environment variables:
# - IOS_SIMULATOR_ID: UDID of the simulator to use (default: booted iPhone, or first available iPhone)
# - IOS_TESTS_TIMEOUT: maximum duration of the test run, in seconds (default: 900)
#----------------------------------------------------------------

# Retrieve scripts directory
scriptsDir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
projectDir="$( cd "$scriptsDir/.." && pwd )"

UNITY_PATH=$(awk -F= '/^editor/ {print $2}' $scriptsDir/unity.properties)
RESULTS_PATH="$projectDir/artifacts/ios-test-results.xml"
LOG_PATH="$projectDir/artifacts/iosTestsRun.log"
XCODE_LOG_PATH="$projectDir/artifacts/iosXcodeBuild.log"
PLAYER_LOG_PATH="$projectDir/artifacts/iosPlayer.out"
PLAYER_PATH="$projectDir/Builds/iOSTestPlayer"
DERIVED_DATA_PATH="$projectDir/Builds/iOSTestDerivedData"
RESULTS_FILE_NAME="ios-test-results.xml"
TIMEOUT=${IOS_TESTS_TIMEOUT:-900}

mkdir -p "$projectDir/artifacts"
rm -rf "$PLAYER_PATH" "$DERIVED_DATA_PATH" "$RESULTS_PATH" "$PLAYER_LOG_PATH"

# Generate the Xcode project with tests
echo "Generating Xcode project with tests..."
$UNITY_PATH -batchmode -projectPath "$projectDir" -buildTarget iOS -runTests -testPlatform iOS -iosTestPlayerPath "$PLAYER_PATH" -logFile "$LOG_PATH"
result=$?
if [ $result -ne 0 ] || [ ! -d "$PLAYER_PATH/Unity-iPhone.xcodeproj" ]
then
    echo "Xcode project generation failed with exit code $result, see $LOG_PATH"
    exit 1
fi

# Select the simulator
SIMULATOR_ID=${IOS_SIMULATOR_ID}
if [ -z "$SIMULATOR_ID" ]; then
    SIMULATOR_ID=$(xcrun simctl list devices available | grep "iPhone" | grep "(Booted)" | head -n 1 | grep -oE '[0-9A-F]{8}(-[0-9A-F]{4}){3}-[0-9A-F]{12}')
fi
if [ -z "$SIMULATOR_ID" ]; then
    SIMULATOR_ID=$(xcrun simctl list devices available | grep "iPhone" | head -n 1 | grep -oE '[0-9A-F]{8}(-[0-9A-F]{4}){3}-[0-9A-F]{12}')
fi
if [ -z "$SIMULATOR_ID" ]; then
    echo "No iPhone simulator available."
    exit 1
fi
echo "Using simulator $SIMULATOR_ID"
xcrun simctl boot "$SIMULATOR_ID" 2>/dev/null
xcrun simctl bootstatus "$SIMULATOR_ID" -b > /dev/null || exit 1

# Build the app
echo "Building app with Xcode..."
xcodebuild \
    -project "$PLAYER_PATH/Unity-iPhone.xcodeproj" \
    -scheme Unity-iPhone \
    -configuration Debug \
    -sdk iphonesimulator \
    -destination "id=$SIMULATOR_ID" \
    -derivedDataPath "$DERIVED_DATA_PATH" \
    CODE_SIGNING_ALLOWED=NO \
    build > "$XCODE_LOG_PATH" 2>&1
if [ $? -ne 0 ]
then
    echo "Xcode build failed, see $XCODE_LOG_PATH"
    exit 1
fi

APP_PATH=$(find "$DERIVED_DATA_PATH/Build/Products/Debug-iphonesimulator" -maxdepth 1 -name "*.app" | head -n 1)
BUNDLE_ID=$(/usr/libexec/PlistBuddy -c "Print :CFBundleIdentifier" "$APP_PATH/Info.plist")
if [ -z "$BUNDLE_ID" ]; then
    echo "Could not find the built app in $DERIVED_DATA_PATH"
    exit 1
fi

# Install and run the app, tests start automatically
echo "Running tests on simulator ($BUNDLE_ID)..."
xcrun simctl uninstall "$SIMULATOR_ID" "$BUNDLE_ID" 2>/dev/null
xcrun simctl install "$SIMULATOR_ID" "$APP_PATH" || exit 1
xcrun simctl launch --stdout="$PLAYER_LOG_PATH" --stderr="$PLAYER_LOG_PATH" "$SIMULATOR_ID" "$BUNDLE_ID" > /dev/null || exit 1

# Wait for the results file written by IOSTestResultsWriter
DATA_CONTAINER=$(xcrun simctl get_app_container "$SIMULATOR_ID" "$BUNDLE_ID" data)
DEVICE_RESULTS_PATH="$DATA_CONTAINER/Documents/$RESULTS_FILE_NAME"
elapsed=0
until [ -f "$DEVICE_RESULTS_PATH" ]; do
    if [ $elapsed -ge $TIMEOUT ]; then
        echo "iOS tests timed out after $TIMEOUT seconds, see $PLAYER_LOG_PATH"
        xcrun simctl terminate "$SIMULATOR_ID" "$BUNDLE_ID" 2>/dev/null
        exit 1
    fi
    sleep 5
    elapsed=$((elapsed + 5))
done
xcrun simctl terminate "$SIMULATOR_ID" "$BUNDLE_ID" 2>/dev/null
cp "$DEVICE_RESULTS_PATH" "$RESULTS_PATH"

# Check the results
summary=$(grep -m 1 "<test-run" "$RESULTS_PATH")
total=$(echo "$summary" | sed -E 's/.* total="([0-9]+)".*/\1/')
passed=$(echo "$summary" | sed -E 's/.* passed="([0-9]+)".*/\1/')
failed=$(echo "$summary" | sed -E 's/.* failed="([0-9]+)".*/\1/')
echo "iOS tests: $total total, $passed passed, $failed failed (results in $RESULTS_PATH)"

if [ "$failed" = "0" ] && [ "$total" != "0" ]
then
    echo "iOS tests passed!"
else
    echo "iOS tests failed"
    exit 1
fi
