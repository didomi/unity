using System.Collections;
using NUnit.Framework;
using UnityEngine;
using IO.Didomi.SDK;
using System;

/// <summary>
/// Provide base methods that can be used in all tests
/// </summary>
public class DidomiBaseTests
{
    private bool sdkReady = false;

    /// <summary>
    /// Load SDK and wait until initialization is ready
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadSdk()
    {
        try
        {
            string apiKey = "c3cd5b46-bf36-4700-bbdc-4ee9176045aa";

            Debug.Log("Tests: Initializing sdk");

            Didomi didomi = Didomi.GetInstance();

            didomi.Initialize(new DidomiInitializeParameters(apiKey, disableDidomiRemoteConfig: true));

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
