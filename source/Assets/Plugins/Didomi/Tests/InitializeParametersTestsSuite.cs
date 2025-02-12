using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;

/// <summary>
/// Tests related to SDK initialization parameters
/// </summary>
public class InitializeParametersTestsSuite: DidomiBaseTests
{
    
    private string noticeId = "XWhEXzb9";

    [SetUp]
    public new void Setup()
    {
        base.Setup();
    }

    [TearDown]
    public new void TearDown()
    {
        base.TearDown();
        Didomi.GetInstance().Reset();
    }

    [UnityTest]
    public IEnumerator TestWithCountryFR()
    {
        yield return LoadSdk(noticeId: noticeId, countryCode: "FR");

        Assert.AreEqual("gdpr", Didomi.GetInstance().GetCurrentUserStatus().Regulation);
    }

    [UnityTest]
    public IEnumerator TestWithCountryUSRegionCA()
    {
        yield return LoadSdk(noticeId: noticeId, countryCode: "US", regionCode: "CA");

        Assert.AreEqual("cpra", Didomi.GetInstance().GetCurrentUserStatus().Regulation);
    }

    [UnityTest]
    public IEnumerator TestWithCountryAU()
    {
        yield return LoadSdk(noticeId: noticeId, countryCode: "AU");

        Assert.AreEqual("none", Didomi.GetInstance().GetCurrentUserStatus().Regulation);
    }

    [UnityTest]
    public IEnumerator TestWithIsUnderage()
    {
        yield return LoadSdk(noticeId: noticeId, isUnderage: true);

        Assert.IsTrue(Didomi.GetInstance().IsReady());
    }
}
