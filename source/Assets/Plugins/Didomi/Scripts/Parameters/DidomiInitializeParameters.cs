namespace IO.Didomi.SDK
{
    /**
     * Initialization parameters for Didomi SDK
     * * [apiKey] Your API key.
     * * [localConfigurationPath] Path to client specific config file in the app assets in JSON format.
     * By default, set as didomi_config.json.
     * * [remoteConfigurationUrl] URL to client specific remote config file in JSON format.
     * * [providerId] Your provider ID (if any).
     * * [disableDidomiRemoteConfig] If set to true, disable remote configuration (only local config file will be used).
     * * [languageCode] Language in which the consent UI should be displayed.
     * By default, the consent UI is displayed in the language configured in the device settings, if language is available and enabled by your configuration.
     * This property allows you to override the default setting and specify a language to display the UI in.
     * String containing the language code e.g.: "es", "fr", etc.
     * * [providerId] Your provider ID (if any).
     * * [noticeId] ID of the notice configuration to load if your are not using app ID targeting (mobile devices).
     * * [tvNoticeId] ID of the notice configuration to load on TV devices if you are not using app ID targeting.
     * * [androidTvEnabled] If set to true, when launched on a AndroidTV device, the SDK will display TV notice.
    */
    public class DidomiInitializeParameters
    {
        /**
         * Your API key
         */
        public string apiKey { get; }

        /**
         * Path to client specific config file in the app assets in JSON format.
         * By default, set as didomi_config.json.
         */
        public string localConfigurationPath { get; }

        /**
         * URL to client specific remote config file in JSON format.
         */
        public string remoteConfigurationUrl { get; }

        /**
         * Your provider ID (if any).
         */
        public string providerId { get; }

        /**
         * If set to true, disable remote configuration (only local config file will be used).
         */
        [ObsoleteAttribute("In the future, it will be mandatory to create your notice from the console (see https://developers.didomi.io/cmp/mobile-sdk/android/setup#from-the-console-recommended for more information).")]
        public bool disableDidomiRemoteConfig { get; }

        /**
         * Language in which the consent UI should be displayed.
         * By default, the consent UI is displayed in the language configured in the device settings, if language is available and enabled by your configuration.
         * This property allows you to override the default setting and specify a language to display the UI in.
         * String containing the language code e.g.: "es", "fr", etc.
         */
        public string languageCode { get; }

        /**
         * ID of the notice configuration to load if you are not using app ID targeting (mobile devices).
         */
        public string noticeId { get; }

        /**
         * ID of the notice configuration to load on TV devices if you are not using app ID targeting.
         * If [androidTvEnabled] is true and SDK is launched on a TV device, this parameter will be used instead of [noticeId]
         * to get configuration from console.
         */
        public string tvNoticeId { get; }

        /**
         * If set to true, when launched on a AndroidTV device, the SDK will display TV notice:
         * * Only Didomi remote config is allowed
         * * Connected TV must be enabled for your organization, and [apiKey] / [tvNoticeId] must correspond to a TV notice in the console.
         *
         * If false or not set, the SDK will not be able to initialize on a TV device.
         */
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
