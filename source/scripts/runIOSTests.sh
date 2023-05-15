#!/bin/bash

#----------------------------------------------------------------
# Run automated tests on iOS for Didomi Plugin.
#----------------------------------------------------------------

# Retrieve scripts directory
scriptsDir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

UNITY_PATH=$(awk -F= '/^editor/ {print $2}' $scriptsDir/unity.properties)
RESULTS_PATH="artifacts/ios-test-results.xml"
LOG_PATH="artifacts/iosTestsRun.log"

# Run the tests
$UNITY_PATH -batchmode -buildTarget iOS -runTests -testResults $RESULTS_PATH -testPlatform iOS -logFile $LOG_PATH

 if [ $? -eq 0 ]
then
    echo "iOS tests passed!"
else
    echo "iOS tests failed!"
    exit 1
fi
