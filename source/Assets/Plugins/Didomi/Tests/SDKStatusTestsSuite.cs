using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using UnityEngine;
using IO.Didomi.SDK.Events;

/// <summary>
/// Tests related to lists of purpose and vendor loaded into SDK
/// </summary>
public class SDKStatusTestsSuite: DidomiBaseTests
{
    private bool consentChanged = false;

    [OneTimeSetUp]
    protected void SetUpSuite()
    {
        var listener = new DidomiEventListener();
        listener.ConsentChanged += EventListener_ConsentChanged;
        Didomi.GetInstance().AddEventListener(listener);
    }

    [UnitySetUp]
    public IEnumerator Setup()
    {
        yield return LoadSdk();
    }

    [TearDown]
    public void TearDown()
    {
        consentChanged = false;
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

    private void EventListener_ConsentChanged(object sender, ConsentChangedEvent e)
    {
        consentChanged = true;
    }
}
