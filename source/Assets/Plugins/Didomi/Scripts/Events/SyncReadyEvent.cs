using System;

namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Synchronization was done successfully
    /// </summary>
    public class SyncReadyEvent : Event
    {
        public delegate bool SyncAcknowledgedCallback();

        private bool statusApplied;
        private SyncAcknowledgedCallback syncAcknowledgedCallback;

        public SyncReadyEvent(
            bool statusApplied,
            SyncAcknowledgedCallback syncAcknowledgedCallback
        )
        {
            this.statusApplied = statusApplied;
            this.syncAcknowledgedCallback = syncAcknowledgedCallback;
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
