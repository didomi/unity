using IO.Didomi.SDK.Events;
using System;
using UnityEngine;

namespace IO.Didomi.SDK.Android
{
    /// <summary>
    /// Class used to represent an AndroidJavaProxy object for <io.didomi.sdk.functionalinterfaces.DidomiCallable> interface. 
    /// With DidomiCallableProxy we are able to give an event handler DidomiCallable to Didomi-Android-Sdk. 
    /// This handler is fired when Didomi SDK is ready. AndroidJavaProxy is a special class which enables you 
    /// to pass a handler to be called from Java to Unity C# methods.
    /// </summary>
    public class DidomiCallableProxy : AndroidJavaProxy
    {
        private Action _didomiCallable;

        public DidomiCallableProxy(Action didomiCallable) : base("io.didomi.sdk.functionalinterfaces.DidomiCallable")
        {
            _didomiCallable = didomiCallable;
        }

        public void call()
        {
            _didomiCallable();
        }
    }
}
