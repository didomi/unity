#!/bin/bash

#---------------------------------------------------------------------------------------------
# Prepare a new release for Didomi Unity Plugin:
# - Increment plugin version
# - Commit changes and create the tag
# - Export the unitypackage files
# The release still has to be created manually in github.
# Param : major|minor|patch to increment plugin version
#   No argument: use patch as default
#---------------------------------------------------------------------------------------------

# Retrieve scripts directory
wd="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

# Update plugin version
if [ "$1" != "" ]
then
  versionParam="$1"
else
  versionParam="patch"
fi
echo "Updating plugin $versionParam version"

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
oldVersion=$(sh $wd/extractPluginVersion.sh)
if [[ ! $oldVersion =~ ^[0-9]+.[0-9]+.[0-9]+$ ]]; then
  echo "Error while getting Plugin version"
  exit 1
fi

# Increment Plugin version
newVersion=$(increment_version "$oldVersion" $position)
echo "Plugin version will change from $oldVersion to $newVersion"

# Update Plugin version value
jsonFile="Assets/Plugins/Didomi/Resources/package.json"
jq --tab ".version = \"$newVersion\"" $jsonFile > package.tmp && mv package.tmp $jsonFile

# Create / push commit
git add Assets/Plugins/Didomi/Resources/package.json
git commit -m "[Bot] Increment version from $oldVersion to $newVersion"
git push || exit 1

tagName="v$newVersion"
# create / push tag
git tag -a "$tagName" -m "Created tag for version $tagName"
git push origin "$tagName"

echo "Exporting packages"
sh $wd/exportPackages.sh || exit 1

echo "Release $tagName is ready!"
