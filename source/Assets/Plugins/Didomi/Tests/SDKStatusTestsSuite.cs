using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using UnityEngine;
using IO.Didomi.SDK.Events;

/// <summary>
/// Tests related to User status (consent)
/// </summary>
public class SDKStatusTestsSuite: DidomiBaseTests
{
    private bool consentChanged = false;

    [OneTimeSetUp]
    protected void SetUpSuite()
    {
        eventListener.ConsentChanged += EventListener_ConsentChanged;
    }

    [UnitySetUp]
    public new IEnumerator Setup()
    {
        base.Setup();
        yield return LoadSdk();
    }

    [TearDown]
    public new void TearDown()
    {
        base.TearDown();
        consentChanged = false;
    }

    [OneTimeTearDown]
    protected void TearDownSuite()
    {
        eventListener.ConsentChanged -= EventListener_ConsentChanged;
    }

    [Test]
    public void TestIsReady()
    {
        Assert.IsTrue(Didomi.GetInstance().IsReady(), "SDK should be initialized");
    }

    [Test]
    public void TestIsConsentRequired()
    {
        Assert.IsTrue(Didomi.GetInstance().IsConsentRequired(), "Consent is required for this regulation");
    }

    [Test]
    public void TestShouldConsentBeCollected()
    {
        Assert.IsTrue(Didomi.GetInstance().ShouldConsentBeCollected(), "Consent should be collected for this regulation");
    }

    [Test]
    public void TestShouldUserStatusBeCollected()
    {
        Assert.IsTrue(Didomi.GetInstance().ShouldUserStatusBeCollected(), "User status should be collected for this regulation");
    }

    [UnityTest]
    public IEnumerator TestIsUserConsentStatusPartial()
    {
        Didomi.GetInstance().Reset();

        Assert.IsTrue(Didomi.GetInstance().IsUserConsentStatusPartial(), "Consent was not given yet");

        consentChanged = false;
        Didomi.GetInstance().SetUserAgreeToAll();
        yield return new WaitUntil(() => consentChanged);

        Assert.IsFalse(Didomi.GetInstance().IsUserConsentStatusPartial(), "Consent was already given");
    }

    [UnityTest]
    public IEnumerator TestIsUserLegitimateInterestStatusPartial()
    {
        Didomi.GetInstance().Reset();

        Assert.IsTrue(Didomi.GetInstance().IsUserLegitimateInterestStatusPartial(), "LI was not given yet");

        consentChanged = false;
        Didomi.GetInstance().SetUserAgreeToAll();
        yield return new WaitUntil(() => consentChanged);

        Assert.IsFalse(Didomi.GetInstance().IsUserLegitimateInterestStatusPartial(), "LI was already given");
    }

    [UnityTest]
    public IEnumerator TestIsUserStatusPartial()
    {
        Didomi.GetInstance().Reset();

        Assert.IsTrue(Didomi.GetInstance().IsUserStatusPartial(), "User status was not given yet");

        consentChanged = false;
        Didomi.GetInstance().SetUserAgreeToAll();
        yield return new WaitUntil(() => consentChanged);

        Assert.IsFalse(Didomi.GetInstance().IsUserStatusPartial(), "User status was already given");
    }

    private void EventListener_ConsentChanged(object sender, ConsentChangedEvent e)
    {
        consentChanged = true;
    }
}
