using System.Collections;
using NUnit.Framework;
using UnityEngine;
using IO.Didomi.SDK;
using System;
using IO.Didomi.SDK.Events;

/// <summary>
/// Provide base methods that can be used in all tests
/// </summary>
public abstract class DidomiBaseTests
{
    private bool sdkReadyCallback = false;
    private bool sdkReadyEvent = false;
    protected static DidomiEventListener eventListener = null;

    public DidomiBaseTests()
    {
        if (eventListener == null)
        {
            eventListener = new DidomiEventListener();
            Didomi.GetInstance().AddEventListener(eventListener);
        }
    }

    protected void Setup()
    {
        eventListener.Ready += EventListener_Ready;
    }

    protected void TearDown()
    {
        eventListener.Ready -= EventListener_Ready;
    }

    /// <summary>
    /// Load SDK and wait until initialization is ready
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadSdk(
        string languageCode = null,
        string noticeId = "Ar7NPQ72",
        string countryCode = null,
        string regionCode = null
    )
    {
        Didomi didomi = Didomi.GetInstance();
        didomi.AddEventListener(eventListener);

        try
        {
            sdkReadyCallback = false;
            sdkReadyEvent = false;
            string apiKey = "9bf8a7e4-db9a-4ff2-a45c-ab7d2b6eadba";

            Debug.Log("Tests: Initializing sdk");

            didomi.Initialize(new DidomiInitializeParameters(
                apiKey,
                languageCode: languageCode,
                noticeId: noticeId,
                countryCode: countryCode,
                regionCode: regionCode
            ));
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception : {ex.Message}");
        }

        // Make sure instance from previous test is not present
        yield return new WaitForSeconds(0.5f);

        didomi.OnReady(
            () =>
            {
                Debug.Log("SDK Ready");
                sdkReadyCallback = true;
            }
        );

        didomi.OnError(
            () =>
            {
                Debug.Log("Initialization error");
                sdkReadyCallback = true;
                sdkReadyEvent = true;
            }
        );

        yield return WaitForSdkReady();
    }

    private void EventListener_Ready(object sender, ReadyEvent e)
    {
        Debug.Log("SDK Ready Event");
        sdkReadyEvent = true;
    }

    /// <summary>
    /// Wait until SDK is ready and Reset the consents
    /// </summary>
    public IEnumerator WaitForSdkReady()
    {
        Debug.Log("Tests: Waiting for sdk ready");

        yield return new WaitUntil(() => sdkReadyCallback && sdkReadyEvent);

        Assert.True(Didomi.GetInstance().IsReady());
        Debug.Log("Tests: sdk is ready!");

        Didomi.GetInstance().Reset();
    }
}
