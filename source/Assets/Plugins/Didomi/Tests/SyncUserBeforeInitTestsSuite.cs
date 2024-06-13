using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;

/// <summary>
/// Tests related to user status synchronization between platforms before SDK was initialized
/// </summary>
public class SyncUserBeforeInitTestsSuite : SyncUserBaseTests
{
    [OneTimeSetUp]
    public new void SetUpSuite()
    {
        base.SetUpSuite();
    }

    [SetUp]
    public void Setup()
    {
        if (Didomi.GetInstance().IsReady())
        {
            ResetStatus();
            ResetResults();
        }
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
    public IEnumerator TestSyncBeforeInit()
    {
        Didomi.GetInstance().SetUser(testUserId);
        yield return LoadSdk();
        yield return ExpectSyncSuccess("Set user before initialization", true, true);
    }
}
