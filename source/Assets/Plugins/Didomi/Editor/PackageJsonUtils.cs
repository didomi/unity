using System.IO;
using Newtonsoft.Json;

public class PackageJsonUtils {
  /// <summary>
  /// Returns an object of type PackageJson which contains the content of the package.json file.
  /// </summary>
  /// <returns>Instance of PackageJson containing the content of package.json.</returns>
  public static PackageJson Read()
  {
      string jsonData = File.ReadAllText(DidomiPaths.PACKAGE_JSON_PATH);
      return JsonConvert.DeserializeObject<PackageJson>(jsonData);
  }
}

// Class used to to load the package.json file. Initially used to read the iOS version number.
// Note: property names need to match the JSON payload. This includes matching the casing.
public class PackageJson
{
    public string name { get; set; }
    public string displayName { get; set; }
    public string agentName { get; set; }
    public string version { get; set; }
    public string iosNativeVersion { get; set; }
    public string androidNativeVersion { get; set; }
    public string description { get; set; }
}
