using IO.Didomi.SDK.Events;
using System;
using UnityEngine;

namespace IO.Didomi.SDK.Android
{
    /// <summary>
    /// Vendor status listener
    /// Event handler is defined as empty functions so that apps can override it.
    /// The `DidomiVendorStatusListenerProxy` is used to pass an event handler of type `DidomiVendorStatusListener` to the Android SDK.
    /// This `AndroidJavaProxy` object maps to the <io.didomi.sdk.functionalinterfaces.DidomiVendorStatusListener> interface from the Android SDK.
    /// </summary>
    public class DidomiVendorStatusListenerProxy : AndroidJavaProxy
    {
        private DidomiVendorStatusListener _vendorStatusListener;

        public DidomiVendorStatusListenerProxy(DidomiVendorStatusListener vendorStatusListener) : base("io.didomi.sdk.functionalinterfaces.DidomiVendorStatusListener")
        {
            _vendorStatusListener = vendorStatusListener;
        }

        public void onVendorStatusChanged(AndroidJavaObject @status)
        {
            var vendorStatus = AndroidObjectMapper.ConvertToCurrentUserStatusVendor(@status);
            _vendorStatusListener.OnVendorStatusChanged(vendorStatus);
            // The status of the vendor has changed
        }
    }
}
