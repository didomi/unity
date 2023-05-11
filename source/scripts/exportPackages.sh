#!/bin/bash

#----------------------------------------------------------
# Export unitypackage files for the release
#----------------------------------------------------------

UNITY_PATH="/Applications/Unity/Hub/Editor/2020.3.46f1/Unity.app/Contents/MacOS/Unity"
EXPORT_PATH="test-export.unitypackage"

$UNITY_PATH -batchmode -quit -projectPath "." -executeMethod DidomiPackageExporter.ExportPackages

if [ $? -eq 0 ]
then
    echo "Export successful"
else
    echo "Error while exporting"
fi
