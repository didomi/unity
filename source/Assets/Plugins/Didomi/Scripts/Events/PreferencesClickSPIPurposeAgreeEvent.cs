namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on agree to a purpose on sensitive personal information screen
    /// </summary>
    public class PreferencesClickSPIPurposeAgreeEvent : Event
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
        public PreferencesClickSPIPurposeAgreeEvent(
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
