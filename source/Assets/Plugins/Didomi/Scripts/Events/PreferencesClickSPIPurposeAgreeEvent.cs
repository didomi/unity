namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on agree to a purpose on sensitive personal information screen
    /// </summary>
    public class PreferencesClickSPIPurposeAgreeEvent : Event
    {
        private string purposeId;

        public PreferencesClickSPIPurposeAgreeEvent(
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
