using System.Collections;
using System.Linq;
using IO.Didomi.SDK;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PurposeAndVendorTestsSuite: DidomiBaseTests
{

    [UnitySetUp]
    public IEnumerator Setup()
    {
        yield return LoadSdk();
    }

    [Test]
    public void TestGetPurpose()
    {
        // Purpose model not available on iOS
        if (Application.platform == RuntimePlatform.Android)
        {
            var purposeId = GetFirstRequiredPurposeId();
            Assert.False(string.IsNullOrEmpty(purposeId));

            var purpose = Didomi.GetInstance().GetPurpose(purposeId);
            Assert.AreEqual(purposeId, purpose?.GetId());
        }
        else
        {
            Assert.Inconclusive("Test only valid for Android platform");
        }
    }

    [Test]
    public void TestGetVendor()
    {
        // Vendor model not available on iOS
        if (Application.platform == RuntimePlatform.Android)
        {
            var vendorId = GetFirstRequiredVendorId();
            Assert.False(string.IsNullOrEmpty(vendorId));
            
            var vendor = Didomi.GetInstance().GetVendor(vendorId);
            Assert.AreEqual(vendorId, vendor?.GetId());
        }
        else
        {
            Assert.Inconclusive("Test only valid for Android platform");
        }
    }

    private string GetFirstRequiredPurposeId()
    {
        var requiredPurposeIdSet = Didomi.GetInstance().GetRequiredPurposeIds();

        var purposeId = string.Empty;
        if (requiredPurposeIdSet.Count > 0)
        {
            purposeId = requiredPurposeIdSet.FirstOrDefault();
        }

        return purposeId;
    }

    private string GetFirstRequiredVendorId()
    {
        var requiredVendorIdSet = Didomi.GetInstance().GetRequiredVendorIds();

        var vendorId = string.Empty;
        if (requiredVendorIdSet.Count > 0)
        {
            vendorId = requiredVendorIdSet.FirstOrDefault();
        }

        return vendorId;
    }
}
