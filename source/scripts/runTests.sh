#!/bin/bash

#---------------------------------------------------------------------
# Run automated tests on all available plartforms for Didomi Plugin.
#---------------------------------------------------------------------

# Retrieve scripts directory
wd="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

echo "Running iOS tests"
sh $wd/runIOSTests.sh || exit 1

echo "Running Android tests"
sh $wd/runAndroidTests.sh || exit 1
