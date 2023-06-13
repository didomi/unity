using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using UnityEngine;
using IO.Didomi.SDK.Events;

/// <summary>
/// Tests related to user status information
/// </summary>
public class GetUserStatusTestsSuite: DidomiBaseTests
{
    
    private bool consentChanged = false;

    // In case of failure, make sure the tested vendor has
    // both consent and LI purposes associated in the latest GVL.
    // GVL endpoint: https://sdk.privacy-center.org/tcf/v2/vendor-list.json
    // Tested vendor: Magnite CTV, Inc.
    string vendorId = "202";
    // Tested purpose: IAB purpose 4
    string purposeId = "select_personalized_ads";

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
    public IEnumerator TestUserStatusInfo()
    {
        Didomi.GetInstance().SetUserAgreeToAll();
        yield return new WaitUntil(() => consentChanged);

        var userStatus = Didomi.GetInstance().GetUserStatus();

        Assert.False(string.IsNullOrEmpty(userStatus.GetConsentString()), "No consent string");
        Assert.False(string.IsNullOrEmpty(userStatus.GetUserId()), "No user id");
        Assert.False(string.IsNullOrEmpty(userStatus.GetCreated()), "No created date");
        Assert.False(string.IsNullOrEmpty(userStatus.GetUpdated()), "No updated date");
    }

    [UnityTest]
    public IEnumerator TestUserStatusAfterAllEnabled()
    {
        Didomi.GetInstance().SetUserStatus(
            purposesConsentStatus: true,
            purposesLIStatus: true,
            vendorsConsentStatus: true,
            vendorsLIStatus: true
        );
        yield return new WaitUntil(() => consentChanged);

        var userStatus = Didomi.GetInstance().GetUserStatus();

        AssertStatus(
            userStatus,
            vendorGlobalEnabled: true,
            vendorGlobalConsentEnabled: true,
            vendorGlobalLIEnabled: true,
            vendorConsentEnabled: true,
            vendorLIEnabled: true,
            purposeGlobalEnabled: true,
            purposeConsentEnabled: true,
            purposeLIEnabled: true
        );
    }

    [UnityTest]
    public IEnumerator TestUserStatusAfterOnlyVendorsEnabled()
    {
        Didomi.GetInstance().SetUserStatus(
            purposesConsentStatus: false,
            purposesLIStatus: false,
            vendorsConsentStatus: true,
            vendorsLIStatus: true
        );
        yield return new WaitUntil(() => consentChanged);

        var userStatus = Didomi.GetInstance().GetUserStatus();

        AssertStatus(
            userStatus,
            vendorGlobalEnabled: false,
            vendorGlobalConsentEnabled: false,
            vendorGlobalLIEnabled: false,
            vendorConsentEnabled: true,
            vendorLIEnabled: true,
            purposeGlobalEnabled: false,
            purposeConsentEnabled: false,
            purposeLIEnabled: false
        );
    }

        [UnityTest]
    public IEnumerator TestUserStatusAfterOnlyConsentEnabled()
    {
        Didomi.GetInstance().SetUserStatus(
            purposesConsentStatus: true,
            purposesLIStatus: false,
            vendorsConsentStatus: true,
            vendorsLIStatus: false
        );
        yield return new WaitUntil(() => consentChanged);

        var userStatus = Didomi.GetInstance().GetUserStatus();

        AssertStatus(
            userStatus,
            vendorGlobalEnabled: false,
            vendorGlobalConsentEnabled: true,
            vendorGlobalLIEnabled: false,
            vendorConsentEnabled: true,
            vendorLIEnabled: false,
            purposeGlobalEnabled: true,
            purposeConsentEnabled: true,
            purposeLIEnabled: false
        );
    }
    
    private void AssertStatus(
        UserStatus userStatus,
        bool vendorGlobalEnabled,
        bool vendorGlobalConsentEnabled,
        bool vendorGlobalLIEnabled,
        bool vendorConsentEnabled,
        bool vendorLIEnabled,
        bool purposeGlobalEnabled,
        bool purposeConsentEnabled,
        bool purposeLIEnabled
    ) {
        AssertStatusInList(
            id: vendorId, 
            choiceIds: userStatus.GetVendors().GetGlobal(),
            expectEnabled: vendorGlobalEnabled,
            msgId: "Vendor",
            msgChoiceIds: "global"
        );

        AssertStatusInList(
            id: vendorId, 
            choiceIds: userStatus.GetVendors().GetGlobalConsent(),
            expectEnabled: vendorGlobalConsentEnabled,
            msgId: "Vendor",
            msgChoiceIds: "globalConsent"
        );

        AssertStatusInList(
            id: vendorId, 
            choiceIds: userStatus.GetVendors().GetGlobalLegitimateInterest(),
            expectEnabled: vendorGlobalLIEnabled,
            msgId: "Vendor",
            msgChoiceIds: "globalLegitimateInterest"
        );

        AssertStatusInList(
            id: vendorId, 
            choiceIds: userStatus.GetVendors().GetConsent(),
            expectEnabled: vendorConsentEnabled,
            msgId: "Vendor",
            msgChoiceIds: "consent"
        );

        AssertStatusInList(
            id: vendorId, 
            choiceIds: userStatus.GetVendors().GetLegitimateInterest(),
            expectEnabled: vendorLIEnabled,
            msgId: "Vendor",
            msgChoiceIds: "legitimateInterest"
        );

        AssertStatusInList(
            id: purposeId, 
            choiceIds: userStatus.GetPurposes().GetGlobal(),
            expectEnabled: purposeGlobalEnabled,
            msgId: "Purpose",
            msgChoiceIds: "global"
        );

        AssertStatusInList(
            id: purposeId, 
            choiceIds: userStatus.GetPurposes().GetConsent(),
            expectEnabled: purposeConsentEnabled,
            msgId: "Purpose",
            msgChoiceIds: "consent"
        );

        AssertStatusInList(
            id: purposeId, 
            choiceIds: userStatus.GetPurposes().GetLegitimateInterest(),
            expectEnabled: purposeLIEnabled,
            msgId: "Purpose",
            msgChoiceIds: "legitimateInterest"
        );
    }

    private void AssertStatusInList(string id, UserStatus.Ids choiceIds, bool expectEnabled, string msgId, string msgChoiceIds) {
        bool enabled = choiceIds.GetEnabled().Contains(id);
        bool disabled = choiceIds.GetDisabled().Contains(id);

        Assert.AreEqual(expectEnabled, enabled, $"{msgId} should {(!enabled ? "not " : "")}be in {msgChoiceIds}.enabled.");
        Assert.AreNotEqual(expectEnabled, disabled, $"{msgId} should {(enabled ? "not " : "")}be in {msgChoiceIds}.disabled.");
    }

    private void EventListener_ConsentChanged(object sender, ConsentChangedEvent e)
    {
        consentChanged = true;
    }
}
