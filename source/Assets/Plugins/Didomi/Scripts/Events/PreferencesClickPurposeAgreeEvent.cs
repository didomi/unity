namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Toggle to agree to a purpose on preferences popup
    /// </summary>
    public class PreferencesClickPurposeAgreeEvent : Event
    {
        private string purposeId;

        public PreferencesClickPurposeAgreeEvent(
            string purposeId
        )
        {
            this.purposeId = purposeId;
        }

        /// <summary>
        /// ID of the purpose
        /// </summary>
        /// <returns></returns>
        public string getPurposeId()
        {
            return purposeId;
        }
    }
}
