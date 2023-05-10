using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DidomiPackageExporter
{
    static void ListFiles()
    {
        string path = Application.dataPath;
        ListFilesRecursively(path);
    }

    static void ListFilesRecursively(string path)
    {
        foreach (string filePath in Directory.GetFiles(path))
        {
            Debug.LogFormat("File: {0}", filePath);
        }

        foreach (string directoryPath in Directory.GetDirectories(path))
        {
            ListFilesRecursively(directoryPath);
        }
    }

    
    public static void ExportStandardPackage()
    {
        string[] exported = GetPathsToExport(false).ToArray();
        AssetDatabase.ExportPackage(exported, "didomi.unitypackage", ExportPackageOptions.Recurse);
    }

    public static void ExportNoDllPackage()
    {
        string[] exported = GetPathsToExport(true).ToArray();
        AssetDatabase.ExportPackage(exported, "didomi-noDll.unitypackage", ExportPackageOptions.Recurse);
    }

    private static List<string> GetPathsToExport(bool noDll)
    {
        string path = Path.Combine(Application.dataPath, "Plugins" + Path.DirectorySeparatorChar + "Didomi");
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

    private static List<string> GetAllFilesPathsExcept(string directoryPath, string excludedFile)
    {
        string assetDirectory = "Assets/Plugins/Didomi/" + Path.GetFileName(directoryPath) + "/";
        string path = Path.Combine(Application.dataPath, "Plugins" + Path.DirectorySeparatorChar + "Didomi");
        Debug.LogFormat("Getting files from {0}", directoryPath);

        List<string> included = new List<string>();

        foreach (string subDirectoryPath in Directory.GetDirectories(path))
        {
            included.Add(assetDirectory + Path.GetFileName(subDirectoryPath));
        }

        foreach (string filePath in Directory.GetFiles(directoryPath))
        {
            string fileName = Path.GetFileName(filePath);
            if (fileName == excludedFile)
            {
                Debug.LogFormat("Excluding file {0}", filePath);
            }
            else
            {
                included.Add(assetDirectory + fileName);
            }
        }

        return included;
    }
}
