namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on agree to a vendor on preferences popup
    /// </summary>
    public class PreferencesClickVendorAgreeEvent : Event
    {

        /// <summary>
        /// ID of the vendor
        /// </summary>
        private string vendorId;

        /**
         * Constructor
         *
         * @param vendorId ID of the vendor
         */
        public PreferencesClickVendorAgreeEvent(
            string vendorId
        )
        {
            this.vendorId = vendorId;
        }

        public string getVendorId()
        {
            return vendorId;
        }
    }
}
