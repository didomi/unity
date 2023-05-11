#!/bin/bash

#---------------------------------------------------------------------------------------------
# Increment Didomi Unity Plugin version (from param major|minor|patch)
#   No argument: use patch as default
#---------------------------------------------------------------------------------------------

# Retrieve scripts directory
wd="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

# set nocasematch option
shopt -s nocasematch

# Increment position (Patch is default)
if [[ "$1" =~ ^major$ ]]; then
  position=0
elif [[ "$1" =~ ^minor$ ]]; then
  position=1
else
  position=2
fi

# unset nocasematch option
shopt -u nocasematch

# Increment version (eg `sh increment_version 1.2.3 1` returns `1.3.0`)
# args:
#   - version number (eg `0.32.4`)
#   - increment number: `0` (major) | `1` (minor) | `2` (patch)
increment_version() {
  local delimiter=.
  local array=($(echo "$1" | tr $delimiter '\n'))
  array[$2]=$((array[$2] + 1))
  if [ $2 -lt 2 ]; then array[2]=0; fi
  if [ $2 -lt 1 ]; then array[1]=0; fi
  echo $(
    local IFS=$delimiter
    echo "${array[*]}"
  )
}

# Get Plugin version
version=$(sh $wd/extractPluginVersion.sh)
if [[ ! $version =~ ^[0-9]+.[0-9]+.[0-9]+$ ]]; then
  echo "Error while getting Plugin version"
  exit 1
fi

# Increment Plugin version
pluginVersion=$(increment_version "$version" $position)
echo "Plugin version will change from $version to $pluginVersion"

# Update Plugin version value
jsonFile="Assets/Plugins/Didomi/Resources/package.json"
jq --tab ".version = \"$pluginVersion\"" $jsonFile > package.tmp && mv package.tmp $jsonFile
