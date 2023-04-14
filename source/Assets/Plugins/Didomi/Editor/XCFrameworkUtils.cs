using System.IO;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using UnityEngine;

/// <summary>
/// Utility class used to handle operations related to the XCFramework.
/// </summary>
public class XCFrameworkUtils {
    /// <summary>
    /// Download the XCFramework.
    /// </summary>
    /// <param name="frameworkName">Name of the XCFramework</param>
    public static void DownloadFramework(string frameworkName)
    {
        

        System.Diagnostics.Process process = new System.Diagnostics.Process();

        Directory.CreateDirectory(DidomiPaths.FRAMEWORK_DIRECTORY_IN_SOURCE);

        string downloadFramework = $"echo \"downloading\" && curl -o {DidomiPaths.ZIP_FILE_PATH} {DidomiPaths.FRAMEWORK_URL}";
        string unzipFramework = $"echo \"unzipping\" && unzip {DidomiPaths.ZIP_FILE_PATH} -d {DidomiPaths.FRAMEWORK_DIRECTORY_IN_SOURCE}";

        // Cleanup previous files
        FileUtils.DeleteImportedFile(DidomiPaths.ZIP_FILE_PATH);
        FileUtils.DeleteImportedDirectory(DidomiPaths.FRAMEWORK_PATH_IN_SOURCE);
        
        string processFramework = $"{downloadFramework} && {unzipFramework}";

        process.StartInfo.FileName = "/bin/bash";
        process.StartInfo.Arguments = $"-c \"{processFramework} > output.txt\"";

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.EnvironmentVariables["LANG"] = "en_US.UTF-8"; // set LANG to en_US.UTF-8
        process.Start();

        // Wait for the process to finish and log the output
        string output = process.StandardError.ReadToEnd();

        // Large timeout to make sure the process has enough time to run
        process.WaitForExit(240000);
        UnityEngine.Debug.Log($"Didomi iOS Post Processor downloading framework: {output}");

        // Delete zip file since it's not needed after the framework is imported
        FileUtils.DeleteImportedFile(DidomiPaths.ZIP_FILE_PATH);
    }

    /// <summary>
    /// Import downloaded XCFramework into Xcode project.
    /// </summary>
    /// <param name="buildPath">Generated project path</param>
    public static void ImportXCFrameworkToProject(PBXProject proj, string targetGuid, string buildPath)
    {
        string frameworkDirectory = Path.Combine(buildPath, "Frameworks", DidomiPaths.PLUGINS_IOS);
        string frameworkPath = Path.Combine(frameworkDirectory, DidomiPaths.FRAMEWORK_NAME);
        var unityTargetGuid = proj.GetUnityFrameworkTargetGuid();
        string fileGuid = proj.AddFile(frameworkPath, frameworkPath);
        proj.AddFileToEmbedFrameworks(targetGuid, fileGuid);

        // We need to manually copy the Info plist file of the XCFramework because Unity doesn't do it.
        string infoPlistPath = Path.Combine(frameworkPath, DidomiPaths.INFO_PLIST);
        string iOSMaps = Path.Combine(frameworkPath, DidomiPaths.IOS_ARCH, DidomiPaths.BC_SYMBOLS_MAP);
        string iOSDSyms = Path.Combine(frameworkPath, DidomiPaths.IOS_ARCH, DidomiPaths.DSYMS);
        string tvOSMaps = Path.Combine(frameworkPath, DidomiPaths.TVOS_ARCH, DidomiPaths.BC_SYMBOLS_MAP);
        string tvOSDSyms = Path.Combine(frameworkPath, DidomiPaths.TVOS_ARCH, DidomiPaths.DSYMS);

        // We need to copy files and folders that are not copied as part of the import
        FileUtils.DeleteImportedDirectory(iOSMaps);
        FileUtils.DeleteImportedDirectory(iOSDSyms);
        FileUtils.DeleteImportedDirectory(tvOSMaps);
        FileUtils.DeleteImportedDirectory(tvOSDSyms);

        File.Copy(DidomiPaths.INFO_PLIST_PATH, infoPlistPath, true);
        FileUtil.CopyFileOrDirectory(DidomiPaths.BC_SYMBOLS_MAP_IOS_PATH, iOSMaps);
        FileUtil.CopyFileOrDirectory(DidomiPaths.DSYMS_IOS_PATH, iOSDSyms);
        FileUtil.CopyFileOrDirectory(DidomiPaths.BC_SYMBOLS_MAP_TVOS_PATH, tvOSMaps);
        FileUtil.CopyFileOrDirectory(DidomiPaths.DSYMS_TVOS_PATH, tvOSDSyms);
        FileUtils.DeleteImportedDirectory(DidomiPaths.FRAMEWORK_PATH_IN_SOURCE);
    }
}