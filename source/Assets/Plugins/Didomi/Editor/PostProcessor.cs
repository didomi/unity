using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Android;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEditor.iOS.Xcode.Extensions;

 using UnityEditor.Build;
 using UnityEditor.Build.Reporting;

/// <summary>
/// The PostProcessorGradleAndroidProject updates the generated project for Android.
/// We post process generated projects to work with the Didomi SDKs (activity type, build settings, etc.).
/// The "IPostGenerateGradleAndroidProject" interface on Unity allows developers to add post-processing when Unity generates
/// apk and Android Studio projects.
/// </summary>
class PostProcessorGradleAndroidProject : IPostGenerateGradleAndroidProject
{
    public int callbackOrder { get { return 0; } }
    public void OnPostGenerateGradleAndroidProject(string path)
    {
        Debug.Log("Didomi OnPostGenerateGradleAndroidProject invoked " + path);
        PostProcessorSettings.InitSettings();
        AndroidPostProcess(path);
    }

    /// <summary>
    /// Runs tasks for Android Platform.
    /// </summary>
    /// <param name="path"></param>
    private static void AndroidPostProcess(string path)
    {
        UpdateUnityPlayerActivity(path);
        UpdateUnityLibraryDependencies(path);
        UpdateGradleProperties(path);
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
        var unityAndroidmManifestFile = $@"src{PostProcessorSettings.FilePathSeperator}main{PostProcessorSettings.FilePathSeperator}AndroidManifest.xml";
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
        var unityPlayerFile = $@"src{PostProcessorSettings.FilePathSeperator}main{PostProcessorSettings.FilePathSeperator}res{PostProcessorSettings.FilePathSeperator}values{PostProcessorSettings.FilePathSeperator}styles.xml";
        var unityPlayerFileAbsolutePath = Path.Combine(path, unityPlayerFile);
        var oldValue = @"</resources>";
        var newValue = @"<style name=""DidomiTheme"" parent =""Theme.AppCompat.Light.DarkActionBar"" />

</resources>";

        PostProcessor.ReplaceLineInFile(unityPlayerFileAbsolutePath, oldValue, newValue);
    }

    /// <summary>
    /// For Android, copy the local `didomi_config.json` file to `unityLibrary/src/main/assets/`.
    /// </summary>
    /// <param name="path"></param>
    private static void CopyDidomiConfigFileToAssetFolder(string path)
    {
        if (!Directory.Exists(PostProcessorSettings.DidomiConfigPath))
        {
            return;
        }

        var files = Directory.GetFiles(PostProcessorSettings.DidomiConfigPath);

        foreach (var filePath in files)
        {
            var fileName = Path.GetFileName(filePath);

            var newCopyFile = $@"src{PostProcessorSettings.FilePathSeperator}main{PostProcessorSettings.FilePathSeperator}assets{PostProcessorSettings.FilePathSeperator}" + fileName;
            var newCopyFileAbsolutePath = Path.Combine(path, newCopyFile);

            File.Copy(filePath, newCopyFileAbsolutePath, true);
        }
    }

    /// <summary>
    /// For Android, copy the local `package.json` to `unityLibrary/src/main/assets/`.
    /// </summary>
    /// <param name="path"></param>
    private static void CopyPackageJsonFileToAssetFolder(string path)
    {
        var fileName = Path.GetFileName(PostProcessorSettings.PackageJsonPath);

        var newCopyFile = $@"src{PostProcessorSettings.FilePathSeperator}main{PostProcessorSettings.FilePathSeperator}assets{PostProcessorSettings.FilePathSeperator}" + fileName;
        var newCopyFileAbsolutePath = Path.Combine(path, newCopyFile);

        File.Copy(PostProcessorSettings.PackageJsonPath, newCopyFileAbsolutePath, true);
    }

    /// <summary>
    /// Update gradle dependencies in the Android project. 
    /// </summary>
    /// <param name="path"></param>
    private static void UpdateUnityLibraryDependencies(string path)
    {
        var unityPlayerFile = $@"build.gradle";
        var unityPlayerFileAbsolutePath = Path.Combine(path, unityPlayerFile);
        var oldValue = "dependencies {";
        var newValue = @"dependencies {
    implementation(""io.didomi.sdk:android:1.72.1"")
    ";
        PostProcessor.ReplaceLineInFile(unityPlayerFileAbsolutePath, oldValue, newValue);
    }

    /// <summary>
    /// Update gradle properties in the Android project. 
    /// </summary>
    /// <param name="path"></param>
    private static void UpdateGradleProperties(string path)
    {
        var unityGradleProperties = $@"..{PostProcessorSettings.FilePathSeperator}gradle.properties";
        var unityGradleAbsolutePath = Path.Combine(path, unityGradleProperties);
        var gradleProperties = File.ReadAllText(unityGradleAbsolutePath);

        if (gradleProperties.Contains("android.useAndroidX=false"))
        {
            PostProcessorSettings.ReplaceLineInFile(unityGradleAbsolutePath, "android.useAndroidX=false", "android.useAndroidX=true");
        }
        else if (!gradleProperties.Contains("android.useAndroidX=true"))
        {
            File.AppendAllText(unityGradleAbsolutePath, System.Environment.NewLine + "android.useAndroidX=true");
        }
    }

    /// <summary>
    /// Convert the Activity class generated by Unity to AppCompatActivity as that is
    /// what is expected by the Didomi SDK.
    /// </summary>
    /// <param name="path"></param>
    private static void UpdateUnityPlayerActivity(string path)
    {
        var unityPlayerFile = $@"src{PostProcessorSettings.FilePathSeperator}main{PostProcessorSettings.FilePathSeperator}java{PostProcessorSettings.FilePathSeperator}com{PostProcessorSettings.FilePathSeperator}unity3d{PostProcessorSettings.FilePathSeperator}player{PostProcessorSettings.FilePathSeperator}UnityPlayerActivity.java";
        var unityPlayerFileAbsolutePath = Path.Combine(path, unityPlayerFile);
        var oldValue = "public class UnityPlayerActivity extends Activity implements IUnityPlayerLifecycleEvents";
        var newValue = $"import androidx.appcompat.app.AppCompatActivity;{System.Environment.NewLine}{System.Environment.NewLine}public class UnityPlayerActivity extends AppCompatActivity implements IUnityPlayerLifecycleEvents";
        PostProcessor.ReplaceLineInFile(unityPlayerFileAbsolutePath, oldValue, newValue);
    }
}

/// <summary>
/// The PostProcessor updates the generated project for iOS.
/// We update XCode mobile projects to work with the Didomi SDKs (activity type, build settings, etc.).
/// The "PostProcessBuild" attribute on Unity allows developers to add post-processing when Unity generates
/// for Xcode projects.
/// </summary>
public static class PostProcessor
{
    /// <summary>
    /// Method called when Unity generates native projects.
    /// </summary>
    /// <param name="buildTarget">Platform (Android, iOS)</param>
    /// <param name="buildPath">Generated project path</param>
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
    {
        Debug.Log("Didomi OnPostProcessBuild invoked" + buildPath);
        
        if (buildTarget == BuildTarget.iOS)
        {

            PostProcessorSettings.InitSettings();
            
            // PBXProject.GetPBXProjectPath returns the wrong path, we need to construct path by ourselves instead
            // var projPath = PBXProject.GetPBXProjectPath(buildPath);
            var projPath = buildPath + $"{PostProcessorSettings.FilePathSeperator}Unity-iPhone.xcodeproj{PostProcessorSettings.FilePathSeperator}project.pbxproj";
            var proj = new PBXProject();
            proj.ReadFromFile(projPath);

            var targetGuid = proj.GetUnityMainTargetGuid();

            // Configure build settings
            proj.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");

            proj.AddBuildProperty(targetGuid, "DEFINES_MODULE", "YES");
            proj.AddBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");

            proj.AddBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");
            proj.AddBuildProperty(targetGuid, "FRAMEWORK_SEARCH_PATHS", "$(inherited) $(PROJECT_DIR) $(PROJECT_DIR)/Frameworks");
            proj.AddBuildProperty(targetGuid, "DYLIB_INSTALL_NAME_BASE", "@rpath");
            proj.AddBuildProperty(targetGuid, "LD_DYLIB_INSTALL_NAME", "@executable_path/../Frameworks/$(EXECUTABLE_PATH)");

            string frameworkName = "Didomi.xcframework";
            string frameworkVersion = "1.86.0";          
            string zipFileName = $"didomi-ios-sdk-{frameworkVersion}-xcframework.zip";
            string frameworkDirectory = $"{Application.dataPath}/Frameworks/Plugins/Didomi/IOS";
            string frameworkPath = $"{frameworkDirectory}/{frameworkName}";
            DownloadFramework(frameworkPath, frameworkDirectory, zipFileName);
            UnityEngine.Debug.Log(" ** Application.dataPath: " + Application.dataPath);
            UnityEngine.Debug.Log(" ** frameworkPath: " + frameworkPath);

            var unityTargetGuid = proj.GetUnityFrameworkTargetGuid();
            string fileGuid = proj.AddFile(frameworkPath, "Frameworks/" + Path.GetFileName(frameworkPath), PBXSourceTree.Source);
            proj.AddFileToBuild(unityTargetGuid, fileGuid);

            SetupDidomiFrameworkForTargetSDK(proj, targetGuid, buildPath);
            CopyDidomiConfigFileToIOSFolder(proj, targetGuid, buildPath);
            CopyPackageJsonToIOSFolder(proj, targetGuid, buildPath);

            proj.WriteToFile(projPath);

      
        }
    }

    /// <summary>
    /// Download the XCFramework.
    /// </summary>
    /// <param name="buildPath">Generated project path</param>
    private static void DownloadFramework(string frameworkPath, string frameworkDirectory, string zipFileName)
    {
        System.Diagnostics.Process process = new System.Diagnostics.Process();

        UnityEngine.Debug.Log("*** frameworkPath: " + frameworkPath);

        string url = $"https://sdk.didomi.io/ios/{zipFileName}";

        string zipFilePath = $"{frameworkDirectory}/{zipFileName}";
        string createFrameworkDirectory = $"mkdir -p {frameworkDirectory}";
        string downloadFramework = $"echo \"downloading\" && curl -o {zipFilePath} {url}";
        string unzipFramework = $"echo \"unzipping\" && unzip {zipFilePath} -d {frameworkDirectory}";

        // Cleanup previous files
        string deleteZippedFramework = $"rm -rf {zipFilePath} && rm -rf {zipFilePath}.meta";
        string deleteFramework = $"rm -rf {frameworkPath} && rm -rf {frameworkPath}.meta";
        string cleanup = $"{deleteZippedFramework} && {deleteFramework}";

        // Cleanup binaries that are not used
        string deleteDeviceFramework = $"rm -rf {frameworkPath}/ios-arm64_armv7";
        string deleteSimulatorFramework = $"rm -rf {frameworkPath}/ios-arm64_i386_x86_64-simulator";
        string deleteTVOSDeviceFramework = $"rm -rf {frameworkPath}/tvos-arm64";
        string deleteTVOSSimulatorFramework = $"rm -rf {frameworkPath}/tvos-arm64_x86_64-simulator";
        string deleteUnused = "";
        if (PlayerSettings.iOS.sdkVersion == iOSSdkVersion.DeviceSDK)
        {
            UnityEngine.Debug.Log(" ** Didomi SDK: Building for Device");
            deleteUnused = $"{deleteSimulatorFramework} && {deleteTVOSDeviceFramework} && {deleteTVOSSimulatorFramework}";
        }
        else
        {
            UnityEngine.Debug.Log(" ** Didomi SDK: Building for Simulator");
            deleteUnused = $"{deleteDeviceFramework} && {deleteTVOSDeviceFramework} && {deleteTVOSSimulatorFramework}";
        }
        
        string processFramework = $"{cleanup} && {createFrameworkDirectory} && {downloadFramework} && {unzipFramework} && {deleteZippedFramework} && {deleteUnused}";

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
    }
    

    /// <summary>
    /// For iOS platform, setups and configures didomi native libs for target SDK Device or Simulator.
    /// </summary>
    /// <param name="project"></param>
    /// <param name="targetGuid"></param>
    /// <param name="path"></param>
    private static void SetupDidomiFrameworkForTargetSDK(PBXProject project, string targetGuid, string path)
    {
        var xcframeworkPath = $"Frameworks{PostProcessorSettings.FilePathSeperator}Plugins{PostProcessorSettings.FilePathSeperator}Didomi{PostProcessorSettings.FilePathSeperator}IOS{PostProcessorSettings.FilePathSeperator}Didomi.xcframework";
        var unusedSDKPath = string.Empty;
        var simulatorPath = $"{xcframeworkPath}{PostProcessorSettings.FilePathSeperator}ios-arm64_i386_x86_64-simulator";
        var devicePath = $"{xcframeworkPath}{PostProcessorSettings.FilePathSeperator}ios-arm64_armv7";

        if (PlayerSettings.iOS.sdkVersion == iOSSdkVersion.DeviceSDK)
        {
            unusedSDKPath = simulatorPath;
        }
        else
        {
            unusedSDKPath = devicePath;
            var mmFile = $"{path}{PostProcessorSettings.FilePathSeperator}Libraries{PostProcessorSettings.FilePathSeperator}Plugins{PostProcessorSettings.FilePathSeperator}Didomi{PostProcessorSettings.FilePathSeperator}IOS{PostProcessorSettings.FilePathSeperator}Didomi.mm";
            var headerFileImportLineDevice = @"#import ""Frameworks/Plugins/Didomi/IOS/Didomi.xcframework/ios-arm64_armv7/Didomi.framework/Headers/Didomi-Swift.h""";
            var headerFileImportLineSimulator = @"#import ""Frameworks/Plugins/Didomi/IOS/Didomi.xcframework/ios-arm64_i386_x86_64-simulator/Didomi.framework/Headers/Didomi-Swift.h""";
            ReplaceLineInFile(mmFile, headerFileImportLineDevice, headerFileImportLineSimulator);
        }

		if(Directory.Exists($"{path}{PostProcessorSettings.FilePathSeperator}{unusedSDKPath}"))
		{
			Directory.Delete($"{path}{PostProcessorSettings.FilePathSeperator}{unusedSDKPath}", true);
			var frameworkPath = $@"{unusedSDKPath}{PostProcessorSettings.FilePathSeperator}Didomi.framework";
			var guid = project.FindFileGuidByProjectPath(frameworkPath);
			var unityFrameworkGuid = project.GetUnityFrameworkTargetGuid();

			project.RemoveFileFromBuild(targetGuid, guid);
			project.RemoveFileFromBuild(unityFrameworkGuid, guid);
			project.RemoveFrameworkFromProject(targetGuid, frameworkPath);
			project.RemoveFrameworkFromProject(unityFrameworkGuid, frameworkPath);
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
        if (!Directory.Exists(PostProcessorSettings.DidomiConfigPath))
        {
            return;
        }

        var files = Directory.GetFiles(PostProcessorSettings.DidomiConfigPath);

        foreach (var filePath in files)
        {
            var fileName = Path.GetFileName(filePath);
            var newCopyFile = $@"Data{PostProcessorSettings.FilePathSeperator}Resources{PostProcessorSettings.FilePathSeperator}" + fileName;
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
        var fileName = Path.GetFileName(PostProcessorSettings.PackageJsonPath);
        var newCopyFile = $@"Data{PostProcessorSettings.FilePathSeperator}Resources{PostProcessorSettings.FilePathSeperator}" + fileName;
        var newCopyFileAbsolutePath = Path.Combine(path, newCopyFile);

        File.Copy(PostProcessorSettings.PackageJsonPath, newCopyFileAbsolutePath, true);
        var fileGuid = project.AddFile(newCopyFile, newCopyFile);
        project.AddFileToBuild(targetGuid, fileGuid);
    }

    /// <summary>
    /// Replace arguments in path
    /// </summary>
    /// <param name="path"></param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    public static void ReplaceLineInFile(string path, string oldValue, string newValue)
    {
        string text = File.ReadAllText(path);
        if (text.Contains(newValue)) return;

        text = text.Replace(oldValue, newValue);
        File.WriteAllText(path, text);
    }
}

/// <summary>
/// Initial settings for PostProcessors
/// </summary>
public static class PostProcessorSettings
{
    /// <summary>
    /// Path of the folder that contains the `didomi_config.json` file in Unity projects.
    /// </summary>
    public static string DidomiConfigPath = string.Empty;

    /// <summary>
    /// Path of the `package.json` file.
    /// </summary>
    public static string PackageJsonPath = string.Empty;

    /// <summary>
    /// File path seperator for Editor OS
    /// </summary>
    public static string FilePathSeperator = string.Empty;

    /// <summary>
    /// Initialize Environment Settings
    /// </summary>
    public static void InitSettings()
    {
        bool isWinEditor = Application.platform == RuntimePlatform.WindowsEditor;
        bool isOSXEditor = Application.platform == RuntimePlatform.OSXEditor;

        if (isOSXEditor)
        {
            FilePathSeperator = "/";
        }
        else
        {
            FilePathSeperator = @"\";
        }

        DidomiConfigPath = Application.dataPath + $@"{FilePathSeperator}DidomiConfig";
        PackageJsonPath = Application.dataPath + $@"{FilePathSeperator}Plugins{FilePathSeperator}Didomi{FilePathSeperator}Resources{FilePathSeperator}package.json";
    }

    /// <summary>
    /// Replace arguments in path
    /// </summary>
    /// <param name="path"></param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    public static void ReplaceLineInFile(string path, string oldValue, string newValue)
    {
        string text = File.ReadAllText(path);
        text = text.Replace(oldValue, newValue);
        File.WriteAllText(path, text);
    }
}