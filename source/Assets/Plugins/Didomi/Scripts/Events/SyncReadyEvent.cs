using System;

namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Synchronization was done successfully
    /// </summary>
    public class SyncReadyEvent : Event
    {
        public delegate bool SyncAcknowledgedCallback();

        private string organizationUserId;
        private bool statusApplied;
        private SyncAcknowledgedCallback syncAcknowledgedCallback;

        public SyncReadyEvent(
            string organizationUserId,
            bool statusApplied,
            SyncAcknowledgedCallback syncAcknowledgedCallback
        )
        {
            this.organizationUserId = organizationUserId;
            this.statusApplied = statusApplied;
            this.syncAcknowledgedCallback = syncAcknowledgedCallback;
        }

        /// <summary>
        /// Organization User Id that was synced
        /// </summary>
        /// <returns></returns>
        public string GetOrganizationUserId()
        {
            return organizationUserId;
        } 

        /// <summary>
        /// Whether the user status was applied
        /// </summary>
        /// <returns></returns>
        public bool IsStatusApplied()
        {
            return statusApplied;
        }

        /// <summary>
        /// Callback that triggers a sync.acknowledged API event if needed. 
        /// </summary>
        /// <returns><c>true</c> if the API event was sent successfully.</returns>
        public bool SyncAcknowledged()
        {
            return syncAcknowledgedCallback();
        }
    }
}
