#!/bin/bash

#----------------------------------------------------------------
# Run automated tests on iOS for Didomi Plugin.
#----------------------------------------------------------------

UNITY_PATH="/Applications/Unity/Hub/Editor/2021.3.20f1/Unity.app/Contents/MacOS/Unity"
RESULTS_PATH="ios-test-results.xml"
LOG_PATH="iosTestsRun.log"

# Run the tests
$UNITY_PATH -batchmode -buildTarget iOS -runTests -testResults $RESULTS_PATH -testPlatform iOS -logFile $LOG_PATH

 if [ $? -eq 0 ]
then
    echo "iOS tests passed!"
else
    echo "iOS tests failed!"
    exit 1
fi
