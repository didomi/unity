using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using UnityEngine;
using IO.Didomi.SDK.Events;
using System.Collections.Generic;

/// <summary>
/// Tests related to current user status getter and setter
/// </summary>
public class CurrentUserStatusTransactionTestsSuite: DidomiBaseTests
{
    private bool consentChanged = false;

    // Tested vendor: 2KDirect, Inc. (dba iPromote).
    string vendorId = "ipromote";
    // Tested purpose: IAB purpose 2
    string purposeId = "select_basic_ads";

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
        consentChanged = false;
    }

    [TearDown]
    public new void TearDown()
    {
        base.TearDown();
        Didomi.GetInstance().Reset();
    }

    [OneTimeTearDown]
    protected void TearDownSuite()
    {
        eventListener.ConsentChanged -= EventListener_ConsentChanged;
    }

    [UnityTest]
    public IEnumerator TestSinglePurpose()
    {
        Didomi.GetInstance().SetUserDisagreeToAll();
        yield return new WaitUntil(() => consentChanged);

        // Enable vendor and purpose
        var result = Didomi.GetInstance().OpenCurrentUserStatusTransaction()
            .EnablePurpose(purposeId)
            .Commit();
        Assert.IsTrue(result, "Consent should be updated after purpose enabled");

        var userStatus = Didomi.GetInstance().GetCurrentUserStatus();

        Assert.IsTrue(userStatus.Purposes[purposeId].Enabled, "Purpose should be enabled");

        result = Didomi.GetInstance().OpenCurrentUserStatusTransaction()
            .DisablePurpose(purposeId)
            .Commit();
        Assert.IsTrue(result, "Consent should be updated after purpose disabled");

        userStatus = Didomi.GetInstance().GetCurrentUserStatus();

        Assert.IsFalse(userStatus.Purposes[purposeId].Enabled, "Purpose should be disabled");
    }

    [UnityTest]
    public IEnumerator TestSingleVendorAndAssociatedPurposes()
    {
        Didomi.GetInstance().SetUserDisagreeToAll();
        yield return new WaitUntil(() => consentChanged);

        var vendor = new List<Vendor>(Didomi.GetInstance().GetRequiredVendors()).Find(vendor => vendor.Id == vendorId);
        ISet<string> purposeIds = new HashSet<string>();
        purposeIds.UnionWith(vendor.PurposeIds);
        purposeIds.UnionWith(vendor.LegIntPurposeIds);
        var purposeIdsArray = new List<string>(purposeIds).ToArray();

        // Enable vendor and purpose
        var result = Didomi.GetInstance().OpenCurrentUserStatusTransaction()
            .EnableVendor(vendorId)
            .EnablePurposes(purposeIdsArray)
            .Commit();
        Assert.IsTrue(result, "Consent should be updated after vendor and purposes enabled");

        var userStatus = Didomi.GetInstance().GetCurrentUserStatus();

        Assert.IsTrue(userStatus.Vendors[vendorId].Enabled, "Vendor should be enabled");
        Assert.IsTrue(userStatus.Purposes[purposeId].Enabled, "Purpose should be enabled");

        // Disable vendor
        result = Didomi.GetInstance().OpenCurrentUserStatusTransaction()
            .DisableVendor(vendorId)
            .Commit();
        Assert.IsTrue(result, "Consent should be updated after vendor disabled");

        userStatus = Didomi.GetInstance().GetCurrentUserStatus();

        Assert.IsFalse(userStatus.Vendors[vendorId].Enabled, "Vendor should be disabled");
        Assert.IsTrue(userStatus.Purposes[purposeId].Enabled, "Purpose should still be enabled");

        // Disable purposes
        result = Didomi.GetInstance().OpenCurrentUserStatusTransaction()
            .EnableVendor(vendorId)
            .DisablePurposes(purposeIdsArray)
            .Commit();
        Assert.IsTrue(result, "Consent should be updated after purposes disabled");

        userStatus = Didomi.GetInstance().GetCurrentUserStatus();

        Assert.IsFalse(userStatus.Vendors[vendorId].Enabled, "Vendor should be disabled as associated purposes are disabled");
        Assert.IsFalse(userStatus.Purposes[purposeId].Enabled, "Purpose should be disabled");
    }

    [UnityTest]
    public IEnumerator TestSeveralVendors()
    {
        Didomi.GetInstance().SetUserAgreeToAll();
        yield return new WaitUntil(() => consentChanged);

        string[] vendorIdsArray = {
            "ipromote",         // IAB vendor 217
            "amob-txzcQCyq",    // IAB vendor 272
            "152media-Aa6Z6mLC" // IAB vendor 1111
        };

        // Disable vendors
        var result = Didomi.GetInstance().OpenCurrentUserStatusTransaction()
            .DisableVendors(vendorIdsArray)
            .Commit();
        Assert.IsTrue(result, "Consent should be updated after vendors disabled");

        var userStatus = Didomi.GetInstance().GetCurrentUserStatus();

        foreach (string id in vendorIdsArray)
        {
            Assert.IsFalse(userStatus.Vendors[id].Enabled, "Vendor " + id + " should be disabled");
        }

        // Enable vendors again
        result = Didomi.GetInstance().OpenCurrentUserStatusTransaction()
            .EnableVendors(vendorIdsArray)
            .Commit();
        Assert.IsTrue(result, "Consent should be updated after vendors enabled");

        userStatus = Didomi.GetInstance().GetCurrentUserStatus();

        foreach (string id in vendorIdsArray)
        {
            Assert.IsTrue(userStatus.Vendors[id].Enabled, "Vendor " + id + " should be enabled");
        }
    }

    [UnityTest]
    public IEnumerator TestNoUserStatusChange()
    {
        Didomi.GetInstance().SetUserDisagreeToAll();
        yield return new WaitUntil(() => consentChanged);

        // Enable invalid vendor and purpose, disable already disabled vendor and purpose
        var result = Didomi.GetInstance().OpenCurrentUserStatusTransaction()
            .EnablePurpose("unknown-purpose")
            .EnableVendor("unkwnown-vendor")
            .DisableVendor(vendorId)
            .DisablePurpose(purposeId)
            .Commit();
        Assert.IsFalse(result, "No status change");
    }

    private void EventListener_ConsentChanged(object sender, ConsentChangedEvent e)
    {
        consentChanged = true;
    }
}
