using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// The PreProcessor currently is used to import the Didomi XCFramework on the fly in order to avoid having it embedded in the Unity project.
/// This can't be done as part of a post processor because this needs to be done early enough so the Xcode project includes the framework.
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
        
        XCFrameworkUtils.DownloadFramework(DidomiPaths.FRAMEWORK_NAME);

        // Important: This imports the framework into the project source code temporarily.
        // Once the build is ready this temporary files are removed.
        AssetDatabase.ImportAsset(DidomiPaths.FRAMEWORK_PATH_IN_SOURCE, ImportAssetOptions.ForceUpdate);
        AssetDatabase.SaveAssets();
    }
}
