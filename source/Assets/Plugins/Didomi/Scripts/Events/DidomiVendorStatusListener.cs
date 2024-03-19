using System;

namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Base class for vendor status listeners passed to our SDK
    /// </summary>
    public class DidomiVendorStatusListener
    {
        public event EventHandler<CurrentUserStatus.VendorStatus> VendorStatusChanged;

        public DidomiVendorStatusListener() { }

        public void OnVendorStatusChanged(CurrentUserStatus.VendorStatus @vendorStatus)
        {
            VendorStatusChanged?.Invoke(this, vendorStatus);
            // The vendor status has changed
        }
    }
}
