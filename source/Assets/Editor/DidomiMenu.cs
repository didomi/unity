using System.IO;
using UnityEditor;
using UnityEngine;

public class DidomiMenu : EditorWindow
{
    [MenuItem("MyProject/List Files")]
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

    [MenuItem("Didomi/Export Packages")]
    static void ListDirectories()
    {
        DidomiPackageExporter.ExportStandardPackage();
        DidomiPackageExporter.ExportNoDllPackage();
    }
}
