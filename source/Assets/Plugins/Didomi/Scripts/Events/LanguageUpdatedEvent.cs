namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// The language update is complete
    /// </summary>
    public class LanguageUpdatedEvent : Event
    {
        /**
         * Selected language code after the update
         */
        private string languageCode;

        /**
         * Constructor
         *
         * @param languageCode Selected language code
         */
        public LanguageUpdatedEvent(
            string languageCode
        )
        {
            this.languageCode = languageCode;
        }

        public string getLanguageCode()
        {
            return languageCode;
        }
    }
}