using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using UnityEngine;
using IO.Didomi.SDK.Events;

/// <summary>
/// Tests related to current user status getter and setter
/// </summary>
public class ApplicableRegulationTestsSuite: DidomiBaseTests
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
    public void TestApplicableRegulation()
    {
        var result = Didomi.GetInstance().GetApplicableRegulation();
        Assert.AreEqual("gdpr", result);
    }

}
