using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;

/// <summary>
/// Tests checking behavior before SDK is initialized.
/// Note: Test Suite is named so it is 1st in alphabetical order, in order to run before any other test initializes the SDK.
/// </summary>
public class AAABeforeSDKInitTestsSuite : SyncUserBaseTests
{
    private bool sdkWasReady;

    [OneTimeSetUp]
    public new void SetUpSuite()
    {
        base.SetUpSuite();
    }

    [SetUp]
    public new void Setup()
    {
        // Complete results are flaky when SDK is ready before test begins
        sdkWasReady = Didomi.GetInstance().IsReady();
        if (sdkWasReady)
        {
            ResetStatus();
            ResetResults();
        }
        base.Setup();
    }

    [TearDown]
    public new void TearDown()
    {
        base.TearDown();
    }

    [OneTimeTearDown]
    public new void TearDownSuite()
    {
        base.TearDownSuite();
    }

    [UnityTest]
    /// Tests related to user status synchronization between platforms before SDK was initialized
    public IEnumerator TestSyncBeforeInit()
    {
        Didomi.GetInstance().SetUser(testUserId);
        yield return LoadSdk();
        yield return ExpectSyncSuccess("Set user before initialization", true, true, !sdkWasReady);
    }
}
