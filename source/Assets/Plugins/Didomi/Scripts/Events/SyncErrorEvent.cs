
namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// An error occurred while trying to synchronize
    /// </summary>
    public class SyncErrorEvent : Event
    {
        private string errorMessage;

        public SyncErrorEvent(
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
