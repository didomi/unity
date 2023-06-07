using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using UnityEngine;
using IO.Didomi.SDK.Events;

/// <summary>
/// Tests related to SDK texts
/// </summary>
public class TranslationsTestsSuite: DidomiBaseTests
{
    private bool languageUpdated = false;

    [OneTimeSetUp]
    protected void SetUpSuite()
    {
        var listener = new DidomiEventListener();
        listener.LanguageUpdated += EventListener_LanguageUpdated;
        Didomi.GetInstance().AddEventListener(listener);
    }

    [TearDown]
    public void TearDown()
    {
        languageUpdated = false;
    }

    [UnityTest]
    public IEnumerator TestGetTexts()
    {
        yield return LoadSdk();

        var key = "notice.content.notice";
        var dict = Didomi.GetInstance().GetText(key);

        Assert.NotNull(dict, "No result");
        Assert.IsTrue(dict["en"].StartsWith("With your agreement"), $"Wrong english language caption: {dict["en"]}");
        Assert.IsTrue(dict["fr"].StartsWith("Avec votre consentement"), $"Wrong french language caption: {dict["fr"]}");
    }

    [UnityTest]
    public IEnumerator TestGetTranslatedTextEnglish()
    {
        yield return LoadSdk(languageCode: "en");

        var key = "notice.content.notice";
        var textEn = Didomi.GetInstance().GetTranslatedText(key);

        Assert.IsTrue(textEn.StartsWith("With your agreement"), $"Wrong caption: {textEn}");
    }

    [UnityTest]
    public IEnumerator TestGetTranslatedTextFrench()
    {
        yield return LoadSdk(languageCode: "fr");

        var key = "notice.content.notice";
        var textFr = Didomi.GetInstance().GetTranslatedText(key);

        Assert.IsTrue(textFr.StartsWith("Avec votre consentement"), $"Wrong caption: {textFr}");
    }

    [UnityTest]
    public IEnumerator TestUpdateSelectedLanguage()
    {
        yield return LoadSdk(languageCode: "en");

        Didomi.GetInstance().UpdateSelectedLanguage("fr");
        yield return new WaitUntil(() => languageUpdated);

        var key = "notice.content.notice";
        var textFr = Didomi.GetInstance().GetTranslatedText(key);

        Assert.IsTrue(textFr.StartsWith("Avec votre consentement"), $"Wrong caption: {textFr}");
    }

    private void EventListener_LanguageUpdated(object sender, LanguageUpdatedEvent e)
    {
        languageUpdated = true;
    }
}
