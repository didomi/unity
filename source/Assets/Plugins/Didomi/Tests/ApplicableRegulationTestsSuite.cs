using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;

/// <summary>
/// Tests related to regulation
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
