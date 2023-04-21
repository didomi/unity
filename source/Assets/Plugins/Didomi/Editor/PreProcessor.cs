using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// This PreProcessor currently is used to download and import the Didomi XCFramework on the fly in order to avoid having it embedded in the Unity project.
/// This can't be done as part of a post processor because it needs to be done early enough so the Xcode project includes the framework as part of the Unity build process.
/// </summary>
public class PreProcessor: IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        if (report.summary.platform != BuildTarget.iOS)
        {
          return;
        }

        Debug.Log("Didomi Pre processor started.");
        
        XCFrameworkUtils.DownloadFramework(DidomiPaths.FRAMEWORK_NAME);
    }
}
