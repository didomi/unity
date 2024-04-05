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
                        else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS)
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
        /// Add a vendor status listener
        /// </summary>
        /// <param name="vendorId">Id of the requested vendor</param>
        /// <param name="vendorStatusListener"></param>
        public void AddVendorStatusListener(string vendorId, DidomiVendorStatusListener vendorStatusListener)
        {
            didomiForPlatform.AddVendorStatusListener(vendorId, vendorStatusListener);
        }

        /// <summary>
        /// Remove vendor status listeners for a specific vendor
        /// </summary>
        /// <param name="vendorId">Id of the requested vendor</param>
        public void RemoveVendorStatusListener(string vendorId)
        {
            didomiForPlatform.RemoveVendorStatusListener(vendorId);
        }

        /// <summary>
        /// Disables or enables showing mock UIs If platform is Unity Editor.
        /// </summary>
        /// <param name="disable">True disables, otherwise false.</param>
        public void DisableMockUI(bool disable)
        {
            UnityEditorDidomi editorPlatform = didomiForPlatform as UnityEditorDidomi;
            if (editorPlatform != null)
            {
                editorPlatform.DisableMockUI(disable);
            }
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
        /// Get the current user consent status as a CurrentUserStatus object.
        /// </summary>
        /// <returns>An instance of CurrentUserStatus</returns>
        public CurrentUserStatus GetCurrentUserStatus()
        {
            return didomiForPlatform.GetCurrentUserStatus();
        }

        /// <summary>
        /// Set the current user consent status from a CurrentUserStatus object.
        /// </summary>
        /// <param name="status">The requested status</param>
        /// <returns>True if user consent has changed, false otherwise</returns>
        public bool SetCurrentUserStatus(CurrentUserStatus status)
        {
            return didomiForPlatform.SetCurrentUserStatus(status);
        }

        public CurrentUserStatusTransaction OpenCurrentUserStatusTransaction()
        {
            return new CurrentUserStatusTransaction(CommitCurrentUserStatusTransaction);
        }

        private bool CommitCurrentUserStatusTransaction(
             ISet<string> enabledVendors,
             ISet<string> disabledVendors,
             ISet<string> enabledPurposes,
             ISet<string> disabledPurposes
        )
        {
            return didomiForPlatform.CommitCurrentUserStatusTransaction(enabledVendors, disabledVendors, enabledPurposes, disabledPurposes);
        }

        /// <summary>
        /// Get the user consent status as a UserStatus object.
        /// </summary>
        /// <returns></returns>
        public UserStatus GetUserStatus()
        {
            return didomiForPlatform.GetUserStatus();
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
        /// Initialize the Didomi SDK and load its configuration.
        /// This method should be called from your Application onCreate method.
        /// </summary>
        [ObsoleteAttribute("This method is deprecated. Use Initialize with DidomiInitializeParameters instead.")]
        public void Initialize(
            string apiKey,
            string localConfigurationPath,
            string remoteConfigurationURL,
            string providerId,
            bool disableDidomiRemoteConfig,
            string languageCode)
        {
            Initialize(
                new DidomiInitializeParameters(
                    apiKey,
                    localConfigurationPath,
                    remoteConfigurationURL,
                    providerId,
                    disableDidomiRemoteConfig,
                    languageCode
                )
            );
        }

        /// <summary>
        /// Initialize the Didomi SDK and load its configuration.
        /// This method should be called from your Application onCreate method.
        /// </summary>
        [ObsoleteAttribute("This method is deprecated. Use Initialize with DidomiInitializeParameters instead.")]
        public void Initialize(
            string apiKey,
            string localConfigurationPath,
            string remoteConfigurationURL,
            string providerId,
            bool disableDidomiRemoteConfig,
            string languageCode,
            string noticeId)
        {
            Initialize(
                new DidomiInitializeParameters(
                    apiKey,
                    localConfigurationPath,
                    remoteConfigurationURL,
                    providerId,
                    disableDidomiRemoteConfig,
                    languageCode,
                    noticeId
                )
            );
        }

        /// <summary>
        /// Initialize the Didomi SDK and load its configuration.
        /// This method should be called from your Application onCreate method.
        /// </summary>
        /// <param name="initializeParameters"> the object specifying parameters to initialize the SDK.</param>
        public void Initialize(DidomiInitializeParameters initializeParameters)
        {
            didomiForPlatform.Initialize(initializeParameters);
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
        /// <returns>True if some required consent are missing</returns>
        public bool IsUserConsentStatusPartial()
        {
            return didomiForPlatform.IsUserConsentStatusPartial();
        }

        /// <summary>
        /// Determine if legitimate interest information is available for all purposes and vendors that are required
        /// </summary>
        /// <returns>True if some required consent are missing with legitimate interest information</returns>
        public bool IsUserLegitimateInterestStatusPartial()
        {
            return didomiForPlatform.IsUserLegitimateInterestStatusPartial();
        }

        /// <summary>
        /// Determine if user status is partial
        /// </summary>
        /// <returns>True if consent or legitimate interest is partial</returns>
        public bool IsUserStatusPartial()
        {
            return didomiForPlatform.IsUserStatusPartial();
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
        /// Provide a function that needs to be called if the SDK encounters an error during initialization.
        /// </summary>
        /// <param name="didomiCallable">that is called when an error occurs.</param>
        public void OnError(Action didomiCallable)
        {
            didomiForPlatform.OnError(didomiCallable);
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
        /// Disable all purposes and vendors for the user
        /// </summary>
        /// <returns>true if user consent status was updated, false otherwise.</returns>
        public bool SetUserDisagreeToAll()
        {
            return didomiForPlatform.SetUserDisagreeToAll();
        }
        
        /// <summary>
        /// Set the user consent status
        /// </summary>
        /// <param name="enabledConsentPurposeIds"></param>
        /// <param name="disabledConsentPurposeIds"></param>
        /// <param name="enabledLIPurposeIds"></param>
        /// <param name="disabledLIPurposeIds"></param>
        /// <param name="enabledConsentVendorIds"></param>
        /// <param name="disabledConsentVendorIds"></param>
        /// <param name="enabledLIVendorIds"></param>
        /// <param name="disabledLIVendorIds"></param>
        /// <returns></returns>
        public bool SetUserStatus(
            ISet<string> enabledConsentPurposeIds,
            ISet<string> disabledConsentPurposeIds,
            ISet<string> enabledLIPurposeIds,
            ISet<string> disabledLIPurposeIds,
            ISet<string> enabledConsentVendorIds,
            ISet<string> disabledConsentVendorIds,
            ISet<string> enabledLIVendorIds,
            ISet<string> disabledLIVendorIds)
        {
            return didomiForPlatform.SetUserStatus(
                enabledConsentPurposeIds,
                disabledConsentPurposeIds,
                enabledLIPurposeIds,
                disabledLIPurposeIds,
                enabledConsentVendorIds,
                disabledConsentVendorIds,
                enabledLIVendorIds,
                disabledLIVendorIds);
        }
        
        /// <summary>
        /// Set the user consent status
        /// </summary>
        /// <param name="purposesConsentStatus"></param>
        /// <param name="purposesLIStatus"></param>
        /// <param name="vendorsConsentStatus"></param>
        /// <param name="vendorsLIStatus"></param>
        /// <returns></returns>
        public bool SetUserStatus(
            bool purposesConsentStatus,
            bool purposesLIStatus,
            bool vendorsConsentStatus,
            bool vendorsLIStatus)
        {
            return didomiForPlatform.SetUserStatus(
                purposesConsentStatus,
                purposesLIStatus,
                vendorsConsentStatus,
                vendorsLIStatus);
        }

        [ObsoleteAttribute("This method is deprecated. Use ShouldUserStatusBeCollected instead.")]
        /// <summary>
        /// Check if the consent should be collected depending on if we have any consents or 
        /// if we have some consents but the number of days before displaying the notice again has not expired yet
        /// </summary>
        /// <returns>True if the consent should be collected</returns>
        public bool ShouldConsentBeCollected()
        {
            return didomiForPlatform.ShouldConsentBeCollected();
        }

        /// <summary>
        /// Determine if the User Status (consent) should be collected or not. User Status should be collected if:
        /// - Regulation is different from NONE and
        /// - User status is partial and
        /// - The number of days before displaying the notice again has exceeded the limit specified on the Console or no User Status has been saved
        /// if we have some consents but the number of days before displaying the notice again has not expired yet
        /// </summary>
        /// <returns>True if the User Status (consent) should be collected</returns>
        public bool ShouldUserStatusBeCollected()
        {
            return didomiForPlatform.ShouldUserStatusBeCollected();
        }

        /// <summary>
        /// Method used to change SDK language when user changes language within client app(if client app supports that feature)
        /// </summary>
        /// <param name="languageCode">international short code(Coded2) for selected language.</param>
        public void UpdateSelectedLanguage(string languageCode)
        {
            didomiForPlatform.UpdateSelectedLanguage(languageCode);
        }

        /// <summary>
        /// Set custom user information from organization
        /// </summary>
        /// <param name="organizationUserId">Organization user id</param>
        public void SetUser(string organizationUserId)
        {
            didomiForPlatform.SetUser(organizationUserId);
        }

        /// <summary>
        /// Set custom user information from organization
        /// </summary>
        /// <param name="userAuthParams">Parameters to synchronize the user consent</param>
        public void SetUser(UserAuthParams userAuthParams)
        {
            didomiForPlatform.SetUser(userAuthParams);
        }

        /// <summary>
        /// Set custom user information from organization, and call setupUI after synchronization if sdk was initialized
        /// </summary>
        /// <param name="organizationUserId">Organization user id</param>
        public void SetUserAndSetupUI(string organizationUserId)
        {
            didomiForPlatform.SetUserAndSetupUI(organizationUserId);
        }

        /// <summary>
        /// Set custom user information from organization, and call setupUI after synchronization if sdk was initialized
        /// </summary>
        /// <param name="userAuthParams">Parameters to synchronize the user consent</param>
        public void SetUserAndSetupUI(UserAuthParams userAuthParams)
        {
            didomiForPlatform.SetUserAndSetupUI(userAuthParams);
        }

        /// <summary>
        /// Remove any custom user information from organization
        /// </summary>
        public void ClearUser()
        {
            didomiForPlatform.ClearUser();
        }
    }
}
