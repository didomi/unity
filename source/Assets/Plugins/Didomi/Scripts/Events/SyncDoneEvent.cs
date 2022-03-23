namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Synchronization was done successfully
    /// </summary>
    public class SyncDoneEvent : Event
    {
        /**
         * Organization User ID that was synced
         */
        private string organizationUserId;

        /**
         * Constructor
         *
         * @param organizationUserId Organization User ID that was synced
         */
        public SyncDoneEvent(
            string organizationUserId
        )
        {
            this.organizationUserId = organizationUserId;
        }

        public string getOrganizationUserId()
        {
            return organizationUserId;
        }
    }
}
