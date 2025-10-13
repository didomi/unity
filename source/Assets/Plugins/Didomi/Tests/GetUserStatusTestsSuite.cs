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
    // GVL endpoint: https://sdk.privacy-center.org/tcf/v3/vendor-list.json
    // Tested vendor: 2KDirect, Inc. (dba iPromote).
    string vendorId = "217";
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
    public IEnumerator TestUserStatusInfo()
    {
        Didomi.GetInstance().SetUserAgreeToAll();
        yield return new WaitUntil(() => consentChanged);

        var userStatus = Didomi.GetInstance().GetUserStatus();

        Assert.False(string.IsNullOrEmpty(userStatus.GetConsentString()), "No consent string");
        Assert.False(string.IsNullOrEmpty(userStatus.GetUserId()), "No user id");
        Assert.False(string.IsNullOrEmpty(userStatus.GetCreated()), "No created date");
        Assert.False(string.IsNullOrEmpty(userStatus.GetUpdated()), "No updated date");
        Assert.AreEqual("gdpr", userStatus.GetRegulation(), $"Incorrect regulation: {userStatus.GetRegulation()}");
        Assert.True(string.IsNullOrEmpty(userStatus.GetDidomiDcs()), "didomiDCS should not be present until feature is enabled");
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
    
    /// <summary>
    /// Check that User Status has the expected enabled / disabled vendors and purposes
    /// </summary>
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

    /// <summary>
    /// Check that User Status enabled / disabled elements has the expected enabled / disabled vendors and purposes
    /// </summary>
    /// <param name="id">Id of element to look after</param>
    /// <param name="choiceIds">List of status</param>
    /// <param name="expectEnabled">Whether the element should be present in the enabled list, otherwise it should be present in the disabled list</param>
    /// <param name="msgId">Type of element, for the failure message</param>
    /// <param name="msgChoiceIds">Type of choices, for the failure message</param>
    private void AssertStatusInList(string id, UserStatus.Ids choiceIds, bool expectEnabled, string msgId, string msgChoiceIds) {
        bool isEnabled = choiceIds.GetEnabled().Contains(id);
        bool isDisabled = choiceIds.GetDisabled().Contains(id);

        // If expectedEnabled is true, the element should be present in the enabled list, otherwise it should not be present
        Assert.AreEqual(expectEnabled, isEnabled, $"{msgId} should {(!expectEnabled ? "not " : "")}be in {msgChoiceIds}.enabled.");
        // If expectedEnabled is true, the element should not be present in the disabled list, otherwise it should be present
        Assert.AreNotEqual(expectEnabled, isDisabled, $"{msgId} should {(expectEnabled ? "not " : "")}be in {msgChoiceIds}.disabled.");
    }

    private void EventListener_ConsentChanged(object sender, ConsentChangedEvent e)
    {
        consentChanged = true;
    }
}
