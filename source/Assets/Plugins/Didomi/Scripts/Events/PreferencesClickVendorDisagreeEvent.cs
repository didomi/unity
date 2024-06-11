namespace IO.Didomi.SDK.Events
{
    /// <summary>
    ///  Toggle to disagree to a vendor on preferences popup
    /// </summary>
    public class PreferencesClickVendorDisagreeEvent : Event
    {
        private string vendorId;

        public PreferencesClickVendorDisagreeEvent(
            string vendorId
        )
        {
            this.vendorId = vendorId;
        }

        /// <summary>
        /// ID of the vendor
        /// </summary>
        /// <returns></returns>
        public string getVendorId()
        {
            return vendorId;
        }
    }
}
