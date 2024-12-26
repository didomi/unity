using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;

/// <summary>
/// Tests related to sharing consent with Webview / Web SDK
/// </summary>
public class ShareConsentsTestsSuite: DidomiBaseTests
{
    [UnitySetUp]
    public new IEnumerator Setup()
    {
        base.Setup();
        yield return LoadSdk();
    }

    [TearDown]
    public new void TearDown()
    {
        base.TearDown();
    }

    [Test]
    public void TestGetJavaScriptForWebView()
    {
        var jsString = Didomi.GetInstance().GetJavaScriptForWebView();
        Assert.IsTrue(jsString != null && jsString.Contains("window.didomiOnReady"), $"Wrong jsString: {jsString}");
    }
}
