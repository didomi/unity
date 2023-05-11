#!/bin/bash

#--------------------------------------------------------------------
# Run automated tests on Android for Didomi Plugin.
# Note: Older Unity versions do not correctly run tests on Android
#--------------------------------------------------------------------

UNITY_PATH="/Applications/Unity/Hub/Editor/2021.3.20f1/Unity.app/Contents/MacOS/Unity"
RESULTS_PATH="android-test-results.xml"
LOG_PATH="androidTestsRun.log"

# Run the tests
$UNITY_PATH -batchmode -buildTarget Android -runTests -testResults $RESULTS_PATH -testPlatform Android -logFile $LOG_PATH

if [ $? -eq 0 ]
then
    echo "Android tests passed!"
else
    echo "Android tests failed!"
    exit 1
fi
