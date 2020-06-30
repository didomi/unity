using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

/// <summary>
/// The PostProcessor updates the generated project for Android and iOS.
/// We update native mobile projects to work with the Didomi SDKs (activity type, build settings, etc.).
/// The "PostProcessBuild" attribute on Unity allows developers to add post-processing when Unity generates
/// the Android Studio and Xcode projects.
/// </summary>
public static class PostProcessor
{
    /// <summary>
    /// Path of the folder that contains the `didomi_config.json` file in Unity projects.
    /// </summary>
    private static readonly string DidomiConfigPath = Application.dataPath + @"\DidomiConfig";
	/// <summary>
    /// Path of the `package.json` file.
    /// </summary>
	private static readonly string PackageJsonPath = Application.dataPath + @"\Plugins\Didomi\Resources\package.json";

    /// <summary>
    /// Method called when Unity generates native projects.
    /// </summary>
    /// <param name="buildTarget">Platform (Android, iOS)</param>
    /// <param name="buildPath">Generated project path</param>
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
    {
        Debug.Log("OnPostProcessBuild for " + buildTarget);
        if (buildTarget == BuildTarget.iOS)
        {
            // PBXProject.GetPBXProjectPath returns the wrong path, we need to construct path by ourselves instead
            // var projPath = PBXProject.GetPBXProjectPath(buildPath);
            var projPath = buildPath + "/Unity-iPhone.xcodeproj/project.pbxproj";
            var proj = new PBXProject();
            proj.ReadFromFile(projPath);

            var targetGuid = proj.GetUnityMainTargetGuid();

            //// Configure build settings
            proj.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");
        
            proj.AddBuildProperty(targetGuid, "DEFINES_MODULE", "YES");
            proj.AddBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");

            proj.AddBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");
            proj.AddBuildProperty(targetGuid, "FRAMERWORK_SEARCH_PATHS", "$(inherited) $(PROJECT_DIR) $(PROJECT_DIR)/Frameworks");
            proj.AddBuildProperty(targetGuid, "DYLIB_INSTALL_NAME_BASE", "@rpath");
            proj.AddBuildProperty(targetGuid, "LD_DYLIB_INSTALL_NAME", "@executable_path/../Frameworks/$(EXECUTABLE_PATH)");
          
            CopyDidomiConfigFileToIOSFolder(proj, targetGuid, buildPath);
			CopyPackageJsonToIOSFolder(proj, targetGuid, buildPath);

            proj.WriteToFile(projPath);
        }
        else if (buildTarget == BuildTarget.Android)
        {
            AndroidPostProcess(buildPath);
        }
    }

    /// <summary>
    /// For iOS, copy the local `didomi_config.json` file to `Data/Resources/`.
    /// It also gets added to the build to be available in the final app.
    /// </summary>
    /// <param name="project"></param>
    /// <param name="targetGuid"></param>
    /// <param name="path"></param>
    private static void CopyDidomiConfigFileToIOSFolder(PBXProject project, string targetGuid, string path)
    {
        var files = Directory.GetFiles(DidomiConfigPath);

        foreach (var filePath in files)
        {
            var fileName = Path.GetFileName(filePath);
            var newCopyFile = @"Data\Resources\" + fileName;
            var newCopyFileAbsolutePath = Path.Combine(path, newCopyFile);

            File.Copy(filePath, newCopyFileAbsolutePath, true);
            var fileGuid = project.AddFile(newCopyFile, newCopyFile);
            project.AddFileToBuild(targetGuid, fileGuid);
        }
    }
	
	/// <summary>
    /// For iOS, copy the `package.json` file to `Data/Resources/`.
    /// It also gets added to the build to be available in the final app.
    /// </summary>
    /// <param name="project"></param>
    /// <param name="targetGuid"></param>
    /// <param name="path"></param>
	private static void CopyPackageJsonToIOSFolder(PBXProject project, string targetGuid, string path)
    {
        var fileName = Path.GetFileName(PackageJsonPath);
        var newCopyFile = @"Data\Resources\" + fileName;
        var newCopyFileAbsolutePath = Path.Combine(path, newCopyFile);

        File.Copy(PackageJsonPath, newCopyFileAbsolutePath, true);
        var fileGuid = project.AddFile(newCopyFile, newCopyFile);
        project.AddFileToBuild(targetGuid, fileGuid);
    }

    /// <summary>
    /// Runs tasks for Android Platform.
    /// </summary>
    /// <param name="path"></param>
    private static void AndroidPostProcess(string path)
    {
        UpdateUnityPlayerActivity(path);
        UpdateUnityLibraryDependencies(path);
        CopyDidomiConfigFileToAssetFolder(path);
		CopyPackageJsonFileToAssetFolder(path);
        UpdateStylesThemeToAppCompat(path);
        UpdateThemeAppCompatInAndroidManifestFile(path);
    }

    /// <summary>
    /// Update the `AndroidManifest` file. Set theme for `AppCompat`.
    /// </summary>
    /// <param name="path"></param>
    private static void UpdateThemeAppCompatInAndroidManifestFile(string path)
    {
        var unityAndroidmManifestFile = @"unityLibrary\src\main\AndroidManifest.xml";
        var unityAndroidmManifestFileAbsolutePath = Path.Combine(path, unityAndroidmManifestFile);

        var lines = File.ReadAllLines(unityAndroidmManifestFileAbsolutePath);
        var builder = new StringBuilder();

        var oldValue = @"android:theme=""@style/UnityThemeSelector""";
        var newValue = @"android:theme=""@style/DidomiTheme""";
        foreach (var line in lines)
        {
            if (line.Contains(oldValue))
            {
                builder.AppendLine(line.Replace(oldValue, newValue));
            }
            else
            {
                builder.AppendLine(line);
            }
        }

        File.WriteAllText(unityAndroidmManifestFileAbsolutePath, builder.ToString());
    }

    /// <summary>
    /// Update the `styles.xml` resource file by adding styles for AppCompat.
    /// </summary>
    /// <param name="path"></param>
    private static void UpdateStylesThemeToAppCompat(string path)
    {
        var unityPlayerFile = @"unityLibrary\src\main\res\values\styles.xml";
        var unityPlayerFileAbsolutePath = Path.Combine(path, unityPlayerFile);
        var oldValue = @"</resources>";
        var newValue = @"<style name=""DidomiTheme"" parent =""Theme.AppCompat.Light.DarkActionBar"" />

</resources>";

        ReplaceLineInFile(unityPlayerFileAbsolutePath, oldValue, newValue);
    }

	/// <summary>
    /// For Android, copy the local `didomi_config.json` file to `unityLibrary\src\main\assets\`.
    /// </summary>
	/// <param name="path"></param>
    private static void CopyDidomiConfigFileToAssetFolder(string path)
    {
        var files = Directory.GetFiles(DidomiConfigPath);

        foreach (var filePath in files)
        {
            var fileName = Path.GetFileName(filePath);

            var newCopyFile = @"unityLibrary\src\main\assets\" + fileName;
            var newCopyFileAbsolutePath = Path.Combine(path, newCopyFile);

            File.Copy(filePath, newCopyFileAbsolutePath, true);
        }
    }
	
	/// <summary>
    /// For Android, copy the local `package.json` to `unityLibrary\src\main\assets\`.
    /// </summary>
	/// <param name="path"></param>
	private static void CopyPackageJsonFileToAssetFolder(string path)
    {
        var fileName = Path.GetFileName(PackageJsonPath);

        var newCopyFile = @"unityLibrary\src\main\assets\" + fileName;
        var newCopyFileAbsolutePath = Path.Combine(path, newCopyFile);

        File.Copy(PackageJsonPath, newCopyFileAbsolutePath, true);
    }

    /// <summary>
    /// Update gradle dependencies in the Android project. 
    /// </summary>
    /// <param name="path"></param>
    private static void UpdateUnityLibraryDependencies(string path)
    {
        var unityPlayerFile = @"unityLibrary\build.gradle";
        var unityPlayerFileAbsolutePath = Path.Combine(path, unityPlayerFile);
        var oldValue = "dependencies {";
        var newValue = @"dependencies {
    ext.kotlin_version = '1.3.72'
    implementation 'com.android.support:appcompat-v7:27.1.1'
    implementation 'com.android.support:design:27.1.1'
    implementation 'com.google.android.gms:play-services-ads:15.0.1'
    implementation ""android.arch.lifecycle:extensions:1.1.0""
    implementation 'android.arch.lifecycle:viewmodel:1.1.0'
    // Force customtabs 27.1.1 as com.google.android.gms:play-services-ads:15.0.1 depends on 26.0.1 by default
    // See https://stackoverflow.com/questions/50009286/gradle-mixing-versions-27-1-1-and-26-1-0
    implementation 'com.android.support:customtabs:27.1.1'
    implementation ""org.jetbrains.kotlin:kotlin-stdlib-jdk8:$kotlin_version""
    api 'com.iab.gdpr_android:gdpr_android:1.0.1'
    api 'com.google.code.gson:gson:2.8.5'
    api 'com.rm:rmswitch:1.2.2'";
        ReplaceLineInFile(unityPlayerFileAbsolutePath, oldValue, newValue);
    }

    /// <summary>
    /// Convert the Activity class generated by Unity to AppCompatActivity as that is
    /// what is expected by the Didomi SDK.
    /// </summary>
    /// <param name="path"></param>
    private static void UpdateUnityPlayerActivity(string path)
    {
        var unityPlayerFile = @"unityLibrary\src\main\java\com\unity3d\player\UnityPlayerActivity.java";
        var unityPlayerFileAbsolutePath = Path.Combine(path, unityPlayerFile);
        var oldValue = "public class UnityPlayerActivity extends Activity implements IUnityPlayerLifecycleEvents";
        var newValue = $"import android.support.v7.app.AppCompatActivity;{System.Environment.NewLine}{System.Environment.NewLine}public class UnityPlayerActivity extends AppCompatActivity implements IUnityPlayerLifecycleEvents";
        ReplaceLineInFile(unityPlayerFileAbsolutePath, oldValue, newValue);
    }

    /// <summary>
    /// Replace arguments in path
    /// </summary>
    /// <param name="path"></param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    private static void ReplaceLineInFile(string path, string oldValue, string newValue)
    {
        string text = File.ReadAllText(path);
        text = text.Replace(oldValue, newValue);
        File.WriteAllText(path, text);
    }
}

