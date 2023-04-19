using System.IO;

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
}
