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

    [UnitySetUp]
    public IEnumerator Setup()
    {
        yield return LoadSdk(languageCode: "en");
    }

    [TearDown]
    public void TearDown()
    {
        languageUpdated = false;
    }

    [Test]
    public void TestGetTexts()
    {
        var key = "notice.content.notice";
        var dict = Didomi.GetInstance().GetText(key);

        Assert.NotNull(dict, "No result");
        Assert.IsTrue(dict["en"].StartsWith("With your agreement"), $"Wrong english language caption: {dict["en"]}");
        Assert.IsTrue(dict["fr"].StartsWith("Avec votre consentement"), $"Wrong french language caption: {dict["fr"]}");
    }

    [Test]
    public void TestGetTranslatedText()
    {
        var key = "notice.content.notice";
        var textEn = Didomi.GetInstance().GetTranslatedText(key);

        Assert.IsTrue(textEn.StartsWith("With your agreement"), $"Wrong caption: {textEn}");
    }

    [UnityTest]
    public IEnumerator TestUpdateSelectedLanguage()
    {
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
