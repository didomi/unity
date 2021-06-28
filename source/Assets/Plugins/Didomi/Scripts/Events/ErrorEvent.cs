using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.Didomi.SDK.Events
{
    public class ErrorEvent : Event
    { 
        /**
         * ID of the purpose
         */
        private string errorMessage;

        /**
         * Constructor
         *
         * @param purposeId ID of the purpose
         */
        public ErrorEvent(
            string errorMessage
        )
        {
            this.errorMessage = errorMessage;
        }

        public string getErrorMessage()
        {
            return errorMessage;
        }
    }
}
