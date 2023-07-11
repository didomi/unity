using System.IO;
using UnityEngine;

/// <summary>
/// Properties and methods related to paths used to handle Didomi dependencies.
/// </summary>
public static class DidomiPaths
{
    public static string PACKAGE_JSON_PATH = Path.Combine(Application.dataPath, "Plugins", "Didomi", "Resources", "package.json");

    // Paths used in the iOS build process.
    public static class IOS
    {
        public const string FRAMEWORK_NAME = "Didomi.xcframework";
        public const string PLUGINS_IOS = "Plugins/Didomi/IOS";
        public const string INFO_PLIST = "Info.plist";
        public const string DSYMS = "dSYMs";
        public const string IOS_ARCH = "ios-arm64_armv7";
        public const string TVOS_ARCH = "tvos-arm64";

        // Since we are reading the Didomi XCFramework version from the package.json there's no need to set it manually.
        public static string FRAMEWORK_VERSION = PackageJsonUtils.Read().iosNativeVersion;
        public static string ZIP_FILE_NAME = $"didomi-ios-sdk-{FRAMEWORK_VERSION}-xcframework.zip";
        public static string FRAMEWORK_URL = $"https://sdk.didomi.io/ios/{ZIP_FILE_NAME}";

        // This path is part of the main Unity project. It will be used to temporary store the XCFramework.
        // Once the XCFramework is downloaded and imported and the whole XCode project is built, this directory is cleaned up.
        public static string FRAMEWORK_DIRECTORY_IN_SOURCE = Path.Combine("Assets", PLUGINS_IOS);
        public static string FRAMEWORK_PATH_IN_SOURCE = Path.Combine(FRAMEWORK_DIRECTORY_IN_SOURCE, FRAMEWORK_NAME);
        public static string FRAMEWORK_PATH_IN_XCODE = Path.Combine("Frameworks", FRAMEWORK_NAME);
        public static string INFO_PLIST_PATH = Path.Combine(FRAMEWORK_PATH_IN_SOURCE, INFO_PLIST);
        public static string ZIP_FILE_PATH = Path.Combine(FRAMEWORK_DIRECTORY_IN_SOURCE, ZIP_FILE_NAME);
    }
}
