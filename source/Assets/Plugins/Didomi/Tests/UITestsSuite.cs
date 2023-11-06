using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using UnityEngine;
using IO.Didomi.SDK.Events;

/// <summary>
/// Tests related to UI elements
/// </summary>
public class UITestsSuite: DidomiBaseTests
{
    private bool noticeDisplayedEvent = false;
    private bool noticeHiddenEvent = false;
    private bool preferencesDisplayedEvent = false;
    private bool preferencesHiddenEvent = false;

    [OneTimeSetUp]
    protected void SetUpSuite()
    {
        var listener = new DidomiEventListener();
        listener.ShowNotice += EventListener_ShowNotice;
        listener.HideNotice += EventListener_HideNotice;
        listener.ShowPreferences += EventListener_ShowPreferences;
        listener.HidePreferences += EventListener_HidePreferences;
        Didomi.GetInstance().AddEventListener(listener);
    }

    [UnitySetUp]
    public IEnumerator Setup()
    {
        yield return LoadSdk();
        Didomi.GetInstance().OnReady(
            () =>
            {
                Didomi.GetInstance().Reset();
            }
        );
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Didomi.GetInstance().HidePreferences();
        Didomi.GetInstance().HideNotice();
        yield return new WaitForSeconds(1);
        ResetEvents();
    }

    [UnityTest]
    public IEnumerator TestSetupUI()
    {
        Didomi.GetInstance().SetupUI();

        yield return new WaitForSeconds(1);
        Assert.True(Didomi.GetInstance().IsNoticeVisible(), "Notice should be visible");
        AssertEvents("Called SetupUI", expectNoticeDisplayed: true);
    }

    [UnityTest]
    public IEnumerator TestNoticeVisibility()
    {
        // On IOS, ShowNotice() requires a previous call to SetupUI()
        yield return SetupUIWithoutNotice();

        // In recent Unity versions, an error message is triggered on iOS, we need to ignore it:
        // '[Error] An abnormal situation has occurred: the PlayerLoop internal function has been called recursively. Please contact Customer Support with a sample project so that we can reproduce the problem and troubleshoot it.'
        bool skipErrorMessage = Application.platform == RuntimePlatform.IPhonePlayer
            && string.Compare(Application.unityVersion, "2021.3.20f1") > 0;
        if (skipErrorMessage)
        {
            LogAssert.ignoreFailingMessages = true;
        }
        Didomi.GetInstance().ShowNotice();
        if (skipErrorMessage)
        {
            LogAssert.ignoreFailingMessages = false;
        }
        yield return new WaitForSeconds(1);
        Assert.True(Didomi.GetInstance().IsNoticeVisible(), "Notice should be visible");
        AssertEvents("Called ShowNotice", expectNoticeDisplayed: true);

        ResetEvents();

        Didomi.GetInstance().HideNotice();
        yield return new WaitForSeconds(1);
        Assert.False(Didomi.GetInstance().IsNoticeVisible(), "Notice should not be visible anymore");
        AssertEvents("Called HideNotice", expectNoticeHidden: true);
    }

    [UnityTest]
    public IEnumerator TestPreferencesVisibility()
    {
        Didomi.GetInstance().ShowPreferences();
        yield return new WaitForSeconds(1);
        Assert.False(Didomi.GetInstance().IsNoticeVisible(), "Notice should not be visible");
        Assert.True(Didomi.GetInstance().IsPreferencesVisible(), "Preferences screen should be visible");
        AssertEvents("Called ShowPreferences", expectPreferencesDisplayed: true);

        ResetEvents();

        Didomi.GetInstance().HidePreferences();
        yield return new WaitForSeconds(1);
        Assert.False(Didomi.GetInstance().IsNoticeVisible(), "Notice should still not be visible");
        Assert.False(Didomi.GetInstance().IsPreferencesVisible(), "Preferences screen should not be visible anymore");
        AssertEvents("Called HidePreferences", expectPreferencesHidden: true);
    }

    private void EventListener_ShowNotice(object sender, ShowNoticeEvent e)
    {
        noticeDisplayedEvent = true;
    }

    private void EventListener_HideNotice(object sender, HideNoticeEvent e)
    {
        noticeHiddenEvent = true;
    }

    private void EventListener_ShowPreferences(object sender, ShowPreferencesEvent e)
    {
        preferencesDisplayedEvent = true;
    }

    private void EventListener_HidePreferences(object sender, HidePreferencesEvent e)
    {
        preferencesHiddenEvent = true;
    }

    private void ResetEvents()
    {
        noticeDisplayedEvent = false;
        noticeHiddenEvent = false;
        preferencesDisplayedEvent = false;
        preferencesHiddenEvent = false;
    }

    private IEnumerator SetupUIWithoutNotice()
    {
        Didomi.GetInstance().SetUserAgreeToAll();
        Didomi.GetInstance().SetupUI();
        yield return new WaitForSeconds(1);
        Assert.False(Didomi.GetInstance().IsNoticeVisible(), "Notice should not be visible at startup");
        Didomi.GetInstance().Reset();
    }

    /**
     * Checks if events were called as expected
     */ 
    private void AssertEvents(
        string message,
        bool expectNoticeDisplayed = false,
        bool expectNoticeHidden = false,
        bool expectPreferencesDisplayed = false,
        bool expectPreferencesHidden = false)
    {
        Assert.AreEqual(expectNoticeDisplayed, noticeDisplayedEvent, $"{message} - Issue with ShowNotice event");
        Assert.AreEqual(expectNoticeHidden, noticeHiddenEvent, $"{message} - Issue with HideNotice event");
        Assert.AreEqual(expectPreferencesDisplayed, preferencesDisplayedEvent, $"{message} - Issue with ShowPreferences event");
        Assert.AreEqual(expectPreferencesHidden, preferencesHiddenEvent, $"{message} - Issue with HidePreferences event");
    }
}
