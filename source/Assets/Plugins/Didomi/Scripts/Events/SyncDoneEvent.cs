namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Synchronization was done successfully
    /// </summary>
    public class SyncDoneEvent : Event
    {
        private string organizationUserId;

        public SyncDoneEvent(
            string organizationUserId
        )
        {
            this.organizationUserId = organizationUserId;
        }

        /// <summary>
        /// Organization User ID that was synced
        /// </summary>
        /// <returns></returns>
        public string getOrganizationUserId()
        {
            return organizationUserId;
        }
    }
}
