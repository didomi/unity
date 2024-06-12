using System;

namespace IO.Didomi.SDK
{
    /// <summary>
    /// Initialization parameters for Didomi SDK
    /// </summary>
    public class DidomiInitializeParameters
    {
        /// <summary>
        /// Your API key
        /// </summary>
        public string apiKey { get; }

        /// <summary>
        /// Path to client specific config file in the app assets in JSON format.
        /// By default, set as <c>didomi_config.json</c>.
        /// </summary>
        public string localConfigurationPath { get; }

        /// <summary>
        /// URL to client specific remote config file in JSON format.
        /// </summary>
        public string remoteConfigurationUrl { get; }

        /// <summary>
        /// Your provider ID (if any).
        /// </summary>
        public string providerId { get; }

        /// <summary>
        /// If set to true, disable remote configuration (only local config file will be used).
        /// </summary>
        [ObsoleteAttribute("In the future, it will be mandatory to create your notice from the console (see https://developers.didomi.io/cmp/mobile-sdk/android/setup#from-the-console-recommended for more information).")]
        public bool disableDidomiRemoteConfig { get; }

        /// <summary>
        /// Language in which the consent UI should be displayed.
        /// By default, the consent UI is displayed in the language configured in the device settings, if language is available and enabled by your configuration.
        /// This property allows you to override the default setting and specify a language to display the UI in.
        /// String containing the language code e.g.: "es", "fr", etc.
        /// </summary>
        public string languageCode { get; }

        /// <summary>
        /// ID of the notice configuration to load if you are not using app ID targeting (mobile devices).
        /// </summary>
        public string noticeId { get; }

        /// <summary>
        /// ID of the notice configuration to load on TV devices if you are not using app ID targeting.
        /// If <c>androidTvEnabled</c> is true and SDK is launched on a TV device, this parameter will be used instead of <c>noticeId</c>
        /// to get configuration from console.
        /// </summary>
        public string tvNoticeId { get; }

        /// <summary>
        /// If set to <c>true</c>, when launched on a AndroidTV device, the SDK will display TV notice.
        /// </summary>
        public bool androidTvEnabled { get; }

        /// <summary>
        /// Override user country code when determining the privacy regulation to apply.
        /// Keep null to let the Didomi SDK determine the user country.
        /// </summary>
        public string countryCode { get; }

        /// <summary>
        /// Override user region code when determining the privacy regulation to apply.
        /// Keep null to let the Didomi SDK determine the user region.
        /// Ignored if countryCode is not set.
        /// </summary>
        public string regionCode { get; }

        /// <summary>
        /// Initialization parameters for Didomi SDK
        /// </summary>
        /// <param name="apiKey">Your API key.</param>
        /// <param name="localConfigurationPath">Path to client specific config file in the app assets in JSON format.
        /// By default, set as <c>didomi_config.json</c>.</param>
        /// <param name="remoteConfigurationUrl">URL to client specific remote config file in JSON format.</param>
        /// <param name="providerId">Your provider ID (if any).</param>
        /// <param name="disableDidomiRemoteConfig">Obsolete. If set to true, disable remote configuration(only local config file will be used).</param>
        /// <param name="languageCode">Language in which the consent UI should be displayed.
        /// By default, the consent UI is displayed in the language configured in the device settings, if language is available and enabled by your configuration.
        /// This property allows you to override the default setting and specify a language to display the UI in.
        /// String containing the language code e.g.: "es", "fr", etc.</param>
        /// <param name="noticeId">ID of the notice configuration to load if your are not using app ID targeting(mobile devices and tvOS).</param>
        /// <param name="tvNoticeId">ID of the notice configuration to load on AndroidTV devices if you are not using app ID targeting.</param>
        /// <param name="androidTvEnabled">If set to <c>true</c>, when launched on a AndroidTV device, the SDK will display TV notice:
        /// <list type="bullet">
        /// <item>
        /// <description>Only Didomi remote config is allowed</description>
        /// </item>
        /// <item>
        /// <description>Connected TV must be enabled for your organization, and <c>apiKey</c> / <c>tvNoticeId</c> must correspond to a TV notice in the console.</description>
        /// </item>
        /// </list>
        /// If <c>false</c> or not set, the SDK will not be able to initialize on a TV device.</param>
        /// <param name="countryCode">Override user country code when determining the privacy regulation to apply.
        /// Keep null to let the Didomi SDK determine the user country.</param>
        /// <param name="regionCode">Override user region code when determining the privacy regulation to apply.
        /// Keep null to let the Didomi SDK determine the user region.
        /// Ignored if countryCode is not set.</param>
        public DidomiInitializeParameters(
            string apiKey,
            string localConfigurationPath = null,
            string remoteConfigurationUrl = null,
            string providerId = null,
            bool disableDidomiRemoteConfig = false,
            string languageCode = null,
            string noticeId = null,
            string tvNoticeId = null,
            bool androidTvEnabled = false,
            string countryCode = null,
            string regionCode = null
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
            this.countryCode = countryCode;
            this.regionCode = regionCode;
        }
    }
}
