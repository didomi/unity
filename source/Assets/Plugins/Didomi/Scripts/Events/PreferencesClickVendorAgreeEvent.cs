namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Toggle to agree to a vendor on preferences popup
    /// </summary>
    public class PreferencesClickVendorAgreeEvent : Event
    {
        private string vendorId;

        public PreferencesClickVendorAgreeEvent(
            string vendorId
        )
        {
            this.vendorId = vendorId;
        }

        /// <summary>
        /// ID of the vendor
        /// </summary>
        public string getVendorId()
        {
            return vendorId;
        }
    }
}
