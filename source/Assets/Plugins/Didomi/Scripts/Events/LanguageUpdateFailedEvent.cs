namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// The language update was not completed
    /// </summary>
    public class LanguageUpdateFailedEvent : Event
    {
        /**
         * Reason of the failure
         */
        private string reason;

        /**
         * Constructor
         *
         * @param languageCode Reason of the failure
         */
        public LanguageUpdateFailedEvent(
            string reason
        )
        {
            this.reason = reason;
        }

        public string getReason()
        {
            return reason;
        }
    }
}