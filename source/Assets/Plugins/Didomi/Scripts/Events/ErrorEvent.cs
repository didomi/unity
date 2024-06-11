
namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// An error occurred within the SDK
    /// </summary>
    public class ErrorEvent : Event
    { 
        private string errorMessage;

        public ErrorEvent(
            string errorMessage
        )
        {
            this.errorMessage = errorMessage;
        }

        /// <summary>
        /// Description of the error
        /// </summary>
        /// <returns></returns>
        public string getErrorMessage()
        {
            return errorMessage;
        }
    }
}
