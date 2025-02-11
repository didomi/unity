using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using System;

/// <summary>
/// Tests related to user status synchronization between platforms after SDK was initialized
/// </summary>
public class SyncUserAfterInitTestsSuite : SyncUserBaseTests
{
    private const string syncUserId1 = "d13e49f6255c8729cbb201310f49d70d65f365415a67f034b567b7eac962b944eda131376594ef5e23b025fada4e4259e953ceb45ea57a2ced7872c567e6d1fae8dcc3a9772ead783d8513032e77d3f1";
    private const string syncUserId2 = "d13e49f6255c8729cbb201310f49d70d65f365415a67f034b567b7eac962b944eda131376594ef5e23b025fada4e4259e953ceb45ea57a2ced7872c567e6d1fae8dcc3a9772ead783d8513032e77d3f2";

    private const string secretId = "testsdks-PEap2wBx";
    private const string digest = "test-digest";
    private const string salt = "test-salt";
    private const string initializationVector = "3ff223854400259e5592cbb992be93cf";

    [OneTimeSetUp]
    public new void SetUpSuite()
    {
        base.SetUpSuite();
    }

    [UnitySetUp]
    public new IEnumerator Setup()
    {
        base.Setup();
        yield return LoadSdk();
        ResetStatus();
        ResetResults();
    }

    [TearDown]
    public new void TearDown()
    {
        base.TearDown();
    }

    [OneTimeTearDown]
    public new void TearDownSuite()
    {
        base.TearDownSuite();
    }

    [UnityTest]
    public IEnumerator TestSyncWithIncorrectParameters()
    {
        Didomi.GetInstance().SetUser(new DidomiUserParameters(
            new UserAuthWithEncryptionParams(
                id: "invalid-user",
                algorithm: "hash-md5",
                secretId: secretId,
                initializationVector: initializationVector,
                expiration: 3_600
            )
        ));

        yield return ExpectSyncError("Incorrect parameters");
    }

    [Test]
    public void TestForceUserAuthParam()
    {
        Assert.That(() => new DidomiUserParameters(null), 
                  Throws.TypeOf<ArgumentNullException>());
    }

    [UnityTest]
    public IEnumerator TestSyncUser()
    {
        Didomi.GetInstance().SetUser(new DidomiUserParameters(new UserAuthWithoutParams(testUserId)));
        yield return ExpectSyncSuccess("Set user with id", true);

        ResetResults();

        Didomi.GetInstance().SetUserAndSetupUI(new DidomiUserParameters(new UserAuthWithoutParams(testUserId)));
        yield return ExpectSyncSuccess("Set user with id and setup UI", false);

        ResetResults();

        Didomi.GetInstance().SetUser(new DidomiUserParameters(
            new UserAuthWithEncryptionParams(
                id: testUserId,
                algorithm: "aes-256-cbc",
                secretId: secretId,
                initializationVector: initializationVector,
                expiration: 3_600
            )
        ));
        yield return ExpectSyncError("Set user with Encryption params with expiration");

        ResetResults();

        Didomi.GetInstance().SetUser(new DidomiUserParameters(
            new UserAuthWithEncryptionParams(
                id: testUserId,
                algorithm: "aes-256-cbc",
                secretId: secretId,
                initializationVector: initializationVector
            )
        ));
        yield return ExpectSyncError("Set user with Encryption params without expiration");

        ResetResults();

        Didomi.GetInstance().SetUserAndSetupUI(new DidomiUserParameters(
            new UserAuthWithEncryptionParams(
                id: testUserId,
                algorithm: "aes-256-cbc",
                secretId: secretId,
                initializationVector: initializationVector
            )
        ));
        yield return ExpectSyncError("Set user with Encryption params and setup UI");

        ResetResults();

        Didomi.GetInstance().SetUser(new DidomiUserParameters(
            new UserAuthWithHashParams(
                id: testUserId,
                algorithm: "hash-md5",
                secretId: secretId,
                digest: digest,
                salt: salt,
                expiration: 3_600
            )
        ));
        yield return ExpectSyncError("Set user with Hash params with salt and expiration");

        ResetResults();

        Didomi.GetInstance().SetUser(new DidomiUserParameters(
            new UserAuthWithHashParams(
                id: testUserId,
                algorithm: "hash-md5",
                secretId: secretId,
                digest: digest,
                salt: null,
                expiration: 3_600
            )
        ));
        yield return ExpectSyncError("Set user with Hash params with expiration and without salt");

        ResetResults();

        Didomi.GetInstance().SetUser(new DidomiUserParameters(
            new UserAuthWithHashParams(
                id: testUserId,
                algorithm: "hash-md5",
                secretId: secretId,
                digest: digest,
                salt: null
            )
        ));
        yield return ExpectSyncError("Set user with Hash params without expiration and without salt");

        ResetResults();

        Didomi.GetInstance().SetUserAndSetupUI(new DidomiUserParameters(
            new UserAuthWithHashParams(
                id: testUserId,
                algorithm: "hash-md5",
                secretId: secretId,
                digest: digest,
                salt: salt,
                expiration: 3_600
            )
        ));
        yield return ExpectSyncError("Set user with Hash params with salt and expiration and setup UI");
    }

    [UnityTest]
    public IEnumerator TestSyncUserWithAdditionalParameters()
    {
        Didomi.GetInstance().SetUser(new DidomiUserParameters(
            userAuth: new UserAuthWithEncryptionParams(
                id: testUserId,
                algorithm: "aes-256-cbc",
                secretId: secretId,
                initializationVector: initializationVector,
                expiration: 3_600
            ),
            dcsUserAuth: new UserAuthWithEncryptionParams(
                id: testUserId,
                algorithm: "aes-256-cbc",
                secretId: secretId,
                initializationVector: initializationVector,
                expiration: 3_600
            ),
            isUnderage: true
        ));
        yield return ExpectSyncError("Set user with Encryption params, with dcsUser / isUnderage");

        ResetResults();

        Didomi.GetInstance().SetUser(new DidomiUserParameters(
            new UserAuthWithHashParams(
                id: testUserId,
                algorithm: "hash-md5",
                secretId: secretId,
                digest: digest,
                salt: salt
            ),
            new UserAuthWithHashParams(
                id: testUserId,
                algorithm: "hash-md5",
                secretId: secretId,
                digest: digest,
                salt: salt
            )
        ));
        yield return ExpectSyncError("Set user with Hash params, with dcsUser");

        ResetResults();

        Didomi.GetInstance().SetUser(new DidomiUserParameters(
            userAuth: new UserAuthWithEncryptionParams(
                id: testUserId,
                algorithm: "aes-256-cbc",
                secretId: secretId,
                initializationVector: initializationVector,
                expiration: 3_600
            ),
            isUnderage: true
        ));
        yield return ExpectSyncError("Set user with Encryption params, with isUnderage");
    }

    [UnityTest]
    public IEnumerator TestSyncWithSynchronizedUsers()
    {
        IList<UserAuthParams> synchronizedUsers = new List<UserAuthParams>
        {
            new UserAuthWithHashParams(
                id: syncUserId1,
                algorithm: "hash-md5",
                secretId: secretId,
                digest: digest,
                salt: salt
            ),
            new UserAuthWithEncryptionParams(
                id: syncUserId2,
                algorithm: "aes-256-cbc",
                secretId: secretId,
                initializationVector: initializationVector
            )
        };

        Didomi.GetInstance().SetUser(new DidomiMultiUserParameters(
            userAuth: new UserAuthWithEncryptionParams(
                id: testUserId,
                algorithm: "aes-256-cbc",
                secretId: secretId,
                initializationVector: initializationVector,
                expiration: 3_600
            ),
            synchronizedUsers: synchronizedUsers
        ));
        yield return ExpectSyncError("Synchronized users with encryption");

        ResetResults();

        Didomi.GetInstance().SetUser(new DidomiMultiUserParameters(
            userAuth: new UserAuthWithHashParams(
                id: testUserId,
                algorithm: "hash-md5",
                secretId: secretId,
                digest: digest,
                salt: salt,
                expiration: 3_600
            ),
            dcsUserAuth: new UserAuthWithHashParams(
                id: testUserId,
                algorithm: "hash-md5",
                secretId: secretId,
                digest: digest,
                salt: salt,
                expiration: 3_600
            ),
            synchronizedUsers: synchronizedUsers,
            isUnderage: false
        ));
        yield return ExpectSyncError("Synchronized users with hash, with dcs / underage");

        ResetResults();

        Didomi.GetInstance().SetUserAndSetupUI(new DidomiMultiUserParameters(
            userAuth: new UserAuthWithHashParams(
                id: testUserId,
                algorithm: "hash-md5",
                secretId: secretId,
                digest: digest,
                salt: salt,
                expiration: 3_600
            ),
            synchronizedUsers: synchronizedUsers,
            isUnderage: true
        ));
        yield return ExpectSyncError("Synchronized users with SetupUI, with underage");
    }

    [UnityTest]
    [Obsolete]
    public IEnumerator TestLegacySync()
    {
        Didomi.GetInstance().SetUser(testUserId);
        yield return ExpectSyncSuccess("Set user with id", true);

        ResetResults();

        IList<UserAuthParams> synchronizedUsers = new List<UserAuthParams>
        {
            new UserAuthWithHashParams(
                id: syncUserId1,
                algorithm: "hash-md5",
                secretId: secretId,
                digest: digest,
                salt: salt
            ),
            new UserAuthWithEncryptionParams(
                id: syncUserId2,
                algorithm: "aes-256-cbc",
                secretId: secretId,
                initializationVector: initializationVector
            )
        };

        Didomi.GetInstance().SetUser(
            new UserAuthWithEncryptionParams(
                id: testUserId,
                algorithm: "aes-256-cbc",
                secretId: secretId,
                initializationVector: initializationVector,
                expiration: 3_600
            ),
            synchronizedUsers
        );
        yield return ExpectSyncError("Synchronized users with encryption");
    }
}
