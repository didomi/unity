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

    [Test]
    public void TestGetVendor()
    {
        // var requiredVendorSet = Didomi.GetInstance().GetRequiredVendors();
        // Assert.False(requiredVendorSet.Count == 0, "No required vendors found");

        var requiredVendorIdSet = Didomi.GetInstance().GetRequiredVendorIds();
        // Assert.AreEqual(requiredVendorIdSet.Count, requiredVendorSet.Count, "Mismatch between required vendors and required vendor IDs");

        var vendorId = requiredVendorIdSet.FirstOrDefault();
        Assert.False(string.IsNullOrEmpty(vendorId), "Invalid 1st vendor");

        var vendor = Didomi.GetInstance().GetVendor(vendorId);
        Assert.NotNull(vendor, "Can't find Vendor with ID: " + vendorId);

        var vendorName = vendor?.Name;
        Assert.False(string.IsNullOrEmpty(vendorName), "Invalid vendor name");

        // Assert.NotNull(requiredVendorSet.FirstOrDefault(obj => obj.Name == vendorName), "Vendor not found in required vendors list with name: " + vendorName);
    }
}
