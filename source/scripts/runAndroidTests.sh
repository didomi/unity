#!/bin/bash

#--------------------------------------------------------------------
# Run automated tests on Android for Didomi Plugin.
# Note: Older Unity versions do not correctly run tests on Android.
# Tested successfully with Unity LTS 2021.3.20f1.
#--------------------------------------------------------------------

# Retrieve scripts directory
scriptsDir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

UNITY_PATH=$(awk -F= '/^editor/ {print $2}' $scriptsDir/unity.properties)
RESULTS_PATH="artifacts/android-test-results.xml"
LOG_PATH="artifacts/androidTestsRun.log"

# Check for any Android emulator / device
connected_devices=$(adb devices | grep -v "List" | grep "device$")
if [ -z "$connected_devices" ]; then
    echo "No Android device connected."
    
    # Search for device name
    DEVICE=$(emulator -list-avds | head -n 1)

    # Start the emulator in the background
    echo "Launching $DEVICE, please wait a few seconds..."
    emulator -avd "$DEVICE" -no-window &

    # Wait for the emulator to be detected
    adb wait-for-device

    echo "Device detected. Waiting for device to fully boot..."

    # Wait until the boot animation completes
    until adb shell getprop sys.boot_completed | grep -m 1 '1'; do
        echo "Waiting for $DEVICE to complete booting..."
        sleep 1
    done

    echo "$DEVICE is ready."
else
    echo "Android device(s) connected, continue with tests."
fi

# Run the tests
$UNITY_PATH -batchmode -buildTarget Android -runTests -testResults $RESULTS_PATH -testPlatform Android -logFile $LOG_PATH

if [ -z "$connected_devices" ]; then
    # Kill the emulator from the background
    adb -s emulator-5554 emu kill
    # Wait for the emulator to close
    while adb devices | grep -q "emulator"; do
        echo "Waiting for $DEVICE to close..."
        sleep 1
    done
fi

result=$?
if [ $result -eq 0 ]
then
    echo "Android tests passed!"
    exit 0
else
    echo "Android tests failed with exit code $result"
    exit 1
fi
