#!/bin/bash

#---------------------------------------------------------------------
# Run automated tests on all available plartforms for Didomi Plugin.
#---------------------------------------------------------------------

# Retrieve scripts directory
scriptsDir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

echo "Running iOS tests"
sh $scriptsDir/runIOSTests.sh || exit 1

echo "Running Android tests"
sh $scriptsDir/runAndroidTests.sh || exit 1
