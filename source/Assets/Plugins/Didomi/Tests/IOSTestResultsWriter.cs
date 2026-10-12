using System.Globalization;
using System.IO;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.TestRunner;

[assembly: TestRunCallback(typeof(IOSTestResultsWriter))]

/// <summary>
/// Writes the test results to the app Documents folder when tests run in an iOS player,
/// so that `scripts/runIOSTests.sh` can retrieve them from the simulator.
/// </summary>
public class IOSTestResultsWriter : ITestRunCallback
{
    public const string ResultsFileName = "ios-test-results.xml";

    public void RunStarted(ITest testsToRun)
    {
    }

    public void RunFinished(ITestResult testResults)
    {
#if UNITY_IOS && !UNITY_EDITOR
        string xml = $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n"
            + $"<test-run id=\"2\" testcasecount=\"{testResults.Test.TestCaseCount}\" result=\"{testResults.ResultState}\""
            + $" total=\"{testResults.PassCount + testResults.FailCount + testResults.SkipCount + testResults.InconclusiveCount}\""
            + $" passed=\"{testResults.PassCount}\" failed=\"{testResults.FailCount}\" inconclusive=\"{testResults.InconclusiveCount}\""
            + $" skipped=\"{testResults.SkipCount}\" asserts=\"{testResults.AssertCount}\""
            + $" start-time=\"{testResults.StartTime:u}\" end-time=\"{testResults.EndTime:u}\" duration=\"{testResults.Duration.ToString(CultureInfo.InvariantCulture)}\">\n"
            + testResults.ToXml(true).OuterXml
            + "\n</test-run>\n";

        // Write to a temporary file first, so the script never reads a partially written file
        string resultsPath = Path.Combine(Application.persistentDataPath, ResultsFileName);
        string tempPath = resultsPath + ".tmp";
        File.WriteAllText(tempPath, xml);
        if (File.Exists(resultsPath))
        {
            File.Delete(resultsPath);
        }
        File.Move(tempPath, resultsPath);
        Debug.Log($"Didomi - Test results written to {resultsPath}");
#endif
    }

    public void TestStarted(ITest test)
    {
    }

    public void TestFinished(ITestResult result)
    {
    }
}
