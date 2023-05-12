using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DidomiPackageExporter
{
    
    /// <summary>
    /// Export the unitypackage files for the plugin release
    /// </summary>
    [MenuItem("Didomi/Export Packages")]
    static void ExportPackages()
    {
        ExportStandardPackage();
        ExportNoDllPackage();
    }

    /// <summary>
    /// Export the Didomi.unitypackage file for the plugin release
    /// </summary>
    static void ExportStandardPackage()
    {
        string[] exported = GetPathsToExport(noDll: false).ToArray();

        try
        {
            AssetDatabase.ExportPackage(exported, "Didomi.unitypackage", ExportPackageOptions.Recurse);
            Debug.Log("Standard package exported successfully");
        }
        catch (Exception ex)
        {
            Debug.LogErrorFormat("Error while exporting standard package: {0}", ex.Message);
            throw ex;
        }
    }

    /// <summary>
    /// Export the Didomi-noDll.unitypackage file for the plugin release
    /// </summary>
    static void ExportNoDllPackage()
    {
        string[] exported = GetPathsToExport(noDll: true).ToArray();

        try
        {
            AssetDatabase.ExportPackage(exported, "Didomi-noDll.unitypackage", ExportPackageOptions.Recurse);
            Debug.Log("noDll package exported successfully");
        }
        catch (Exception ex)
        {
            Debug.LogErrorFormat("Error while exporting noDll package: {0}", ex.Message);
            throw ex;
        }
    }

    /// <summary>
    /// Get the list of paths to export
    /// <param name="noDll">If true, dll files are excluded from the package</param>
    /// </summary>
    private static List<string> GetPathsToExport(bool noDll)
    {
        string path = Path.Combine(Application.dataPath, "Plugins", "Didomi");
        Debug.LogFormat("Exporting package from {0}", path);

        List<string> included = new List<string>();

        foreach (string directoryPath in Directory.GetDirectories(path))
        {
            string directoryName = Path.GetFileName(directoryPath);
            if (directoryName == "Tests")
            {
                Debug.Log("Excluding Tests directory");
            }
            else if (noDll && directoryName == "IOS")
            {
                Debug.Log("Removing Dll from iOS folder");
                included.AddRange(GetAllFilesPathsExcept(directoryPath, excludedFile: "Newtonsoft.Json.dll"));
            }
            else if (directoryName == "Resources")
            {
                Debug.Log("Removing package-lock.json from resources");
                included.AddRange(GetAllFilesPathsExcept(directoryPath, excludedFile: "package-lock.json"));
            }
            else
            {
                Debug.LogFormat("Adding directory {0}", directoryName);
                included.Add("Assets/Plugins/Didomi/" + directoryName);
            }
        }
        return included;
    }

    /// <summary>
    /// Get the list of paths of all files in the directory, except the specified one
    /// <param name="directoryPath">Inspected directory</param>
    /// <param name="excludedFile">File to exclude from the build</param>
    /// </summary>
    private static List<string> GetAllFilesPathsExcept(string directoryPath, string excludedFile)
    {
        string assetDirectory = "Assets/Plugins/Didomi/" + Path.GetFileName(directoryPath) + "/";
        Debug.LogFormat("Getting files from {0}", directoryPath);

        List<string> included = new List<string>();

        foreach (string subDirectoryPath in Directory.GetDirectories(directoryPath))
        {
            string subDirectoryName = Path.GetFileName(subDirectoryPath);
            Debug.LogFormat("Adding directory {0}", subDirectoryName);
            included.Add(assetDirectory + Path.GetFileName(subDirectoryName));
        }

        foreach (string filePath in Directory.GetFiles(directoryPath))
        {
            string fileName = Path.GetFileName(filePath);
            if (fileName.StartsWith(excludedFile))
            {
                Debug.LogFormat("Excluding file {0}", filePath);
            }
            else
            {
                Debug.LogFormat("Adding file {0}", fileName);
                included.Add(assetDirectory + fileName);
            }
        }

        return included;
    }
}
