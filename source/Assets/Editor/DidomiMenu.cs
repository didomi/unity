using System.IO;
using UnityEditor;
using UnityEngine;

public class DidomiMenu : EditorWindow
{
    [MenuItem("Didomi/Export Packages")]
    static void ExportPackages()
    {
        DidomiPackageExporter.ExportStandardPackage();
        DidomiPackageExporter.ExportNoDllPackage();
    }
}
