using System;
using UnityEditor;
using UnityEditor.TestTools;
using UnityEngine.TestTools;

[assembly: TestPlayerBuildModifier(typeof(IOSTestPlayerBuildModifier))]
[assembly: PostBuildCleanup(typeof(IOSTestPlayerBuildModifier))]

/// <summary>
/// Builds the iOS test player without running it, so that `scripts/runIOSTests.sh` can build and run it with xcodebuild.
/// When Unity runs the player itself, Xcode opens the generated project before the Didomi PostProcessor updates it,
/// and the build stops on a "modified by another application" dialog.
/// Only active when the Editor is started with `-iosTestPlayerPath <directory>`.
/// </summary>
public class IOSTestPlayerBuildModifier : ITestPlayerBuildModifier, IPostBuildCleanup
{
    private const string PlayerPathArgument = "-iosTestPlayerPath";

    // Static because the cleanup is invoked on a new instance of the class
    private static bool isSplitBuild;

    public BuildPlayerOptions ModifyOptions(BuildPlayerOptions playerOptions)
    {
        string playerPath = GetPlayerPathArgument();
        if (playerOptions.target != BuildTarget.iOS || string.IsNullOrEmpty(playerPath))
        {
            return playerOptions;
        }

        // Do not open the generated project in Xcode, the script builds and runs it
        playerOptions.options &= ~BuildOptions.AutoRunPlayer;
        playerOptions.locationPathName = playerPath;
        isSplitBuild = true;
        return playerOptions;
    }

    public void Cleanup()
    {
        if (isSplitBuild)
        {
            // The Editor would otherwise keep waiting for test results. Exit on the next update to let other cleanups run.
            EditorApplication.update += () => EditorApplication.Exit(0);
        }
    }

    private static string GetPlayerPathArgument()
    {
        string[] args = Environment.GetCommandLineArgs();
        int index = Array.IndexOf(args, PlayerPathArgument);
        return index >= 0 && index + 1 < args.Length ? args[index + 1] : null;
    }
}
