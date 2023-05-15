#!/bin/bash

#-----------------------------------------------------------------------------
# Update the Didomi Unity Plugin:
# - Update Android and iOS native sdks
# - Run tests
#-----------------------------------------------------------------------------

# Retrieve scripts directory
scriptsDir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

echo "Updating native SDKs"
sh $scriptsDir/updateNativeSdks.sh || exit 1

echo "Running Plugin tests"
sh $scriptsDir/runTests.sh || exit 1

echo "Update complete!"
