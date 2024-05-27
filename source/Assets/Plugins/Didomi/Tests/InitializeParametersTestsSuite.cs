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

    [TearDown]
    public void TearDown()
    {
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
}
