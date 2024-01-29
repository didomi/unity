using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using UnityEngine;
using IO.Didomi.SDK.Events;

/// <summary>
/// Tests related to current user status information
/// </summary>
public class GetCurrentUserStatusTestsSuite: DidomiBaseTests
{
    
    private bool consentChanged = false;

    // In case of failure, make sure the tested vendor has
    // both consent and LI purposes associated in the latest GVL.
    // GVL endpoint: https://sdk.privacy-center.org/tcf/v3/vendor-list.json
    // Tested vendor: 2KDirect, Inc. (dba iPromote).
    string vendorId = "217";
    // Tested purpose: IAB purpose 2
    string purposeId = "select_basic_ads";

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
        consentChanged = false;
    }

    [TearDown]
    public void TearDown()
    {
        Didomi.GetInstance().Reset();
    }

    [UnityTest]
    public IEnumerator TestCurrentUserStatusInfo()
    {
        Didomi.GetInstance().SetUserAgreeToAll();
        yield return new WaitUntil(() => consentChanged);

        var currentUserStatus = Didomi.GetInstance().GetCurrentUserStatus();

        Assert.False(string.IsNullOrEmpty(currentUserStatus.ConsentString), "No consent string");
        Assert.False(string.IsNullOrEmpty(currentUserStatus.UserId), "No user id");
        Assert.False(string.IsNullOrEmpty(currentUserStatus.Created), "No created date");
        Assert.False(string.IsNullOrEmpty(currentUserStatus.Updated), "No updated date");
        Assert.AreEqual("gdpr", currentUserStatus.Regulation, $"Incorrect regulation: {currentUserStatus.Regulation}");
        Assert.True(string.IsNullOrEmpty(currentUserStatus.DidomiDcs), "didomiDcs should not be present until feature is enabled");
    }

    private void EventListener_ConsentChanged(object sender, ConsentChangedEvent e)
    {
        consentChanged = true;
    }
}
