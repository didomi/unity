using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using UnityEngine;
using IO.Didomi.SDK.Events;

/// <summary>
/// Tests related to lists of purpose and vendor loaded into SDK
/// </summary>
public class PurposeAndVendorListsTestsSuite: DidomiBaseTests
{
    private bool consentChanged = false;

    [OneTimeSetUp]
    protected void SetUpSuite()
    {
        var listener = new DidomiEventListener();
        listener.ConsentChanged += EventListener_ConsentChanged;
        Didomi.GetInstance().AddEventListener(listener);
    }

    [UnitySetUp]
    public IEnumerator Setup()
    {
        yield return LoadSdk();
    }

    [TearDown]
    public void TearDown()
    {
        consentChanged = false;
    }

    [Test]
    public void TestPurposesAndVendorsCountAfterReset()
    {
        AssertHasRequiredPurposesAndVendors(true);
    }

    [UnityTest]
    public IEnumerator TestPurposesAndVendorsCountAfterUserAgreeToAll()
    {
        Didomi.GetInstance().SetUserAgreeToAll();
        yield return new WaitUntil(() => consentChanged);

        AssertHasRequiredPurposesAndVendors(true);
    }

    [UnityTest]
    public IEnumerator TestPurposesAndVendorsCountAfterUserDisagreeToAll()
    {
        Didomi.GetInstance().SetUserDisagreeToAll();
        yield return new WaitUntil(() => consentChanged);

        AssertHasRequiredPurposesAndVendors(true);
    }

    /// <summary>
    /// Check required purposes and vendors
    /// </summary>
    /// <param name="hasRequiredElements">Whether the required vendors and purposes list should be populated</param>
    private void AssertHasRequiredPurposesAndVendors(bool hasRequiredElements)
    {
        AssertEmptiness(Didomi.GetInstance().GetRequiredPurposeIds(), hasRequiredElements, "requiredPurposeIds");
        AssertEmptiness(Didomi.GetInstance().GetRequiredVendorIds(), hasRequiredElements, "requiredVendorIds");
    }

    /// <summary>
    /// Check content of a set
    /// </summary>
    /// <param name="element">The tested set</param>
    /// <param name="expectContent">Whether the set should have any element</param>
    /// <param name="checkedElement">Name of the checked element, in case an error message is printed</param>
    private void AssertEmptiness<T>(ISet<T> element, bool expectContent, string checkedElement)
    {
        Assert.NotNull(element, message: $"{checkedElement} is null");
        Assert.AreEqual(expectContent, element.Count > 0, message: $"{checkedElement} count = {element.Count}, expected content? {expectContent}");
    }

    private void EventListener_ConsentChanged(object sender, ConsentChangedEvent e)
    {
        consentChanged = true;
    }
}
