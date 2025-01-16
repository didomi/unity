using System;

namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on disagree to a purpose on sensitive personal information screen
    /// </summary>
    [ObsoleteAttribute("SPI purposes are now treated as other purposes.Use PreferencesClickPurposeDisagreeEvent instead.")]
    public class PreferencesClickSPIPurposeDisagreeEvent : Event
    {
        private string purposeId;

        public PreferencesClickSPIPurposeDisagreeEvent(
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
