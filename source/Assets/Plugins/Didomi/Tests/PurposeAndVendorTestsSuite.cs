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
            var requiredPurposeSet = Didomi.GetInstance().GetRequiredPurposes();
            Assert.False(requiredPurposeSet.Count == 0, "No required purpose found");

            var requiredPurposeIdSet = Didomi.GetInstance().GetRequiredPurposeIds();
            Assert.AreEqual(requiredPurposeIdSet.Count, requiredPurposeSet.Count, "Mismatch between required purpose and required purpose IDs");

            var purposeId = requiredPurposeIdSet.FirstOrDefault();
            Assert.False(string.IsNullOrEmpty(purposeId), "Invalid 1st purpose");

            var purpose = Didomi.GetInstance().GetPurpose(purposeId);
            Assert.NotNull(purpose, "Can't find Purpose with ID: " + purposeId);

            Assert.AreEqual(purposeId, purpose?.Id, "Mismatch between purpose ID and purpose");
            Assert.False(string.IsNullOrEmpty(purpose?.Name), "Purpose name is null or empty");
            Assert.False(string.IsNullOrEmpty(purpose?.DescriptionText), "Purpose descriptionText is null or empty");

            Assert.NotNull(requiredPurposeSet.FirstOrDefault(obj => obj.Id == purposeId), "Purpose not found in required purposes list with id: " + purposeId);
        }
        else
        {
            Assert.Pass("Test only valid for Android platform: method not supported on iOS");
        }
    }

    [Test]
    public void TestGetVendor()
    {
        // Vendor model not available on iOS
        if (Application.platform == RuntimePlatform.Android)
        {
            var vendorId = GetFirstRequiredVendorId();
            Assert.False(string.IsNullOrEmpty(vendorId), "Invalid 1st vendor");
            
            var vendor = Didomi.GetInstance().GetVendor(vendorId);
            Assert.AreEqual(vendorId, vendor?.GetId());
        }
        else
        {
            Assert.Pass("Test only valid for Android platform: method not supported on iOS");
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
