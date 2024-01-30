using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using UnityEngine;
using IO.Didomi.SDK.Events;

/// <summary>
/// Tests related to current user status getter and setter
/// </summary>
public class CurrentUserStatusTestsSuite: DidomiBaseTests
{
    private bool consentChanged = false;

    // Tested vendor: 2KDirect, Inc. (dba iPromote).
    string vendorId = "ipromote";
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

    [UnityTest]
    public IEnumerator TestCurrentUserStatusAfterAllEnabledThenDisabled()
    {
        Didomi.GetInstance().SetUserAgreeToAll();
        yield return new WaitUntil(() => consentChanged);

        var userStatus = Didomi.GetInstance().GetCurrentUserStatus();

        var vendorStatus = userStatus.Vendors[vendorId];
        Assert.AreEqual(vendorId, vendorStatus.Id, $"Wrong id for vendor: {vendorStatus.Id}");
        Assert.IsTrue(vendorStatus.Enabled, "Vendor should be enabled");

        var purposeStatus = userStatus.Purposes[purposeId];
        Assert.AreEqual(purposeId, purposeStatus.Id, $"Wrong id for purpose: {purposeStatus.Id}");
        Assert.IsTrue(purposeStatus.Enabled, "Purpose should be enabled");

        // Modify user status
        foreach (CurrentUserStatus.PurposeStatus purpose in userStatus.Purposes.Values)
        {
            purpose.Enabled = false;
        }
        foreach (CurrentUserStatus.VendorStatus vendor in userStatus.Vendors.Values)
        {
            vendor.Enabled = false;
        }
        var result = Didomi.GetInstance().SetCurrentUserStatus(userStatus);
        Assert.IsTrue(result, "Consent not changed");
        yield return new WaitUntil(() => consentChanged);

        var newUserStatus = Didomi.GetInstance().GetCurrentUserStatus();

        var newVendorStatus = newUserStatus.Vendors[vendorId];
        Assert.AreEqual(vendorId, newVendorStatus.Id, $"Wrong id for vendor: {vendorStatus.Id}");
        Assert.IsFalse(newVendorStatus.Enabled, "Vendor should be disabled");

        var newPurposeStatus = newUserStatus.Purposes[purposeId];
        Assert.AreEqual(purposeId, newPurposeStatus.Id, $"Wrong id for purpose: {purposeStatus.Id}");
        Assert.IsFalse(newPurposeStatus.Enabled, "Purpose should be disabled");

    }

    [UnityTest]
    public IEnumerator TestCurrentUserStatusAfterPurposeDisabled()
    {
        Didomi.GetInstance().SetUserAgreeToAll();
        var oldUserStatus = Didomi.GetInstance().GetCurrentUserStatus();

        // Disable the purpose
        oldUserStatus.Purposes[purposeId].Enabled = false;
        var result = Didomi.GetInstance().SetCurrentUserStatus(oldUserStatus);
        Assert.IsTrue(result, "Consent not changed");
        yield return new WaitUntil(() => consentChanged);

        var userStatus = Didomi.GetInstance().GetCurrentUserStatus();

        var vendorStatus = userStatus.Vendors[vendorId];
        Assert.AreEqual(vendorId, vendorStatus.Id, $"Wrong id for vendor: {vendorStatus.Id}");
        Assert.IsFalse(vendorStatus.Enabled, "Vendor should be disabled, as one of its purposes is disabled");

        var purposeStatus = userStatus.Purposes[purposeId];
        Assert.AreEqual(purposeId, purposeStatus.Id, $"Wrong id for purpose: {purposeStatus.Id}");
        Assert.IsFalse(purposeStatus.Enabled, "Purpose should be disabled");
    }

    [UnityTest]
    public IEnumerator TestCurrentUserStatusAfterVendorDisabled()
    {
        Didomi.GetInstance().SetUserAgreeToAll();
        var oldUserStatus = Didomi.GetInstance().GetCurrentUserStatus();

        // Disable the vendor
        oldUserStatus.Purposes[purposeId].Enabled = false;
        var result = Didomi.GetInstance().SetCurrentUserStatus(oldUserStatus);
        Assert.IsTrue(result, "Consent not changed");
        yield return new WaitUntil(() => consentChanged);

        var userStatus = Didomi.GetInstance().GetCurrentUserStatus();

        var vendorStatus = userStatus.Vendors[vendorId];
        Assert.AreEqual(vendorId, vendorStatus.Id, $"Wrong id for vendor: {vendorStatus.Id}");
        Assert.IsFalse(vendorStatus.Enabled, "Vendor should be disabled");

        var purposeStatus = userStatus.Purposes[purposeId];
        Assert.AreEqual(purposeId, purposeStatus.Id, $"Wrong id for purpose: {purposeStatus.Id}");
        Assert.IsTrue(purposeStatus.Enabled, "Purpose should be enabled");
    }

    private void EventListener_ConsentChanged(object sender, ConsentChangedEvent e)
    {
        consentChanged = true;
    }
}
