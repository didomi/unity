using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using UnityEngine;
using IO.Didomi.SDK.Events;

/// <summary>
/// Tests related to the widgets APIs.
/// The test notice has no widget configured, so the calls are expected to be ignored by the native SDKs.
/// </summary>
public class WidgetsTestsSuite: DidomiBaseTests
{
    private bool widgetDisplayedEvent = false;
    private bool widgetHiddenEvent = false;

    [OneTimeSetUp]
    protected void SetUpSuite()
    {
        eventListener.ShowWidget += EventListener_ShowWidget;
        eventListener.HideWidget += EventListener_HideWidget;
    }

    [UnitySetUp]
    public new IEnumerator Setup()
    {
        base.Setup();
        yield return LoadSdk();
    }

    [OneTimeTearDown]
    protected void TearDownSuite()
    {
        eventListener.ShowWidget -= EventListener_ShowWidget;
        eventListener.HideWidget -= EventListener_HideWidget;
    }

    [TearDown]
    public new void TearDown()
    {
        base.TearDown();
        widgetDisplayedEvent = false;
        widgetHiddenEvent = false;
    }

    [Test]
    public void TestIsWidgetVisibleWithoutWidget()
    {
        Assert.False(Didomi.GetInstance().IsWidgetVisible(), "No widget should be visible");
        Assert.False(Didomi.GetInstance().IsWidgetVisible("widget-id"), "Widget should not be visible");
    }

    [UnityTest]
    public IEnumerator TestShowWidgetWithoutWidget()
    {
        Didomi.GetInstance().ShowWidget();
        Didomi.GetInstance().ShowWidget(new DidomiWidgetParameters("widget-id", "purposes"));
        yield return new WaitForSeconds(1);

        Assert.False(Didomi.GetInstance().IsWidgetVisible(), "No widget should be visible");
        Assert.False(widgetDisplayedEvent, "ShowWidget event should not be triggered");
    }

    [UnityTest]
    public IEnumerator TestHideWidgetWithoutWidget()
    {
        Didomi.GetInstance().HideWidget();
        yield return new WaitForSeconds(1);

        Assert.False(Didomi.GetInstance().IsWidgetVisible(), "No widget should be visible");
        Assert.False(widgetHiddenEvent, "HideWidget event should not be triggered");
    }

    private void EventListener_ShowWidget(object sender, ShowWidgetEvent e)
    {
        widgetDisplayedEvent = true;
    }

    private void EventListener_HideWidget(object sender, HideWidgetEvent e)
    {
        widgetHiddenEvent = true;
    }
}
