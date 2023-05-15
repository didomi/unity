#!/bin/bash

#---------------------------------------------------------------------------------------------
# Update Didomi Unity Plugin and prepare a new release
# The release still has to be created manually in github.
# Param : major|minor|patch to increment plugin version
#   No argument: use patch as default
#---------------------------------------------------------------------------------------------

# Retrieve scripts directory
scriptsDir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

sh $scriptsDir/update.sh || exit 1

echo "Preparing release"
sh $scriptsDir/release.sh $1 || exit 1
