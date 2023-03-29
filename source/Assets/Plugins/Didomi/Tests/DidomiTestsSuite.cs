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

        AssertPurposesAndVendorsCount(true, false, false);
    }

    [UnityTest]
    public IEnumerator TestPurposesAndVendorsCountAfterUserAgreeToAll()
    {
        yield return WaitForSdkReady();
        Didomi.GetInstance().SetUserAgreeToAll();

        AssertPurposesAndVendorsCount(true, true, false);
    }

    [UnityTest]
    public IEnumerator TestPurposesAndVendorsCountAfterUserDisagreeToAll()
    {
        yield return WaitForSdkReady();
        Didomi.GetInstance().SetUserDisagreeToAll();

        AssertPurposesAndVendorsCount(true, false, true);
    }


    public IEnumerator WaitForSdkReady()
    {
        Debug.Log("Tests: Waiting for sdk ready");

        yield return new WaitUntil(() => sdkReady);

        Assert.True(Didomi.GetInstance().IsReady());
        Debug.Log("Tests: sdk is ready!");

        Didomi.GetInstance().Reset();
    }

    private void AssertPurposesAndVendorsCount(
        bool hasRequiredElements,
        bool hasEnabledElements,
        bool hasDisabledElements)
    {
        AssertEmptiness(Didomi.GetInstance().GetRequiredPurposeIds(), hasRequiredElements, "requiredPurposeIds");
        AssertEmptiness(Didomi.GetInstance().GetRequiredVendorIds(), hasRequiredElements, "requiredVendorIds");

        AssertEmptiness(Didomi.GetInstance().GetEnabledPurposeIds(), hasEnabledElements, "enabledPurposeIds");
        AssertEmptiness(Didomi.GetInstance().GetEnabledVendorIds(), hasEnabledElements, "enabledVendorIds");

        AssertEmptiness(Didomi.GetInstance().GetDisabledPurposeIds(), hasDisabledElements, "disabledPurposeIds");
        AssertEmptiness(Didomi.GetInstance().GetDisabledVendorIds(), hasDisabledElements, "disabledVendorIds");
    }

    private void AssertEmptiness<T>(ISet<T> element, bool expectContent, string checkedElement)
    {
        Assert.NotNull(element, message: $"{checkedElement} is null");
        Assert.AreEqual(element.Count > 0, expectContent, message: $"{checkedElement} count = {element.Count}, expected content? {expectContent}");
    }
}
