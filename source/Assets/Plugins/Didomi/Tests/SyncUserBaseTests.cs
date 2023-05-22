using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using IO.Didomi.SDK;
using IO.Didomi.SDK.Events;

/// <summary>
/// Base for tests related to sharing consent with Webview / Web SDK
/// </summary>
public abstract class SyncUserBaseTests : DidomiBaseTests
{
    protected const string testUserId = "d13e49f6255c8729cbb201310f49d70d65f365415a67f034b567b7eac962b944eda131376594ef5e23b025fada4e4259e953ceb45ea57a2ced7872c567e6d1fae8dcc3a9772ead783d8513032e77d3fd";

    private bool syncError = false;
    private string syncedUserId = null;

    protected void SetUpSuite()
    {
        var listener = new DidomiEventListener();
        listener.SyncDone += EventListener_SyncDone;
        listener.SyncError += EventListener_SyncError;
        Didomi.GetInstance().AddEventListener(listener);
    }

    protected void TearDown()
    {
        ResetResults();
        Didomi.GetInstance().ClearUser();
        Didomi.GetInstance().Reset();
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

    /**
     * Reset the results so it can be checked again later
     */ 
    protected void ResetResults()
    {
        syncError = false;
        syncedUserId = null;
    }

    /**
     * Check that user is synchronized successfully
     */
    protected IEnumerator ExpectSyncSuccess(string message)
    {
        yield return WaitForCallback();

        Assert.AreEqual(testUserId, syncedUserId, message);
        Assert.IsFalse(syncError, message);
    }

    /**
     * Check that we get a sync error
     */
    protected IEnumerator ExpectSyncError()
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
