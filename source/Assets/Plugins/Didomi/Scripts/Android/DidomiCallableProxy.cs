using IO.Didomi.SDK.Events;
using System;
using UnityEngine;

namespace IO.Didomi.SDK.Android
{
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
