#!/bin/bash

#----------------------------------------------------------
# Extract Didomi Unity Plugin version (eg: 1.2.3)
# Returns the current version if match pattern
#----------------------------------------------------------

version=$(jq -r '.version' Assets/Plugins/Didomi/Resources/package.json)
if [[ ! $version =~ ^[0-9]+.[0-9]+.[0-9]+$ ]]; then
  echo "Error while getting plugin version"
  exit 1
fi

echo "$version"
