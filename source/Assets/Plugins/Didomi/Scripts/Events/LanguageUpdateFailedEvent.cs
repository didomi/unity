namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// The language update was not completed
    /// </summary>
    public class LanguageUpdateFailedEvent : Event
    {
        private string reason;

        public LanguageUpdateFailedEvent(
            string reason
        )
        {
            this.reason = reason;
        }

        /// <summary>
        /// Reason of the failure
        /// </summary>
        /// <returns></returns>
        public string getReason()
        {
            return reason;
        }
    }
}