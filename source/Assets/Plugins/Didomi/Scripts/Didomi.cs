using IO.Didomi.SDK.Android;
using IO.Didomi.SDK.Events;
using IO.Didomi.SDK.Interfaces;
using IO.Didomi.SDK.IOS;
using IO.Didomi.SDK.UnityEditor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IO.Didomi.SDK
{
    /// <summary>
    /// Main class exposed to Unity apps.
    /// It gives access to a single instance of the Didomi plugin and will call
    /// the native SDKs as needed through platform-specific implementations
    /// (IOSDidomi, AndroidDidomi, UnityEditorDidomi).
    /// </summary>
    public class Didomi
    {
        private static Didomi didomiInstance;
        private static readonly object instanceLock = new object();
        private static IDidomi didomiForPlatform = null;

        /// <summary>
        /// Get the global singleton instance of the Didomi SDK
        /// </summary>
        /// <returns>Didomi</returns>
        public static Didomi GetInstance()
        {
            if (didomiInstance == null)
            {
                lock (instanceLock)
                {
                    if (didomiInstance == null)
                    {
                        if (Application.platform == RuntimePlatform.Android)
                        {
                            didomiForPlatform = new AndroidDidomi();
                        }
                        else if (Application.platform == RuntimePlatform.IPhonePlayer)
                        {
                            didomiForPlatform = new IOSDidomi();
                        }
                        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.OSXEditor)
                        {
                            didomiForPlatform = new UnityEditorDidomi();
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }

                        didomiInstance = new Didomi();
                    }

                    return didomiInstance;
                }
            }
            else
            {
                return didomiInstance;
            }
        }

        /// <summary>
        /// Add an event listener
        /// </summary>
        /// <param name="eventListener"></param>
        public void AddEventListener(DidomiEventListener eventListener)
        {
            didomiForPlatform.AddEventListener(eventListener);
        }

        /// <summary>
        /// Get the disabled purposes
        /// </summary>
        /// <returns></returns>
        public ISet<Purpose> GetDisabledPurposes()
        {
            return didomiForPlatform.GetDisabledPurposes();
        }

        /// <summary>
        /// Get the IDs of the disabled purposes
        /// </summary>
        /// <returns></returns>
        public ISet<string> GetDisabledPurposeIds()
        {
            return didomiForPlatform.GetDisabledPurposeIds();
        }

        /// <summary>
        /// Get the disabled vendors
        /// </summary>
        /// <returns></returns>
        public ISet<Vendor> GetDisabledVendors()
        {
            return didomiForPlatform.GetDisabledVendors();
        }

        /// <summary>
        /// Get the IDs of the disabled vendors
        /// </summary>
        /// <returns></returns>
        public ISet<string> GetDisabledVendorIds()
        {
            return didomiForPlatform.GetDisabledVendorIds();
        }

        /// <summary>
        /// Get the enabled purposes
        /// </summary>
        /// <returns></returns>
        public ISet<Purpose> GetEnabledPurposes()
        {
            return didomiForPlatform.GetEnabledPurposes();
        }

        /// <summary>
        /// Get the IDs of the enabled purposes
        /// </summary>
        /// <returns></returns>
        public ISet<string> GetEnabledPurposeIds()
        {
            return didomiForPlatform.GetEnabledPurposeIds();
        }

        /// <summary>
        /// Get the enabled vendors
        /// </summary>
        /// <returns></returns>
        public ISet<Vendor> GetEnabledVendors()
        {
            return didomiForPlatform.GetEnabledVendors();
        }

        /// <summary>
        /// Get the IDs of the enabled vendors
        /// </summary>
        /// <returns></returns>
        public ISet<string> GetEnabledVendorIds()
        {
            return didomiForPlatform.GetEnabledVendorIds();
        }

        /// <summary>
        /// Get JavaScript to embed into a WebView to pass the consent status from the app to the Didomi Web SDK embedded into the WebView
        /// Inject the returned tag into a WebView with `evaluateJavaScript`
        /// </summary>
        /// <returns></returns>
        public string GetJavaScriptForWebView()
        {
            return didomiForPlatform.GetJavaScriptForWebView();
        }

        /// <summary>
        /// Get a purpose from its ID
        /// </summary>
        /// <param name="purposeId"></param>
        /// <returns></returns>
        public Purpose GetPurpose(string purposeId)
        {
            return didomiForPlatform.GetPurpose(purposeId);
        }

        /// <summary>
        /// Get the configured purposes
        /// </summary>
        /// <returns></returns>
        public ISet<Purpose> GetRequiredPurposes()
        {
            return didomiForPlatform.GetRequiredPurposes();
        }

        /// <summary>
        /// Get the configured purpose IDs
        /// </summary>
        /// <returns></returns>
        public ISet<string> GetRequiredPurposeIds()
        {
            return didomiForPlatform.GetRequiredPurposeIds();
        }

        /// <summary>
        /// Get the configured vendors
        /// </summary>
        /// <returns></returns>
        public ISet<Vendor> GetRequiredVendors()
        {
            return didomiForPlatform.GetRequiredVendors();
        }

        /// <summary>
        /// Get the configured vendor IDs
        /// </summary>
        /// <returns></returns>
        public ISet<string> GetRequiredVendorIds()
        {
            return didomiForPlatform.GetRequiredVendorIds();
        }

        /// <summary>
        /// Method used to get a dictionary/map for a given key in the form of { en: "Value in English", fr: "Value in French" }
        /// from the didomi_config.json file containing translations in different languages.
        /// 
        /// The keys and values considered come from different places in the didomi_config.json file such as { notice: ... }, { preferences: ... } and
        /// { texts: ... }, giving the latter the highest priority in case of duplicates.
        /// </summary>
        /// <param name="key">string used as a key to find a dictionary/map that contains translation texts</param>
        /// <returns>a dictionary/map that contains the translation texts in different languages for the given key.</returns>
        public IDictionary<string, string> GetText(string key)
        {
            return didomiForPlatform.GetText(key);
        }

        /// <summary>
        /// Method used to get a translated string for a given key from the didomi_config.json file based on the currently selected language.
        /// 
        /// The keys and values considered come from different places in the didomi_config.json file such as { notice: ... }, { preferences: ... } and
        /// { texts: ... }, giving the latter the highest priority in case of duplicates.
        /// </summary>
        /// <param name="key">string used as a key to find a translated string.</param>
        /// <returns>a translated string based on the provided key and the selected language.</returns>
        public string GetTranslatedText(string key)
        {
            return didomiForPlatform.GetTranslatedText(key);
        }

        /// <summary>
        /// Get the user consent status for a specific purpose
        /// </summary>
        /// <param name="purposeId"></param>
        /// <returns></returns>
        public bool GetUserConsentStatusForPurpose(string purposeId)
        {
            return didomiForPlatform.GetUserConsentStatusForPurpose(purposeId);
        }

        /// <summary>
        /// Get the user consent status for a specific vendor
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        public bool GetUserConsentStatusForVendor(string vendorId)
        {
            return didomiForPlatform.GetUserConsentStatusForVendor(vendorId);
        }

        /// <summary>
        /// Check if a vendor has consent for all the purposes that it requires
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        public bool GetUserConsentStatusForVendorAndRequiredPurposes(string vendorId)
        {
            return didomiForPlatform.GetUserConsentStatusForVendorAndRequiredPurposes(vendorId);
        }

        /// <summary>
        /// Get a vendor from its ID
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        public Vendor GetVendor(string vendorId)
        {
            return didomiForPlatform.GetVendor(vendorId);
        }

        /// <summary>
        /// Hide the notice if it is currently displayed
        /// </summary>
        public void HideNotice()
        {
            didomiForPlatform.HideNotice();
        }

        /// <summary>
        /// Hide the preferences popup for purposes
        /// </summary>
        public void HidePreferences()
        {
            didomiForPlatform.HidePreferences();
        }

        /// <summary>
        /// The initial configuration of the SDK. This should be called from an Application onCreate method. (Method with disableDidomiRemoteConfig)
        /// </summary>
        /// <param name="apiKey">Your API key</param>
        /// <param name="localConfigurationPath">Path to client specific config file in JSON format. If not specified,
        /// by defalut is set as didomi_config.json in the app assets.</param>
        /// <param name="remoteConfigurationURL">URL to client specific remote config file in JSON format</param>
        /// <param name="providerId">Your provider ID (if any)</param>
        /// <param name="disableDidomiRemoteConfig">Used to disable remote configuration.</param>
        /// <param name="languageCode">Language in which the consent UI should be displayed. 
        /// By default, the consent UI is displayed in the language configured in the device settings,
        /// if langauge is availabe and enabled by your configuration. 
        /// This property allows you to override the default setting and specify a language to display the UI in.
        /// String containing the language code e.g.: "es", "fr", etc.</param>
        public void Initialize(
            string apiKey,
            string localConfigurationPath,
            string remoteConfigurationURL,
            string providerId,
            bool disableDidomiRemoteConfig,
            string languageCode=null
            )
        {
            didomiForPlatform.Initialize(
                apiKey,
                localConfigurationPath,
                remoteConfigurationURL,
                providerId,
                disableDidomiRemoteConfig,
                languageCode);
        }

        /// <summary>
        /// Determine if consent is required for the user. The rules are (OR):
        /// - The user country is in the EU
        /// - The company is from the EU
        /// - The user country is unknown and the app has chosen to collect consent when unknown
        /// </summary>
        /// <returns>True if consent is required, false if it is not required.</returns>
        public bool IsConsentRequired()
        {
            return didomiForPlatform.IsConsentRequired();
        }

        /// <summary>
        /// Determine if consent information is available for all purposes and vendors that are required
        /// </summary>
        /// <returns></returns>
        public bool IsUserConsentStatusPartial()
        {
            return didomiForPlatform.IsUserConsentStatusPartial();
        }

        /// <summary>
        /// Check if the consent notice is currently displayed
        /// </summary>
        /// <returns></returns>
        public bool IsNoticeVisible()
        {
            return didomiForPlatform.IsNoticeVisible();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsPreferencesVisible()
        {
            return didomiForPlatform.IsPreferencesVisible();
        }

        /// <summary>
        ///  Is the Didomi SDK ready?
        /// </summary>
        /// <returns></returns>
        public bool IsReady()
        {
            return didomiForPlatform.IsReady();
        }

        /// <summary>
        /// Provide a function that needs to be called once the SDK is ready, 
        /// Automatically call the function if the SDK is already ready(that's why this is not a standard event)
        /// </summary>
        /// <param name="didomiCallable">that is called once the SDK is ready.</param>
        public void OnReady(Action didomiCallable)
        {
            didomiForPlatform.OnReady(didomiCallable);
        }

        /// <summary>
        /// Provide the objects required to display UI elements
        /// </summary>
        public void SetupUI()
        {
            didomiForPlatform.SetupUI();
        }

        /// <summary>
        /// Show the consent notice (if required, not disabled in the config and not already displayed)
        /// </summary>
        public void ShowNotice()
        {
            didomiForPlatform.ShowNotice();
        }

        /// <summary>
        /// Show the preferences popup for purposes
        /// </summary>
        public void ShowPreferences()
        {
            didomiForPlatform.ShowPreferences();
        }

        /// <summary>
        /// Remove all consents for the user
        /// </summary>
        public void Reset()
        {
            didomiForPlatform.Reset();
        }

        /// <summary>
        /// Enable all purposes and vendors for the user
        /// </summary>
        /// <returns>true if user consent status was updated, false otherwise.</returns>
        public bool SetUserAgreeToAll()
        {
            return didomiForPlatform.SetUserAgreeToAll();
        }

        /// <summary>
        /// Set the user consent status
        /// </summary>
        /// <param name="enabledPurposeIds"></param>
        /// <param name="disabledPurposeIds"></param>
        /// <param name="enabledVendorIds"></param>
        /// <param name="disabledVendorIds"></param>
        /// <returns>true if user consent status was updated, false otherwise.</returns>
        public bool SetUserConsentStatus(
            ISet<string> enabledPurposeIds,
            ISet<string> disabledPurposeIds,
            ISet<string> enabledVendorIds,
            ISet<string> disabledVendorIds)
        {
            return didomiForPlatform.SetUserConsentStatus(
                enabledPurposeIds,
                disabledPurposeIds,
                enabledVendorIds,
                disabledVendorIds);
        }

        /// <summary>
        /// Disable all purposes and vendors for the user
        /// </summary>
        /// <returns>true if user consent status was updated, false otherwise.</returns>
        public bool SetUserDisagreeToAll()
        {
            return didomiForPlatform.SetUserDisagreeToAll();
        }

        /// <summary>
        /// Check if the consent should be collected depending on if we have any consents or 
        /// if we have some consents but the number of days before displaying the notice again has not expired yet
        /// </summary>
        /// <returns></returns>
        public bool ShouldConsentBeCollected()
        {
            return didomiForPlatform.ShouldConsentBeCollected();
        }

        /// <summary>
        /// Method used to change SDK language when user changes language within client app(if client app supports that feature)
        /// </summary>
        /// <param name="languageCode">international short code(Coded2) for selected language.</param>
        public void UpdateSelectedLanguage(string languageCode)
        {
            didomiForPlatform.UpdateSelectedLanguage(languageCode);
        }
    }
}