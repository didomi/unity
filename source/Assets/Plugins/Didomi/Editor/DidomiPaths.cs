using System.IO;

/// <summary>
/// Properties and methods related to paths used to handle Didomi dependencies.
/// </summary>
public static class DidomiPaths
{
  public const string FRAMEWORK_VERSION = "1.87.0";
  public static string ZIP_FILE_NAME = $"didomi-ios-sdk-{FRAMEWORK_VERSION}-xcframework.zip";
  public static string FRAMEWORK_URL = $"https://sdk.didomi.io/ios/{ZIP_FILE_NAME}";

  public const string FRAMEWORK_NAME = "Didomi.xcframework";
  public const string PLUGINS_IOS = "Plugins/Didomi/IOS";
  // This path is part of the main Unity project. It will be used to temporary store the XCFramework.
  // Once the XCFramework is downloaded and imported and the whole XCode project is built, this directory is cleaned up.
  public static string FRAMEWORK_DIRECTORY_IN_SOURCE = Path.Combine("Assets", PLUGINS_IOS);
  public static string FRAMEWORK_PATH_IN_SOURCE = Path.Combine(FRAMEWORK_DIRECTORY_IN_SOURCE, FRAMEWORK_NAME);
  public static string INFO_PLIST_PATH = Path.Combine(FRAMEWORK_PATH_IN_SOURCE, "Info.plist");
  public const string INFO_PLIST = "Info.plist";
  public static string ZIP_FILE_PATH = Path.Combine(FRAMEWORK_DIRECTORY_IN_SOURCE, ZIP_FILE_NAME);
  public const string BC_SYMBOLS_MAP = "BCSymbolMaps";
  public const string DSYMS = "dSYMs";
  public const string IOS_ARCH = "ios-arm64_armv7";
  public const string TVOS_ARCH = "tvos-arm64";
}