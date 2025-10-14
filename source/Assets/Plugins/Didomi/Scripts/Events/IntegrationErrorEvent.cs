using System;

namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// An error occurred during Didomi SDK integration
    /// </summary>
    public class IntegrationErrorEvent : Event
    {
        private string integrationName;
        private string reason;

        public IntegrationErrorEvent(
            string integrationName,
            string reason
        )
        {
            this.integrationName = integrationName;
            this.reason = reason;
        }

        /// <summary>
        /// Name of the integration that failed
        /// </summary>
        /// <returns></returns>
        public string getIntegrationName()
        {
            return integrationName;
        }

        /// <summary>
        /// Reason for the integration failure
        /// </summary>
        /// <returns></returns>
        public string getReason()
        {
            return reason;
        }
    }
}
