#!/bin/bash

#--------------------------------------------------------------------
# Run automated tests on Android for Didomi Plugin.
# Note: Older Unity versions do not correctly run tests on Android.
# Tested successfully with Unity LTS 2021.3.20f1.
#
# Optional environment variables:
#   ANDROID_AVD            AVD to launch when no device is connected
#                          (default: first entry of `emulator -list-avds`).
#   ANDROID_TESTS_TIMEOUT  Max duration in seconds of one Unity test run
#                          before it is considered hung (default: 1200).
#   ANDROID_TESTS_ATTEMPTS Number of attempts when a run hangs (default: 2).
#--------------------------------------------------------------------

# Retrieve scripts directory
scriptsDir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
projectDir="$( cd "$scriptsDir/.." && pwd )"

UNITY_PATH=$(awk -F= '/^editor/ {print $2}' $scriptsDir/unity.properties)
RESULTS_PATH="artifacts/android-test-results.xml"
LOG_PATH="artifacts/androidTestsRun.log"
TEST_APP_ID="com.UnityTestRunner.UnityTestRunner"
TIMEOUT=${ANDROID_TESTS_TIMEOUT:-1200}
ATTEMPTS=${ANDROID_TESTS_ATTEMPTS:-2}
PROJECT_SETTINGS="$projectDir/ProjectSettings/ProjectSettings.asset"
PROJECT_SETTINGS_BACKUP="$projectDir/artifacts/ProjectSettings.asset.bak"

# Use a dedicated Gradle home so the build ignores the user's global
# ~/.gradle/gradle.properties. Settings there (org.gradle.java.home pointing at a
# JDK newer than Unity's Gradle supports, configuration-cache, configureondemand)
# break the Android player build. Keeps local runs consistent with CI.
export GRADLE_USER_HOME="$scriptsDir/../artifacts/.gradle"

cd "$projectDir"
mkdir -p artifacts

. "$scriptsDir/ensureUnityClosed.sh"
ensure_unity_closed "$projectDir" || exit 1

EMULATOR_SERIAL=""
EMULATOR_PID=""

# Background processes (Unity, emulator) do not receive Ctrl-C:
# stop them and restore the project when the script is interrupted.
on_interrupt() {
    trap - INT TERM
    echo "Interrupted, cleaning up..."
    stop_unity 2>/dev/null
    rm -f "$PROJECT_SETTINGS_BACKUP"
    if [ -n "$EMULATOR_SERIAL" ]; then
        adb -s "$EMULATOR_SERIAL" emu kill >/dev/null 2>&1
    elif [ -n "$EMULATOR_PID" ]; then
        kill $EMULATOR_PID 2>/dev/null
    fi
    exit 130
}
trap on_interrupt INT TERM

# Check for any Android emulator / device
connected_devices=$(adb devices | grep -v "List" | grep "device$")
if [ -z "$connected_devices" ]; then
    echo "No Android device connected."

    DEVICE=${ANDROID_AVD:-$(emulator -list-avds | head -n 1)}
    if [ -z "$DEVICE" ]; then
        echo "No AVD available, create one first."
        exit 1
    fi

    # Start the emulator in the background.
    # Software rendering is used because host GPU rendering is unreliable
    # without a window and can crash the emulator.
    echo "Launching $DEVICE, please wait a few seconds..."
    emulator -avd "$DEVICE" -no-window -gpu swiftshader_indirect -no-snapshot-save &
    EMULATOR_PID=$!

    # Wait for the emulator to be detected
    adb wait-for-device
    EMULATOR_SERIAL=$(adb devices | awk '/^emulator-[0-9]+[[:space:]]+device$/ {print $1; exit}')
    export ANDROID_SERIAL="$EMULATOR_SERIAL"

    echo "Device $EMULATOR_SERIAL detected. Waiting for device to fully boot..."

    # Wait until the boot animation completes
    until adb shell getprop sys.boot_completed 2>/dev/null | grep -q '1'; do
        echo "Waiting for $DEVICE to complete booting..."
        sleep 1
    done

    # sys.boot_completed is set before shared storage and the package manager
    # are usable. Launching the player too early makes il2cpp resource
    # extraction fail ("Not enough storage space to install required resources").
    until adb shell 'touch /sdcard/Android/.ready_check && rm /sdcard/Android/.ready_check' >/dev/null 2>&1 \
        && adb shell pm path android >/dev/null 2>&1; do
        echo "Waiting for $DEVICE storage to be ready..."
        sleep 1
    done
    sleep 5

    echo "$DEVICE is ready."
else
    echo "Android device(s) connected, continue with tests."
fi

# Remove leftovers from previous runs (stale test app, port tunnels)
reset_device_state() {
    adb shell am force-stop "$TEST_APP_ID" >/dev/null 2>&1
    adb uninstall "$TEST_APP_ID" >/dev/null 2>&1
    adb reverse --remove-all >/dev/null 2>&1
    adb forward --remove-all >/dev/null 2>&1
}

UNITY_PID=""

# Kill the running Unity test run, if any, and restore the project settings
# the test framework could not reset (e.g. application identifier).
stop_unity() {
    [ -z "$UNITY_PID" ] && return
    kill $UNITY_PID 2>/dev/null
    local waited=0
    while kill -0 $UNITY_PID 2>/dev/null && [ $waited -lt 10 ]; do
        sleep 1
        waited=$((waited + 1))
    done
    kill -9 $UNITY_PID 2>/dev/null
    wait $UNITY_PID 2>/dev/null
    UNITY_PID=""
    [ -f "$PROJECT_SETTINGS_BACKUP" ] && cp "$PROJECT_SETTINGS_BACKUP" "$PROJECT_SETTINGS"
}

# Run Unity tests once. Returns Unity's exit code, or 124 if the run timed out.
run_unity_tests() {
    rm -f "$projectDir/$RESULTS_PATH"
    "$UNITY_PATH" -batchmode -projectPath "$projectDir" -buildTarget Android -runTests \
        -testResults "$RESULTS_PATH" -testPlatform Android -logFile "$LOG_PATH" &
    UNITY_PID=$!

    local elapsed=0
    while kill -0 $UNITY_PID 2>/dev/null; do
        if [ $elapsed -ge $TIMEOUT ]; then
            echo "Unity test run exceeded ${TIMEOUT}s, killing it (player/editor connection likely lost)."
            stop_unity
            return 124
        fi
        sleep 5
        elapsed=$((elapsed + 5))
    done

    wait $UNITY_PID
    local status=$?
    UNITY_PID=""
    return $status
}

cp "$PROJECT_SETTINGS" "$PROJECT_SETTINGS_BACKUP"

attempt=1
while true; do
    echo "Running Android tests (attempt $attempt/$ATTEMPTS)..."
    reset_device_state
    ensure_unity_closed "$projectDir" || { result=1; break; }
    run_unity_tests
    result=$?
    if [ $result -ne 124 ] || [ $attempt -ge $ATTEMPTS ]; then
        break
    fi
    attempt=$((attempt + 1))
done

reset_device_state
rm -f "$PROJECT_SETTINGS_BACKUP"

if [ -n "$EMULATOR_SERIAL" ]; then
    # Kill the emulator started by this script
    adb -s "$EMULATOR_SERIAL" emu kill
    # Wait for the emulator to close
    while adb devices | grep -q "^$EMULATOR_SERIAL"; do
        echo "Waiting for $DEVICE to close..."
        sleep 1
    done
fi

if [ $result -eq 0 ]
then
    echo "Android tests passed!"
    exit 0
elif [ $result -eq 124 ]
then
    echo "Android tests timed out after $ATTEMPTS attempt(s)"
    exit 1
else
    echo "Android tests failed with exit code $result"
    exit 1
fi
