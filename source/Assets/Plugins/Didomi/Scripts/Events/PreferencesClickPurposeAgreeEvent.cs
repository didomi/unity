namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Toggle to agree to a purpose on preferences popup
    /// </summary>
    public class PreferencesClickPurposeAgreeEvent : Event
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
        public PreferencesClickPurposeAgreeEvent(
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
