using IO.Didomi.SDK.Events;
using UnityEngine;

namespace IO.Didomi.SDK.Android
{
    public class DidomiCallableProxy : AndroidJavaProxy
    {
        private DidomiCallable _didomiCallable;

        public DidomiCallableProxy(DidomiCallable didomiCallable) : base("io.didomi.sdk.functionalinterfaces.DidomiCallable")
        {
            _didomiCallable = didomiCallable;
        }

        public void call()
        {
            _didomiCallable.OnCall();
        }
    }
}
