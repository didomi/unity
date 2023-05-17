using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using IO.Didomi.SDK.Events;

/// <summary>
/// Tests related to sharing consent with Webview / Web SDK
/// </summary>
public class SyncUserTestSuite : DidomiBaseTests
{
    private const string testUserId = "d13e49f6255c8729cbb201310f49d70d65f365415a67f034b567b7eac962b944eda131376594ef5e23b025fada4e4259e953ceb45ea57a2ced7872c567e6d1fae8dcc3a9772ead783d8513032e77d3fd";

    private bool syncError = false;
    private string syncedUserId = null;

    [OneTimeSetUp]
    public void SetUpSuite()
    {
        Debug.Log("Initialize...");
        var listener = new DidomiEventListener();
        listener.SyncDone += EventListener_SyncDone;
        listener.SyncError += EventListener_SyncError;
        Didomi.GetInstance().AddEventListener(listener);
    }

    [UnitySetUp]
    public IEnumerator Setup()
    {
        if (!Didomi.GetInstance().IsReady())
        {
            yield return LoadSdk();
        }
    }

    [TearDown]
    public void TearDown()
    {
        ResetResults();
        Didomi.GetInstance().ClearUser();
        Didomi.GetInstance().Reset();
    }

    [UnityTest]
    public IEnumerator TestSyncWithIncorrectParameters()
    {
        Didomi.GetInstance().SetUser(new UserAuthWithEncryptionParams(
            id: "invalid-user",
            algorithm: "hash-md5",
            secretId: "testsdks-PEap2wBx",
            initializationVector: "3ff223854400259e5592cbb992be93cf",
            expiration: 3_600
        ));

        yield return ExpectSyncError();
    }

    [UnityTest]
    public IEnumerator TestSyncUser()
    {
        Didomi.GetInstance().SetUser(testUserId);
        yield return ExpectSyncSuccess("Set user with id");

        ResetResults();

        Didomi.GetInstance().SetUserAndSetupUI(testUserId);
        yield return ExpectSyncSuccess("Set user with id and setup UI");

        ResetResults();

        Didomi.GetInstance().SetUser(new UserAuthWithEncryptionParams(
            id: testUserId,
            algorithm: "aes-256-cbc",
            secretId: "testsdks-PEap2wBx",
            initializationVector: "3ff223854400259e5592cbb992be93cf",
            expiration: 3_600
        ));
        yield return ExpectSyncSuccess("Set user with Encryption params with expiration");

        ResetResults();

        Didomi.GetInstance().SetUser(new UserAuthWithEncryptionParams(
            id: testUserId,
            algorithm: "aes-256-cbc",
            secretId: "testsdks-PEap2wBx",
            initializationVector: "3ff223854400259e5592cbb992be93cf"
        ));
        yield return ExpectSyncSuccess("Set user with Encryption params without expiration");

        ResetResults();

        Didomi.GetInstance().SetUserAndSetupUI(new UserAuthWithEncryptionParams(
            id: testUserId,
            algorithm: "aes-256-cbc",
            secretId: "testsdks-PEap2wBx",
            initializationVector: "3ff223854400259e5592cbb992be93cf"
        ));
        yield return ExpectSyncSuccess("Set user with Encryption params and setup UI");

        ResetResults();

        Didomi.GetInstance().SetUser(new UserAuthWithHashParams(
            id: testUserId,
            algorithm: "hash-md5",
            secretId: "testsdks-PEap2wBx",
            digest: "test-digest",
            salt: "test-salt",
            expiration: 3_600
        ));
        yield return ExpectSyncSuccess("Set user with Encryption params with salt and expiration");

        ResetResults();

        Didomi.GetInstance().SetUser(new UserAuthWithHashParams(
            id: testUserId,
            algorithm: "hash-md5",
            secretId: "testsdks-PEap2wBx",
            digest: "test-digest",
            salt: null,
            expiration: 3_600
        ));
        yield return ExpectSyncSuccess("Set user with Encryption params with expiration and without salt");

        ResetResults();

        Didomi.GetInstance().SetUserAndSetupUI(new UserAuthWithHashParams(
            id: testUserId,
            algorithm: "hash-md5",
            secretId: "testsdks-PEap2wBx",
            digest: "test-digest",
            salt: "test-salt",
            expiration: 3_600
        ));
        yield return ExpectSyncSuccess("Set user with Encryption params and setup UI");
    }

    private void EventListener_SyncDone(object sender, SyncDoneEvent e)
    {
        Debug.Log("Sync Done!");
        syncedUserId = e.getOrganizationUserId();
    }

    private void EventListener_SyncError(object sender, SyncErrorEvent e)
    {
        Debug.LogFormat("Sync Error, message: {0}", e.getErrorMessage());
        syncError = true;
    }

    private void ResetResults()
    {
        syncError = false;
        syncedUserId = null;
    }

    /**
     * Check that user is synchronized successfully
     */
    private IEnumerator ExpectSyncSuccess(string message)
    {
        yield return WaitForCallback();

        Assert.AreEqual(testUserId, syncedUserId, message);
        Assert.IsFalse(syncError, message);
    }

    /**
     * Check that we get a sync error
     */
    private IEnumerator ExpectSyncError()
    {
        yield return WaitForCallback();

        Assert.IsNull(syncedUserId);
        Assert.IsTrue(syncError);
    }

    /**
     * Wait for SyncDone or SyncError callback
     */
    private IEnumerator WaitForCallback()
    {
        yield return new WaitUntil(() => syncedUserId != null || syncError);
    }
}
