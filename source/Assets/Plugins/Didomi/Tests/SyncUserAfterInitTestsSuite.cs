using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;

/// <summary>
/// Tests related to sharing consent with Webview / Web SDK
/// </summary>
public class SyncUserAfterInitTestsSuite : SyncUserBaseTests
{

    [OneTimeSetUp]
    public new void SetUpSuite()
    {
        base.SetUpSuite();
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
    public new void TearDown()
    {
        base.TearDown();
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
}
