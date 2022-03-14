
namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// An error occurred while trying to synchronize
    /// </summary>
    public class SyncErrorEvent : Event
    {
        /**
         * description of the error
         */
        private string errorMessage;

        /**
         * Constructor
         *
         * @param errorMessage description of the error
         */
        public SyncErrorEvent(
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
