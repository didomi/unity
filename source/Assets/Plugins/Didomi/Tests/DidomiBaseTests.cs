using System.Collections;
using NUnit.Framework;
using UnityEngine;
using IO.Didomi.SDK;
using System;

/// <summary>
/// Provide base methods that can be used in all tests
/// </summary>
public abstract class DidomiBaseTests
{
    private bool sdkReady = false;

    /// <summary>
    /// Load SDK and wait until initialization is ready
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadSdk(string languageCode = null)
    {
        Didomi didomi = Didomi.GetInstance();

        try
        {
            sdkReady = false;
            string apiKey = "9bf8a7e4-db9a-4ff2-a45c-ab7d2b6eadba";
            string noticeId = "Ar7NPQ72";

            Debug.Log("Tests: Initializing sdk");

            didomi.Initialize(new DidomiInitializeParameters(apiKey, languageCode: languageCode, noticeId: noticeId));
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception : {ex.Message}");
        }

        // Make sure instance from previous test is not present
        yield return new WaitForSeconds(1);

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

        yield return WaitForSdkReady();
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
}
