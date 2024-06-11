namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Toggle to disagree to a purpose on preferences popup
    /// </summary>
    public class PreferencesClickPurposeDisagreeEvent : Event
    {
        private string purposeId;

        public PreferencesClickPurposeDisagreeEvent(
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
