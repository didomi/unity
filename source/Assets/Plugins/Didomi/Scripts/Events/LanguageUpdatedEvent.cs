namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// The language update is complete
    /// </summary>
    public class LanguageUpdatedEvent : Event
    {
        private string languageCode;

        public LanguageUpdatedEvent(
            string languageCode
        )
        {
            this.languageCode = languageCode;
        }

        /// <summary>
        /// Selected language code
        /// </summary>
        /// <returns></returns>
        public string getLanguageCode()
        {
            return languageCode;
        }
    }
}