
namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// An error occurred within the SDK
    /// </summary>
    public class ErrorEvent : Event
    { 
        /**
         * Description of the error
         */
        private string errorMessage;

        /**
         * Constructor
         *
         * @param errorMessage Description of the error
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
