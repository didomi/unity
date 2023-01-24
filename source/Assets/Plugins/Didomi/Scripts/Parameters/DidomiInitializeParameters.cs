namespace IO.Didomi.SDK
{
    public class DidomiInitializeParameters
    {
        public string apiKey { get; }

        public string localConfigurationPath { get; }

        public string remoteConfigurationUrl { get; }

        public string providerId { get; }

        public bool disableDidomiRemoteConfig { get; }

        public string languageCode { get; }

        public string noticeId { get; }

        public string tvNoticeId { get; }

        public bool androidTvEnabled { get; }

        public DidomiInitializeParameters(
            string apiKey,
            string localConfigurationPath = null,
            string remoteConfigurationUrl = null,
            string providerId = null,
            bool disableDidomiRemoteConfig = false,
            string languageCode = null,
            string noticeId = null,
            string tvNoticeId = null,
            bool androidTvEnabled = false
        ) {
            this.apiKey = apiKey;
            this.localConfigurationPath = localConfigurationPath;
            this.remoteConfigurationUrl = remoteConfigurationUrl;
            this.providerId = providerId;
            this.disableDidomiRemoteConfig = disableDidomiRemoteConfig;
            this.languageCode = languageCode;
            this.noticeId = noticeId;
            this.tvNoticeId = tvNoticeId;
            this.androidTvEnabled = androidTvEnabled;
        }
    }
}
