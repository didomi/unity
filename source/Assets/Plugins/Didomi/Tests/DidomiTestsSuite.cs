using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using System;

public class DidomiTestsSuite
{
    private bool sdkReady = false;

    [SetUp]
    public void setup()
    {
        try
        {
            string apiKey = "c3cd5b46-bf36-4700-bbdc-4ee9176045aa";
            string noticeId = null;
            string localConfigurationPath = null;
            string remoteConfigurationURL = null;
            string providerId = null;
            bool disableDidomiRemoteConfig = true;
            string languageCode = null;

            Debug.Log("Tests: Initializing sdk");

            Didomi didomi = Didomi.GetInstance();

            didomi.Initialize(
                apiKey,
                localConfigurationPath,
                remoteConfigurationURL,
                providerId,
                disableDidomiRemoteConfig,
                languageCode,
                noticeId);

            didomi.OnReady(
                () =>
                {
                    sdkReady = true;
                }
                );
            didomi.OnError(
                () =>
                {
                    sdkReady = true;
                }
                );
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception : {ex.Message}");
        }
    }

    [UnityTest]
    public IEnumerator TestPurposesAndVendorsCountAfterReset()
    {
        yield return WaitForSdkReady();

        AssertHasRequiredPurposesAndVendors(true);
        AssertHasEnabledPurposesAndVendors(false);
        AssertHasDisabledPurposesAndVendors(false);
    }

    [UnityTest]
    public IEnumerator TestPurposesAndVendorsCountAfterUserAgreeToAll()
    {
        yield return WaitForSdkReady();
        Didomi.GetInstance().SetUserAgreeToAll();

        AssertHasRequiredPurposesAndVendors(true);
        AssertHasEnabledPurposesAndVendors(true);
        AssertHasDisabledPurposesAndVendors(false);
    }

    [UnityTest]
    public IEnumerator TestPurposesAndVendorsCountAfterUserDisagreeToAll()
    {
        yield return WaitForSdkReady();
        Didomi.GetInstance().SetUserDisagreeToAll();

        AssertHasRequiredPurposesAndVendors(true);
        AssertHasEnabledPurposesAndVendors(false);
        AssertHasDisabledPurposesAndVendors(true);
    }

    /**
     * Wait until SDK is ready and Reset the consents
     */
    public IEnumerator WaitForSdkReady()
    {
        Debug.Log("Tests: Waiting for sdk ready");

        yield return new WaitUntil(() => sdkReady);

        Assert.True(Didomi.GetInstance().IsReady());
        Debug.Log("Tests: sdk is ready!");

        Didomi.GetInstance().Reset();
    }

    /**
     * Check required purposes and vendors
     * @param hasRequiredElements whether the required vendors and purposes list should be populated
     */
    private void AssertHasRequiredPurposesAndVendors(bool hasRequiredElements)
    {
        AssertEmptiness(Didomi.GetInstance().GetRequiredPurposeIds(), hasRequiredElements, "requiredPurposeIds");
        AssertEmptiness(Didomi.GetInstance().GetRequiredVendorIds(), hasRequiredElements, "requiredVendorIds");
    }

    /**
     * Check enabled purposes and vendors
     * @param hasEnabledElements whether the enabled vendors and purposes list should be populated
     */
    private void AssertHasEnabledPurposesAndVendors(bool hasEnabledElements)
    {
        AssertEmptiness(Didomi.GetInstance().GetEnabledPurposeIds(), hasEnabledElements, "enabledPurposeIds");
        AssertEmptiness(Didomi.GetInstance().GetEnabledVendorIds(), hasEnabledElements, "enabledVendorIds");
    }

    /**
     * Check disabled purposes and vendors
     * @param hasDisabledElements whether the disabled vendors and purposes list should be populated
     */
    private void AssertHasDisabledPurposesAndVendors(bool hasDisabledElements)
    {
        AssertEmptiness(Didomi.GetInstance().GetDisabledPurposeIds(), hasDisabledElements, "disabledPurposeIds");
        AssertEmptiness(Didomi.GetInstance().GetDisabledVendorIds(), hasDisabledElements, "disabledVendorIds");
    }

    /**
     * Check content of a set
     * @param element the tested set
     * @param expectContent whether the set should have any element
     * @param checkedElement name of the checked element, in case an error message is printed
     */
    private void AssertEmptiness<T>(ISet<T> element, bool expectContent, string checkedElement)
    {
        Assert.NotNull(element, message: $"{checkedElement} is null");
        Assert.AreEqual(expectContent, element.Count > 0, message: $"{checkedElement} count = {element.Count}, expected content? {expectContent}");
    }
}
