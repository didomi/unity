using System.Collections;
using NUnit.Framework;
using UnityEngine;
using IO.Didomi.SDK;
using IO.Didomi.SDK.Events;
using System;

/// <summary>
/// Base for tests related to user status synchronization between platforms
/// </summary>
public abstract class SyncUserBaseTests : DidomiBaseTests
{
    protected const string testUserId = "d13e49f6255c8729cbb201310f49d70d65f365415a67f034b567b7eac962b944eda131376594ef5e23b025fada4e4259e953ceb45ea57a2ced7872c567e6d1fae8dcc3a9772ead783d8513032e77d3fd";

    private bool syncError = false;
    private string syncDoneUserId = null;
    private string syncReadyUserId = null;
    private Boolean? statusApplied = null;
    private Boolean? syncAcknowledged = null;
    private Boolean? syncAcknowledged2 = null;

    protected void SetUpSuite()
    {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        // For iOS, we need to fully reset the SDK between each test suite
        IO.Didomi.SDK.IOS.DidomiFramework.ResetDidomi();
        Didomi.GetInstance().AddEventListener(eventListener);
#endif

        eventListener.SyncDone += EventListener_SyncDone;
        eventListener.SyncReady += EventListener_SyncReady;
        eventListener.SyncError += EventListener_SyncError;
    }

    protected new void TearDown()
    {
        base.TearDown();
        ResetStatus();
        ResetResults();
    }

    protected void TearDownSuite()
    {
        eventListener.SyncDone -= EventListener_SyncDone;
        eventListener.SyncReady -= EventListener_SyncReady;
        eventListener.SyncError -= EventListener_SyncError;
    }

    protected void ResetStatus()
    {
        Didomi.GetInstance().ClearUser();
        Didomi.GetInstance().Reset();
    }

    private void EventListener_SyncDone(object sender, SyncDoneEvent e)
    {
        Debug.Log("Sync Done!");
        syncDoneUserId = e.getOrganizationUserId();
    }

    private void EventListener_SyncReady(object sender, SyncReadyEvent e)
    {
        Debug.Log("Sync Ready!");
        syncReadyUserId = e.GetOrganizationUserId();
        statusApplied = e.IsStatusApplied();
        syncAcknowledged = e.SyncAcknowledged();
        syncAcknowledged2 = e.SyncAcknowledged();
    }

    private void EventListener_SyncError(object sender, SyncErrorEvent e)
    {
        Debug.LogFormat("Sync Error, message: {0}", e.getErrorMessage());
        syncError = true;
    }

    /// <summary>
    /// Reset the results so it can be checked again later
    /// </summary>
    protected void ResetResults()
    {
        syncError = false;
        syncDoneUserId = null;
        syncReadyUserId = null;
        statusApplied = null;
        syncAcknowledged = null;
        syncAcknowledged2 = null;
    }

    /// <summary>
    /// Check that user is synchronized successfully
    /// </summary>
    protected IEnumerator ExpectSyncSuccess(string message, bool expectApplied)
    {
        yield return ExpectSyncSuccess(message, expectApplied, expectApplied);
    }

    protected IEnumerator ExpectSyncSuccess(string message, bool expectApplied, bool expectAcknowledged)
    {
        yield return WaitForCallback();

        Assert.AreEqual(testUserId, syncDoneUserId, "Sync done User Id - " + message);
        Assert.AreEqual(testUserId, syncReadyUserId, "Sync ready User Id - " + message);
        Assert.IsFalse(syncError, "Sync error - " + message);
        Assert.AreEqual(expectApplied, statusApplied, "Status applied - " + message);
        Assert.AreEqual(expectAcknowledged, syncAcknowledged, "Sync acknowledged - " + message);
        Assert.IsFalse(syncAcknowledged2, "Sync acknowledged should always fail at the 2nd call - " + message);
    }

    /// <summary>
    /// Check that we get a sync error
    /// </summary>
    protected IEnumerator ExpectSyncError(string message)
    {
        yield return WaitForCallback();

        Assert.IsNull(syncDoneUserId, message);
        Assert.IsNull(syncReadyUserId, message);
        Assert.IsTrue(syncError, message);
        Assert.IsNull(statusApplied, message);
    }

    /// <summary>
    /// Wait for SyncDone or SyncError callback
    /// </summary>
    private IEnumerator WaitForCallback()
    {
        yield return new WaitUntil(() => (syncDoneUserId != null && syncAcknowledged != null && syncAcknowledged2 != null) || syncError);
    }
}
