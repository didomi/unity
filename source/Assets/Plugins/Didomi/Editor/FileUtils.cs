using System.IO;
using UnityEditor;

/// <summary>
/// Utility used by Didomi to handle files and directories.
/// </summary>
public class FileUtils {

    /// <summary>
    /// Delete imported file and related meta file.
    /// </summary>
    /// <param name="path">Path to file to be deleted</param>
    public static void DeleteImportedFile(string path)
    {
        if (DeleteFromAssetDatabase(path))
        {
            return;
        }

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        string metaPath = $"{path}.meta";
        if (File.Exists(metaPath))
        {
            File.Delete(metaPath);
        }
    }

    /// <summary>
    /// Delete imported directory and related meta file.
    /// </summary>
    /// <param name="path">Path to directory to be deleted</param>
    public static void DeleteImportedDirectory(string path)
    {
        if (DeleteFromAssetDatabase(path))
        {
            return;
        }

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }

        string metaPath = $"{path}.meta";
        if (File.Exists(metaPath))
        {
            File.Delete(metaPath);
        }
    }

    /// <summary>
    /// Delete an asset known to the AssetDatabase, so that Unity removes it together with its meta file
    /// and import state. Deleting it directly from disk can fail an import still running on it.
    /// </summary>
    /// <param name="path">Path to the asset, relative to the project</param>
    /// <returns>true if the asset was deleted through the AssetDatabase</returns>
    private static bool DeleteFromAssetDatabase(string path)
    {
        string assetPath = path.Replace('\\', '/');
        // Paths outside the project (e.g. the generated Xcode project) are not handled by the AssetDatabase
        if (!assetPath.StartsWith("Assets/"))
        {
            return false;
        }

        bool existsOnDisk = File.Exists(assetPath) || Directory.Exists(assetPath);
        if (!existsOnDisk || string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(assetPath)))
        {
            return false;
        }

        return AssetDatabase.DeleteAsset(assetPath);
    }
}
