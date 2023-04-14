namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on disagree to a purpose on sensitive personal information screen
    /// </summary>
    public class PreferencesClickSPIPurposeDisagreeEvent : Event
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
        public PreferencesClickSPIPurposeDisagreeEvent(
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
