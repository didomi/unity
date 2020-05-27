using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.Didomi.SDK.Events
{
    public class DidomiCallable
    {
        public event EventHandler OnReady;

        public virtual void OnCall()
        {
            OnReady?.Invoke(this, EventArgs.Empty);
        }
    }
}
