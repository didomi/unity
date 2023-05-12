#!/bin/bash

#----------------------------------------------------------
# Export unitypackage files for the release
#----------------------------------------------------------

# Retrieve scripts directory
scriptsDir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

UNITY_PATH=$(awk -F= '/^editor/ {print $2}' $scriptsDir/unity.properties)

$UNITY_PATH -batchmode -quit -projectPath "." -executeMethod DidomiPackageExporter.ExportPackages

if [ $? -eq 0 ]
then
    echo "Export successful"
else
    echo "Error while exporting"
fi
