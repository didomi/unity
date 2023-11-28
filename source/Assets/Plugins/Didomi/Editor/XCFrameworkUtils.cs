using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
#if UNITY_IOS || UNITY_TVOS
using System.Collections;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
#endif

/// <summary>
/// Utility class used to handle operations related to the Didomi XCFramework.
/// </summary>
public class XCFrameworkUtils {
    /// <summary>
    /// Download the XCFramework.
    /// </summary>
    /// <param name="frameworkName">Name of the XCFramework</param>
    public static void DownloadFramework(string frameworkName)
    {
        Directory.CreateDirectory(DidomiPaths.IOS.FRAMEWORK_DIRECTORY_IN_SOURCE);
        // Cleanup previous files
        FileUtils.DeleteImportedFile(DidomiPaths.IOS.ZIP_FILE_PATH);
        FileUtils.DeleteImportedDirectory(DidomiPaths.IOS.FRAMEWORK_PATH_IN_SOURCE);

        UnityWebRequest request = UnityWebRequest.Get(DidomiPaths.IOS.FRAMEWORK_URL);
        request.timeout = 240; // 60*4 = 240 seconds (4 minutes)
        request.SendWebRequest();

        while (!request.isDone) { } // Wait for the request to finish

        if (request.result == UnityWebRequest.Result.Success)
        {
            File.WriteAllBytes(DidomiPaths.IOS.ZIP_FILE_PATH, request.downloadHandler.data);
            Debug.Log($"Didomi - Download of Didomi framework succeeded from: {request.url}");
        }
        else
        {
            Debug.LogError($"Didomi - Download of Didomi framework failed: {request.error}");
        }
        
        // Create an unzip command string to be executed by bash shell using the 'unzip' command, which extracts files from a ZIP archive 
        string unzipFramework = $"echo \"unzipping\" && unzip {DidomiPaths.IOS.ZIP_FILE_PATH} -d {DidomiPaths.IOS.FRAMEWORK_DIRECTORY_IN_SOURCE}";
        BashUtils.Execute(unzipFramework);

        // Delete zip file since it's not needed after the framework has been unzipped.
        FileUtils.DeleteImportedFile(DidomiPaths.IOS.ZIP_FILE_PATH);
    }

    #if UNITY_IOS || UNITY_TVOS
    /// <summary>
    /// Import XCFramework into Xcode project.
    /// </summary>
    /// <param name="buildPath">Generated project path</param>
    public static void ImportXCFrameworkToProject(PBXProject proj, string targetGuid, string buildPath)
    {
        string frameworkDirectory = Path.Combine(buildPath, "Frameworks", DidomiPaths.IOS.PLUGINS_IOS);
        string frameworkPath = Path.Combine(frameworkDirectory, DidomiPaths.IOS.FRAMEWORK_NAME);
        string fileGuid = proj.AddFile(frameworkPath, DidomiPaths.IOS.FRAMEWORK_PATH_IN_XCODE);
        proj.AddFileToEmbedFrameworks(targetGuid, fileGuid);

        // We need to manually copy the Info plist file because Unity doesn't do it.
        string infoPlistPath = Path.Combine(frameworkPath, DidomiPaths.IOS.INFO_PLIST);
        File.Copy(DidomiPaths.IOS.INFO_PLIST_PATH, infoPlistPath, true);

        // We need to manually copy files that are used to symbolicate crash reports because Unity doesn't do it.
        CopyDSyms(frameworkPath, DidomiPaths.IOS.IOS_ARCH);
        CopyDSyms(frameworkPath, DidomiPaths.IOS.TVOS_ARCH);

        // We delete the whole directory where the framework is placed after we are done.
        FileUtils.DeleteImportedDirectory(DidomiPaths.IOS.FRAMEWORK_PATH_IN_SOURCE);
    }
    #endif

    /// <summary>
    /// Copy dSYMs folder
    /// </summary>
    /// <param name="frameworkPath">Path to framework</param>
    /// <param name="arch">Architecture (ios-arm64_armv7, ios-arm64_i386_x86_64-simulator, etc.)</param>
    private static void CopyDSyms(string frameworkPath, string arch) {
        string targetPath = Path.Combine(frameworkPath, arch, DidomiPaths.IOS.DSYMS);
        string sourcePath = Path.Combine(DidomiPaths.IOS.FRAMEWORK_PATH_IN_SOURCE, arch, DidomiPaths.IOS.DSYMS);
        FileUtils.DeleteImportedDirectory(targetPath);
        FileUtil.CopyFileOrDirectory(sourcePath, targetPath);
    }
}
