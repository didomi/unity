namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on disagree to a purpose on preferences popup
    /// </summary>
    public class PreferencesClickPurposeDisagreeEvent : Event
    {

        /**
         * ID of the purpose
         */
        private string purposeId;

        /**
         * Constructor
         *
         * @param purposeId ID of the purpose
         */
        public PreferencesClickPurposeDisagreeEvent(
            string purposeId
        )
        {
            this.purposeId = purposeId;
        }

        public string getPurposeId()
        {
            return purposeId;
        }
    }
}
