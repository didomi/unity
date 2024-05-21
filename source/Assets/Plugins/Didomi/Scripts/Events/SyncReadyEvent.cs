using System;

namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Synchronization was done successfully
    /// </summary>
    public class SyncReadyEvent : Event
    {
        public delegate bool SyncAcknowledgedCallback();

        /**
         * Whether the user status was applied
         */
        private bool statusApplied;

        /**
         * Callback that triggers a sync.acknowledged API event if needed. It returns true if the API event was sent successfully.
         */
        private SyncAcknowledgedCallback syncAcknowledgedCallback;

        public SyncReadyEvent(
            bool statusApplied,
            SyncAcknowledgedCallback syncAcknowledgedCallback
        )
        {
            this.statusApplied = statusApplied;
            this.syncAcknowledgedCallback = syncAcknowledgedCallback;
        }

        /**
         * Whether the user status was applied
         */
        public bool IsStatusApplied()
        {
            return statusApplied;
        }

        /**
         * Callback that triggers a sync.acknowledged API event if needed. 
         * It returns true if the API event was sent successfully.
         */
        public bool SyncAcknowledged()
        {
            return syncAcknowledgedCallback();
        }
    }
}
