using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;

/// <summary>
/// Tests related to the user's country and region codes
/// </summary>
public class UserCountryRegionCodeTestsSuite: DidomiBaseTests
{
    [UnitySetUp]
    public new IEnumerator Setup()
    {
        base.Setup();
        yield return LoadSdk(countryCode: "FR", regionCode: "IDF");
    }

    [TearDown]
    public new void TearDown()
    {
        base.TearDown();
    }

    [Test]
    public void TestUserCountryCode()
    {
        var result = Didomi.GetInstance().GetUserCountryCode();
        Assert.AreEqual("FR", result);
    }

    [Test]
    public void TestUserRegionCode()
    {
        var result = Didomi.GetInstance().GetUserRegionCode();
        Assert.AreEqual("IDF", result);
    }

}
