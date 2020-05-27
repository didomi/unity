using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace IO.Didomi.SDK.Events
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
