#!/bin/bash

#-----------------------------------------------------------------------------
# Perform all the necessary steps to update the Didomi Unity Plugin:
# - Update Android and iOS native sdks
# - Update plugin version number
# - Run tests
# - Export the unitypackage files
# Param : major|minor|patch to increment plugin version
#   No argument: use patch as default
#-----------------------------------------------------------------------------

# Retrieve scripts directory
wd="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

echo "Updating native SDKs"
sh $wd/updateNativeSdks.sh || exit 1

if [ "$1" != "" ]
then
  versionParam="$1"
else
  versionParam="patch"
fi
echo "Updating plugin $versionParam version"
sh $wd/updatePluginVersion.sh $1 || exit 1

echo "Running Plugin tests"
sh $wd/runTests.sh || exit 1

echo "Exporting packages"
sh $wd/exportPackages.sh || exit 1

echo "Update complete!"
