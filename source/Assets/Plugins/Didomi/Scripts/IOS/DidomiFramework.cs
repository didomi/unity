using Assets.Plugins.Scripts.IOS;
using IO.Didomi.SDK.Events;
using System;
using System.Runtime.InteropServices;

namespace IO.Didomi.SDK.IOS
{
    /// <summary>
    /// Connect calls from C# to Objective-C++
    /// The [DllImport("__Internal")] declaration binds functions to their Objective-C++ counterparts. 
    /// All functions in the iOS SDK must have a correspondig declaration in this C# file.
    /// </summary>
    public class DidomiFramework
    {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setUserAgent(string name, string version);
#endif

        public static void SetUserAgent()
        {
#if UNITY_IOS && !UNITY_EDITOR
            setUserAgent(Package.GetInstance().agentName, Package.GetInstance().version);
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool isReady();
#endif

        public static bool CallIsReadyMethod()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return isReady();
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool setupUI();
#endif

        public static bool SetupUI()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return setupUI();
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void initialize(string apiKey,
            string localConfigurationPath,
            string remoteConfigurationPath,
            string providerId,
            bool disableDidomiRemoteConfig,
            string languageCode);
#endif

        public static bool Initialize(
          string apiKey,
          string localConfigurationPath,
          string remoteConfigurationPath,
          string providerId,
          bool disableDidomiRemoteConfig,
          string languageCode)
        {
#if UNITY_IOS && !UNITY_EDITOR
            initialize(apiKey, localConfigurationPath, remoteConfigurationPath, providerId, disableDidomiRemoteConfig, languageCode);
#endif

            return false;
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void initializeWithNoticeId(string apiKey,
            string localConfigurationPath,
            string remoteConfigurationPath,
            string providerId,
            bool disableDidomiRemoteConfig,
            string languageCode,
            string noticeId);
#endif

        public static bool Initialize(
          string apiKey,
          string localConfigurationPath,
          string remoteConfigurationPath,
          string providerId,
          bool disableDidomiRemoteConfig,
          string languageCode,
          string noticeId)
        {
#if UNITY_IOS && !UNITY_EDITOR
            initializeWithNoticeId(apiKey, localConfigurationPath, remoteConfigurationPath, providerId, disableDidomiRemoteConfig, languageCode, noticeId);
#endif

            return false;
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getTranslatedText(string key);
#endif

        public static string GetTranslatedText(string key)
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getTranslatedText(key);
#else
            return String.Empty;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool getUserConsentStatusForPurpose(string purposeId);
#endif

        public static bool GetUserConsentStatusForPurpose(string purposeId)
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getUserConsentStatusForPurpose(purposeId);
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool getUserConsentStatusForVendor(string vendorId);
#endif

        public static bool GetUserConsentStatusForVendor(string vendorId)
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getUserConsentStatusForVendor(vendorId);
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool getUserConsentStatusForVendorAndRequiredPurposes(string vendorId);
#endif

        public static bool GetUserConsentStatusForVendorAndRequiredPurposes(string vendorId)
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getUserConsentStatusForVendorAndRequiredPurposes(vendorId);
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int getUserLegitimateInterestStatusForPurpose(string purposeId);
#endif

        public static int GetUserLegitimateInterestStatusForPurpose(string purposeId)
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getUserLegitimateInterestStatusForPurpose(purposeId);
#else
            return 0;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int getUserLegitimateInterestStatusForVendor(string vendorId);
#endif

        public static int GetUserLegitimateInterestStatusForVendor(string vendorId)
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getUserLegitimateInterestStatusForVendor(vendorId);
#else
            return 0;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int getUserLegitimateInterestStatusForVendorAndRequiredPurposes(string vendorId);
#endif

        public static int GetUserLegitimateInterestStatusForVendorAndRequiredPurposes(string vendorId)
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getUserLegitimateInterestStatusForVendorAndRequiredPurposes(vendorId);
#else
            return 0;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getUserStatus();
#endif

        public static string GetUserStatus()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getUserStatus();
#else
            return String.Empty;
#endif
        }


#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void hideNotice();
#endif

        public static void HideNotice()
        {
#if UNITY_IOS && !UNITY_EDITOR
            hideNotice();
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void hidePreferences();
#endif

        public static void HidePreferences()
        {
#if UNITY_IOS && !UNITY_EDITOR
            hidePreferences();
#endif
        }


#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool isConsentRequired();
#endif

        public static bool IsConsentRequired()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return isConsentRequired();
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool isNoticeVisible();
#endif

        public static bool IsNoticeVisible()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return isNoticeVisible();
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool isPreferencesVisible();
#endif

        public static bool IsPreferencesVisible()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return isPreferencesVisible();
#else
            return false;
#endif
        }


#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void showPreferences();
#endif

        public static void ShowPreferences()
        {
#if UNITY_IOS && !UNITY_EDITOR
            showPreferences();
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool isUserConsentStatusPartial();
#endif

        public static bool IsUserConsentStatusPartial()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return isUserConsentStatusPartial();
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void reset();
#endif

        public static void Reset()
        {
#if UNITY_IOS && !UNITY_EDITOR
            reset();
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool setUserAgreeToAll();
#endif

        public static bool SetUserAgreeToAll()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return setUserAgreeToAll();
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool setUserDisagreeToAll();
#endif

        public static bool SetUserDisagreeToAll()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return setUserDisagreeToAll();
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool shouldConsentBeCollected();
#endif

        public static bool ShouldConsentBeCollected()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return shouldConsentBeCollected();
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void showNotice();
#endif

        public static void ShowNotice()
        {
#if UNITY_IOS && !UNITY_EDITOR
            showNotice();
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void updateSelectedLanguage(string languageCode);
#endif

        public static void UpdateSelectedLanguage(string languageCode)
        {
#if UNITY_IOS && !UNITY_EDITOR
            updateSelectedLanguage(languageCode);
#endif
        }

        public static void OnError(Action onErrorAction)
        {
            onErrorActionInner = onErrorAction;
#if UNITY_IOS && !UNITY_EDITOR
            onError(CallOnError);
#endif
        }

        static Action onErrorActionInner;

        public delegate void OnErrorDelegate(string errorEvent);

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void onError(OnErrorDelegate callback);
#endif

        [AOT.MonoPInvokeCallback(typeof(OnErrorDelegate))]
        static void CallOnError(string errorEvent)
        {
            onErrorActionInner?.Invoke();
        }

        public static void OnReady(Action onReadyAction)
        {
            onReadyActionInner = onReadyAction;
#if UNITY_IOS && !UNITY_EDITOR
            onReady(CallOnReady);
#endif
        }

        static Action onReadyActionInner;

        public delegate void OnReadyDelegate();

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void onReady(OnReadyDelegate callback);
#endif

        [AOT.MonoPInvokeCallback(typeof(OnReadyDelegate))]
        static void CallOnReady()
        {
            onReadyActionInner?.Invoke();
        }

        public delegate void OnEventListenerDelegate(int eventType, string argument);

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void addEventListener(OnEventListenerDelegate eventListenerDelegate);
#endif

        public static void AddEventListener(DidomiEventListener eventListener)
        {
            eventListenerInner = eventListener;
#if UNITY_IOS && !UNITY_EDITOR
            addEventListener(CallOnEventListenerDelegate);
#endif
        }

        static DidomiEventListener eventListenerInner;

        [AOT.MonoPInvokeCallback(typeof(OnEventListenerDelegate))]
        static void CallOnEventListenerDelegate(int eventType, string argument)
        {
            DDMEventType eventTypeEnum = (DDMEventType)eventType;
            switch (eventTypeEnum)
            {
                case DDMEventType.DDMEventTypeConsentChanged:
                    eventListenerInner.OnConsentChanged(new ConsentChangedEvent());
                    break;
                case DDMEventType.DDMEventTypeHideNotice:
                    eventListenerInner.OnHideNotice(new HideNoticeEvent());
                    break;
                case DDMEventType.DDMEventTypeReady:
                    eventListenerInner.OnReady(new ReadyEvent());
                    break;
                case DDMEventType.DDMEventTypeError:
                    eventListenerInner.OnError(new ErrorEvent(argument));
                    break;
                case DDMEventType.DDMEventTypeShowNotice:
                    eventListenerInner.OnShowNotice(new ShowNoticeEvent());
                    break;
                case DDMEventType.DDMEventTypeNoticeClickAgree:
                    eventListenerInner.OnNoticeClickAgree(new NoticeClickAgreeEvent());
                    break;
                case DDMEventType.DDMEventTypeNoticeClickDisagree:
                    eventListenerInner.OnNoticeClickDisagree(new NoticeClickDisagreeEvent());
                    break;
                case DDMEventType.DDMEventTypeNoticeClickMoreInfo:
                    eventListenerInner.OnConsentChanged(new ConsentChangedEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickAgreeToAll:
                    eventListenerInner.OnPreferencesClickAgreeToAll(new PreferencesClickAgreeToAllEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickDisagreeToAll:
                    eventListenerInner.OnPreferencesClickDisagreeToAll(new PreferencesClickDisagreeToAllEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickPurposeAgree:
                    eventListenerInner.OnPreferencesClickPurposeAgree(new PreferencesClickPurposeAgreeEvent(argument));
                    break;
                case DDMEventType.DDMEventTypePreferencesClickPurposeDisagree:
                    eventListenerInner.OnPreferencesClickPurposeDisagree(new PreferencesClickPurposeDisagreeEvent(argument));
                    break;
                case DDMEventType.DDMEventTypePreferencesClickViewVendors:
                    eventListenerInner.OnPreferencesClickViewVendors(new PreferencesClickViewVendorsEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickSaveChoices:
                    eventListenerInner.OnPreferencesClickSaveChoices(new PreferencesClickSaveChoicesEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickVendorAgree:
                    eventListenerInner.OnPreferencesClickVendorAgree(new PreferencesClickVendorAgreeEvent(argument));
                    break;
                case DDMEventType.DDMEventTypePreferencesClickVendorDisagree:
                    eventListenerInner.OnPreferencesClickVendorDisagree(new PreferencesClickVendorDisagreeEvent(argument));
                    break;
                case DDMEventType.DDMEventTypePreferencesClickVendorSaveChoices:
                    eventListenerInner.OnPreferencesClickVendorSaveChoices(new PreferencesClickVendorSaveChoicesEvent());
                    break;
                case DDMEventType.DDMEventTypeHidePreferences:
                    eventListenerInner.OnHidePreferences(new HidePreferencesEvent());
                    break;
                case DDMEventType.DDMEventTypeShowPreferences:
                    eventListenerInner.OnShowPreferences(new ShowPreferencesEvent());
                    break;
                case DDMEventType.DDMEventTypeSyncDone:
                    eventListenerInner.OnSyncDone(new SyncDoneEvent(argument));
                    break;
                case DDMEventType.DDMEventTypeSyncError:
                    eventListenerInner.OnSyncError(new SyncErrorEvent(argument));
                    break;
                default:
                    break;
            }

        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getDisabledPurposeIds();
#endif

        public static string GetDisabledPurposeIds()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getDisabledPurposeIds();
#else
            return String.Empty;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getDisabledVendorIds();
#endif

        public static string GetDisabledVendorIds()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getDisabledVendorIds();
#else
            return String.Empty;
#endif
        }



#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getEnabledPurposeIds();
#endif

        public static string GetEnabledPurposeIds()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getEnabledPurposeIds();
#else
            return String.Empty;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getEnabledVendorIds();
#endif

        public static string GetEnabledVendorIds()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getEnabledVendorIds();
#else
            return String.Empty;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getRequiredPurposeIds();
#endif

        public static string GetRequiredPurposeIds()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getRequiredPurposeIds();
#else
            return String.Empty;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getRequiredVendorIds();
#endif

        public static string GetRequiredVendorIds()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getRequiredVendorIds();
#else
            return String.Empty;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getJavaScriptForWebView();
#endif

        public static string GetJavaScriptForWebView()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getJavaScriptForWebView();
#else
            return String.Empty;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getText(string key);
#endif

        public static string GetText(string key)
        {
#if UNITY_IOS && !UNITY_EDITOR
            return getText(key);
#else
            return String.Empty;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool setUserConsentStatus(string enabledPurposeIds, string disabledPurposeIds, string enabledVendorIds, string disabledVendorIds);
#endif

        public static bool SetUserConsentStatus(string enabledPurposeIds, string disabledPurposeIds, string enabledVendorIds, string disabledVendorIds)
        {
#if UNITY_IOS && !UNITY_EDITOR
            return setUserConsentStatus(enabledPurposeIds, disabledPurposeIds, enabledVendorIds, disabledVendorIds);
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool setUserStatus(string enabledConsentPurposeIds,
            string disabledConsentPurposeIds,
            string enabledLIPurposeIds,
            string disabledLIPurposeIds,
            string enabledConsentVendorIds,
            string disabledConsentVendorIds,
            string enabledLIVendorIds,
            string disabledLIVendorIds);
#endif

        public static bool SetUserStatus(
            string enabledConsentPurposeIds,
            string disabledConsentPurposeIds,
            string enabledLIPurposeIds,
            string disabledLIPurposeIds,
            string enabledConsentVendorIds,
            string disabledConsentVendorIds,
            string enabledLIVendorIds,
            string disabledLIVendorIds)
        {
#if UNITY_IOS && !UNITY_EDITOR
            return setUserStatus(
                enabledConsentPurposeIds,
                disabledConsentPurposeIds,
                enabledLIPurposeIds,
                disabledLIPurposeIds,
                enabledConsentVendorIds,
                disabledConsentVendorIds,
                enabledLIVendorIds,
                disabledLIVendorIds);
#else
            return false;
#endif
        }


#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool setUserStatus1(bool purposesConsentStatus, bool purposesLIStatus, bool vendorsConsentStatus, bool vendorsLIStatus);
#endif

        public static bool SetUserStatus(bool purposesConsentStatus, bool purposesLIStatus, bool vendorsConsentStatus, bool vendorsLIStatus)
        {
#if UNITY_IOS && !UNITY_EDITOR
            return setUserStatus1(purposesConsentStatus, purposesLIStatus, vendorsConsentStatus, vendorsLIStatus);
#else
            return false;
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setUser(string organizationUserId);
#endif

        public static void SetUser(string organizationUserId)
        {
#if UNITY_IOS && !UNITY_EDITOR
            setUser(organizationUserId);
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setUserAndSetupUI(string organizationUserId);
#endif

        public static void SetUserAndSetupUI(string organizationUserId)
        {
#if UNITY_IOS && !UNITY_EDITOR
            setUserAndSetupUI(organizationUserId);
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setUserWithEncryptionParamsWithExpiration(string organizationUserId, string algorithm, string secretID, string initializationVector, long expiration);

        [DllImport("__Internal")]
        private static extern void setUserWithEncryptionParams(string organizationUserId, string algorithm, string secretID, string initializationVector);
#endif

        public static void SetUserWithEncryptionParams(UserAuthWithEncryptionParams parameters)
        {
            long? expiration = parameters.Expiration;
            if (expiration.HasValue)
            {
#if UNITY_IOS && !UNITY_EDITOR
                setUserWithEncryptionParamsWithExpiration(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.InitializationVector, expiration.Value);
#endif
            }
            else
            {
#if UNITY_IOS && !UNITY_EDITOR
                setUserWithEncryptionParams(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.InitializationVector);
#endif
            }
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setUserWithEncryptionParamsWithExpirationAndSetupUI(string organizationUserId, string algorithm, string secretID, string initializationVector, long expiration);

        [DllImport("__Internal")]
        private static extern void setUserWithEncryptionParamsAndSetupUI(string organizationUserId, string algorithm, string secretID, string initializationVector);
#endif

        public static void SetUserWithEncryptionParamsAndSetupUI(UserAuthWithEncryptionParams parameters)
        {
            long? expiration = parameters.Expiration;
            if (expiration.HasValue)
            {
#if UNITY_IOS && !UNITY_EDITOR
                setUserWithEncryptionParamsWithExpirationAndSetupUI(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.InitializationVector, expiration.Value);
#endif
            }
            else
            {
#if UNITY_IOS && !UNITY_EDITOR
                setUserWithEncryptionParamsAndSetupUI(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.InitializationVector);
#endif
            }
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setUserWithHashParamsWithExpiration(string organizationUserId, string algorithm, string secretID, string digest, string salt, long expiration);

        [DllImport("__Internal")]
        private static extern void setUserWithHashParams(string organizationUserId, string algorithm, string secretID, string digest, string salt);
#endif

        public static void SetUserWithHashParams(UserAuthWithHashParams parameters)
        {
            long? expiration = parameters.Expiration;
            if (expiration.HasValue)
            {
#if UNITY_IOS && !UNITY_EDITOR
                setUserWithHashParamsWithExpiration(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.Digest, parameters.Salt, expiration.Value);
#endif
            }
            else
            {
#if UNITY_IOS && !UNITY_EDITOR
                setUserWithHashParams(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.Digest, parameters.Salt);
#endif
            }
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setUserWithHashParamsWithExpirationAndSetupUI(string organizationUserId, string algorithm, string secretID, string digest, string salt, long expiration);

        [DllImport("__Internal")]
        private static extern void setUserWithHashParamsAndSetupUI(string organizationUserId, string algorithm, string secretID, string digest, string salt);
#endif

        public static void SetUserWithHashParamsAndSetupUI(UserAuthWithHashParams parameters)
        {
            long? expiration = parameters.Expiration;
            if (expiration.HasValue)
            {
#if UNITY_IOS && !UNITY_EDITOR
                setUserWithHashParamsWithExpirationAndSetupUI(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.Digest, parameters.Salt, expiration.Value);
#endif
            }
            else
            {
#if UNITY_IOS && !UNITY_EDITOR
                setUserWithHashParamsAndSetupUI(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.Digest, parameters.Salt);
#endif
            }
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void clearUser();
#endif

        public static void ClearUser()
        {
#if UNITY_IOS && !UNITY_EDITOR
            clearUser();
#endif
        }
    }
}