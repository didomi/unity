#!/bin/bash

#----------------------------------------------------------
# Update Android and iOS SDKs version (latest from repos)
#----------------------------------------------------------

# Get last version from pod
pod_last_version() {
  version=""
  temp_file=$(mktemp)
  pod trunk info Didomi-XCFramework > "$temp_file"
  
  while IFS= read -r line; do
    if [[ "$line" =~ ^[[:space:]]*-[[:space:]]*([0-9]+\.[0-9]+\.[0-9]+) ]]; then
      current_version="${BASH_REMATCH[1]}"
      if [[ -z "$version" || $(printf '%s\n' "$version" "$current_version" | sort -V | tail -n1) == "$current_version" ]]; then
        version=$current_version
      fi
    fi
  done < "$temp_file"
  
  rm "$temp_file"
  echo "$version"
}

# Update android SDK Version
androidVersion=$(curl -s 'https://repo.maven.apache.org/maven2/io/didomi/sdk/android/maven-metadata.xml' | sed -ne '/release/{s/.*<release>\(.*\)<\/release>.*/\1/p;q;}')
if [[ ! $androidVersion =~ ^[0-9]+.[0-9]+.[0-9]+$ ]]; then
  echo "Error while getting android SDK version ($androidVersion)"
  exit 1
fi

echo "Android SDK last version is $androidVersion"

# Update ios SDK Version
iOSVersion=$(pod_last_version)
if [[ ! $iOSVersion =~ ^[0-9]+.[0-9]+.[0-9]+$ ]]; then
  echo "Error while getting ios SDK version ($iOSVersion)"
  exit 1
fi

echo "iOS SDK last version is $iOSVersion"

# Update package.json file with the new versions
sed -i~ -e "s|\"androidNativeVersion\": \"[0-9]\{1,2\}.[0-9]\{1,2\}.[0-9]\{1,2\}\"|\"androidNativeVersion\": \"$androidVersion\"|g" ./Assets/Plugins/Didomi/Resources/package.json || exit 1
sed -i~ -e "s|\"iosNativeVersion\": \"[0-9]\{1,2\}.[0-9]\{1,2\}.[0-9]\{1,2\}\"|\"iosNativeVersion\": \"$iOSVersion\"|g" ./Assets/Plugins/Didomi/Resources/package.json || exit 1

# Cleanup backup files
find . -type f -name '*~' -delete
