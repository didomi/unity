namespace IO.Didomi.SDK.Events
{
    /// <summary>
    ///  Click on disagree to a vendor on preferences popup
    /// </summary>
    public class PreferencesClickVendorDisagreeEvent : Event
    {

        /**
         * ID of the vendor
         */
        private string vendorId;

        /**
         * Constructor
         *
         * @param vendorId ID of the vendor
         */
        public PreferencesClickVendorDisagreeEvent(
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
