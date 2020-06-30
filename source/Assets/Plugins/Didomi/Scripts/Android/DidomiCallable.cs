using IO.Didomi.SDK.Events;
using System;
using UnityEngine;

namespace IO.Didomi.SDK.Android
{
    /// <summary>
    /// Event handler when the Didomi SDK is ready
    /// The `DidomiCallableProxy` is used to pass an event handler of type `DidomiCallable` to the Android SDK.
    /// This `AndroidJavaProxy` object maps to the <io.didomi.sdk.functionalinterfaces.DidomiCallable> interface from the Android SDK.
    /// </summary>
    public class DidomiCallable : AndroidJavaProxy
    {
        private Action _didomiCallable;

        public DidomiCallable(Action didomiCallable) : base("io.didomi.sdk.functionalinterfaces.DidomiCallable")
        {
            _didomiCallable = didomiCallable;
        }

        public void call()
        {
            _didomiCallable();
        }
    }
}
