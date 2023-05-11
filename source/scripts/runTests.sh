#!/bin/bash

#----------------------------------------------------------------
# Run automated tests on the project
# Note: Currently Unity does not correctly run tests on Android,
# so we only run tests on iOS pplatform. 
#----------------------------------------------------------------

UNITY_PATH="/Applications/Unity/Hub/Editor/2020.3.46f1/Unity.app/Contents/MacOS/Unity"
RESULTS_PATH="test-results.xml"
LOG_PATH="testsRun.log"

# Run the tests for Didomi Plugin on iOS
$UNITY_PATH -batchmode -buildTarget iOS -runTests -testResults $RESULTS_PATH -testPlatform iOS -logFile $LOG_PATH

if [ $? -eq 0 ]
then
    echo "Tests passed!"
else
    echo "Tests failed!"
fi
