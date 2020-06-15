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
		//#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setUserAgent(string agentName, string agentSDKVersion);
        //#endif

        public static void SetUserAgent()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            setUserAgent(Package.GetInstance().agentName, Package.GetInstance().version);
            //#endif
        }
		
        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool isReady();
        //#endif

        public static bool CallIsReadyMethod()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return isReady();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool setupUI();
        //#endif

        public static bool SetupUI()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return setupUI();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void initialize(string apiKey,
            string localConfigurationPath,
            string remoteConfigurationPath,
            string providerId,
            bool disableDidomiRemoteConfig,
            string languageCode);
        //#endif

        public static bool Initialize(
          string apiKey,
          string localConfigurationPath,
          string remoteConfigurationPath,
          string providerId,
          bool disableDidomiRemoteConfig,
          string languageCode)
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            initialize(apiKey, localConfigurationPath, remoteConfigurationPath, providerId, disableDidomiRemoteConfig, languageCode);
            //#endif

            return false;
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getTranslatedText(string key);
        //#endif

        public static string GetTranslatedText(string key)
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return getTranslatedText(key);
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool getUserConsentStatusForPurpose(string purposeId);
        //#endif

        public static bool GetUserConsentStatusForPurpose(string purposeId)
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return getUserConsentStatusForPurpose(purposeId);
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool getUserConsentStatusForVendor(string vendorId);
        //#endif

        public static bool GetUserConsentStatusForVendor(string vendorId)
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return getUserConsentStatusForVendor(vendorId);
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool getUserConsentStatusForVendorAndRequiredPurposes(string vendorId);
        //#endif

        public static bool GetUserConsentStatusForVendorAndRequiredPurposes(string vendorId)
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return getUserConsentStatusForVendorAndRequiredPurposes(vendorId);
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void hideNotice();
        //#endif

        public static void HideNotice()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            hideNotice();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void hidePreferences();
        //#endif

        public static void HidePreferences()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            hidePreferences();
            //#endif
        }


        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool isConsentRequired();
        //#endif

        public static bool IsConsentRequired()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return isConsentRequired();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool isPreferencesVisible();
        //#endif

        public static bool IsPreferencesVisible()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return isPreferencesVisible();
            //#endif
        }


        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void showPreferences();
        //#endif

        public static void ShowPreferences()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            showPreferences();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool isUserConsentStatusPartial();
        //#endif

        public static bool IsUserConsentStatusPartial()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return isUserConsentStatusPartial();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void reset();
        //#endif

        public static void Reset()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            reset();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool setUserAgreeToAll();
        //#endif

        public static bool SetUserAgreeToAll()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return setUserAgreeToAll();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool setUserDisagreeToAll();
        //#endif

        public static bool SetUserDisagreeToAll()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return setUserDisagreeToAll();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool shouldConsentBeCollected();
        //#endif

        public static bool ShouldConsentBeCollected()
        {
           //#if UNITY_IOS && !UNITY_EDITOR
            return shouldConsentBeCollected();
           //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void showNotice();
        //#endif

        public static void ShowNotice()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            showNotice();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void updateSelectedLanguage(string languageCode);
        //#endif

        public static void UpdateSelectedLanguage(string languageCode)
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            updateSelectedLanguage(languageCode);
            //#endif
        }

        public static void OnReady(Action onReadyAction)
        {
            onReadyActionInner = onReadyAction;
            //#if UNITY_IOS && !UNITY_EDITOR
            onReady(CallOnReady);
            //#endif
        }

        static Action onReadyActionInner;

        public delegate void OnReadyDelegate();

        [DllImport("__Internal")]
        private static extern void onReady(OnReadyDelegate callback);

        [AOT.MonoPInvokeCallback(typeof(OnReadyDelegate))]
        static void CallOnReady()
        {
            onReadyActionInner?.Invoke();
        }

        public delegate void OnEventListenerDelegate(DDMEventType eventType, string argument);

        [DllImport("__Internal")]
        private static extern void addEventListener(OnEventListenerDelegate eventListenerDelegate);

        public static void AddEventListener(EventListener eventListener)
        {
            eventListenerInner = eventListener;
            //#if UNITY_IOS && !UNITY_EDITOR
            addEventListener(CallOnEventListenerDelegate);
            //#endif
        }

        static EventListener eventListenerInner;

        [AOT.MonoPInvokeCallback(typeof(OnEventListenerDelegate))]
        static void CallOnEventListenerDelegate(DDMEventType eventType, string argument)
        {
            switch (eventType)
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
                case DDMEventType.DDMEventTypeShowNotice:
                    eventListenerInner.OnShowNotice(new ShowNoticeEvent());
                    break;
                case DDMEventType.DDMEventTypeNoticeClickAgree:
                    eventListenerInner.OnNoticeClickAgree(new NoticeClickAgreeEvent());
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
                default:
                    break;
            }

        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getDisabledPurposeIds();
        //#endif

        public static string GetDisabledPurposeIds()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return getDisabledPurposeIds();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getDisabledVendorIds();
        //#endif

        public static string GetDisabledVendorIds()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return getDisabledVendorIds();
            //#endif
        }

        

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getEnabledPurposeIds();
        //#endif

        public static string GetEnabledPurposeIds()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return getEnabledPurposeIds();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getEnabledVendorIds();
        //#endif

        public static string GetEnabledVendorIds()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return getEnabledVendorIds();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getRequiredPurposeIds();
        //#endif

        public static string GetRequiredPurposeIds()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return getRequiredPurposeIds();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getRequiredVendorIds();
        //#endif

        public static string GetRequiredVendorIds()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return getRequiredVendorIds();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getJavaScriptForWebView();
        //#endif

        public static string GetJavaScriptForWebView()
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return getJavaScriptForWebView();
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getText(string key);
        //#endif

        public static string GetText(string key)
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return getText(key);
            //#endif
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool setUserConsentStatus(string enabledPurposeIds, string disabledPurposeIds, string enabledVendorIds, string disabledVendorIds);
        //#endif

        public static bool SetUserConsentStatus(string enabledPurposeIds, string disabledPurposeIds, string enabledVendorIds, string disabledVendorIds)
        {
            //#if UNITY_IOS && !UNITY_EDITOR
            return setUserConsentStatus(enabledPurposeIds, disabledPurposeIds, enabledVendorIds, disabledVendorIds);
            //#endif
        }
    }
}

