using Assets.Plugins.Scripts.IOS;
using IO.Didomi.SDK.Events;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace IO.Didomi.SDK.IOS
{
    /// <summary>
    /// Connect calls from C# to Objective-C++
    /// The [DllImport("__Internal")] declaration binds functions to their Objective-C++ counterparts. 
    /// All functions in the iOS SDK must have a correspondig declaration in this C# file.
    /// </summary>
    public class DidomiFramework
    {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setUserAgent(string name, string version);
#endif

        public static void SetUserAgent()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            setUserAgent(Package.GetInstance().agentName, Package.GetInstance().version);
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int isReady();
#endif

        public static bool CallIsReadyMethod()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return isReady() == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setupUI();
#endif

        public static void SetupUI()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            setupUI();
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void initializeWithParameters(
                string apiKey,
                string localConfigurationPath,
                string remoteConfigurationUrl,
                string providerId,
                bool disableDidomiRemoteConfig,
                string languageCode,
                string noticeId,
                string countryCode,
                string regionCode
        );
#endif

        public static void Initialize(DidomiInitializeParameters initializeParameters)
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            initializeWithParameters(
                initializeParameters.apiKey,
                initializeParameters.localConfigurationPath,
                initializeParameters.remoteConfigurationUrl,
                initializeParameters.providerId,
                initializeParameters.disableDidomiRemoteConfig,
                initializeParameters.languageCode,
                initializeParameters.noticeId,
                initializeParameters.countryCode,
                initializeParameters.regionCode
                );
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getTranslatedText(string key);
#endif

        public static string GetTranslatedText(string key)
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return getTranslatedText(key);
#else
            return String.Empty;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getCurrentUserStatus();
#endif

        public static string GetCurrentUserStatus()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return getCurrentUserStatus();
#else
            return String.Empty;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int setCurrentUserStatus(
            string purposesStatus,
            string vendorsStatus
        );
#endif

        public static bool SetCurrentUserStatus(
            string purposesStatus,
            string vendorsStatus
        )
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return setCurrentUserStatus(purposesStatus, vendorsStatus) == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int commitCurrentUserStatusTransaction(
            string enabledVendorIds,
            string disabledVendorIds,
            string enabledPurposeIds,
            string disabledPurposeIds
        );
#endif

        public static bool CommitCurrentUserStatusTransaction(
            string enabledVendorIds,
            string disabledVendorIds,
            string enabledPurposeIds,
            string disabledPurposeIds
        )
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return commitCurrentUserStatusTransaction(
                enabledVendorIds,
                disabledVendorIds,
                enabledPurposeIds,
                disabledPurposeIds
            ) == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getUserStatus();
#endif

        public static string GetUserStatus()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return getUserStatus();
#else
            return String.Empty;
#endif
        }


#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void hideNotice();
#endif

        public static void HideNotice()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            hideNotice();
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void hidePreferences();
#endif

        public static void HidePreferences()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            hidePreferences();
#endif
        }


#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int isConsentRequired();
#endif

        public static bool IsConsentRequired()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return isConsentRequired() == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int isNoticeVisible();
#endif

        public static bool IsNoticeVisible()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return isNoticeVisible() == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int isPreferencesVisible();
#endif

        public static bool IsPreferencesVisible()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return isPreferencesVisible() == 1;
#else
            return false;
#endif
        }


#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void showPreferences();
#endif

        public static void ShowPreferences()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            showPreferences();
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int isUserConsentStatusPartial();
#endif

        public static bool IsUserConsentStatusPartial()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return isUserConsentStatusPartial() == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int isUserLegitimateInterestStatusPartial();
#endif

        public static bool IsUserLegitimateInterestStatusPartial()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return isUserLegitimateInterestStatusPartial() == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int isUserStatusPartial();
#endif

        public static bool IsUserStatusPartial()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return isUserStatusPartial() == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void reset();
#endif

        public static void Reset()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            reset();
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int setUserAgreeToAll();
#endif

        public static bool SetUserAgreeToAll()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return setUserAgreeToAll() == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int setUserDisagreeToAll();
#endif

        public static bool SetUserDisagreeToAll()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return setUserDisagreeToAll() == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int shouldConsentBeCollected();
#endif

        public static bool ShouldConsentBeCollected()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return shouldConsentBeCollected() == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int shouldUserStatusBeCollected();
#endif

        public static bool ShouldUserStatusBeCollected()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return shouldUserStatusBeCollected() == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void showNotice();
#endif

        public static void ShowNotice()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            showNotice();
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void updateSelectedLanguage(string languageCode);
#endif

        public static void UpdateSelectedLanguage(string languageCode)
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            updateSelectedLanguage(languageCode);
#endif
        }

        public static void OnError(Action onErrorAction)
        {
            onErrorActionInner = onErrorAction;
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            onError(CallOnError);
#endif
        }

        static Action onErrorActionInner;

        public delegate void OnErrorDelegate(string errorEvent);

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
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
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            onReady(CallOnReady);
#endif
        }

        static Action onReadyActionInner;

        public delegate void OnReadyDelegate();

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void onReady(OnReadyDelegate callback);
#endif

        [AOT.MonoPInvokeCallback(typeof(OnReadyDelegate))]
        static void CallOnReady()
        {
            onReadyActionInner?.Invoke();
        }

        public delegate int NativeSyncAcknowledged();

        public delegate void OnEventListenerDelegate(int eventType, string argument);

        public delegate void OnSyncReadyEventListenerDelegate(int eventType, int statusApplied, int syncAcknowledgedCallbackIndex);

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void addEventListener(OnEventListenerDelegate eventListenerDelegate, OnSyncReadyEventListenerDelegate syncReadyEventListenerDelegate);

        [DllImport("__Internal")]
        private static extern int syncAcknowledgedCallback(int callbackIndex);

        [DllImport("__Internal")]
        private static extern void removeSyncAcknowledgedCallback(int callbackIndex);
#endif

        public static void AddEventListener(DidomiEventListener eventListener)
        {
            eventListenerInner = eventListener;
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            addEventListener(CallOnEventListenerDelegate, CallOnSyncReadyEventListenerDelegate);
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
                case DDMEventType.DDMEventTypeReady:
                    eventListenerInner.OnReady(new ReadyEvent());
                    break;
                case DDMEventType.DDMEventTypeError:
                    eventListenerInner.OnError(new ErrorEvent(argument));
                    break;
                case DDMEventType.DDMEventTypeNoticeClickAgree:
                    eventListenerInner.OnNoticeClickAgree(new NoticeClickAgreeEvent());
                    break;
                case DDMEventType.DDMEventTypeNoticeClickDisagree:
                    eventListenerInner.OnNoticeClickDisagree(new NoticeClickDisagreeEvent());
                    break;
                case DDMEventType.DDMEventTypeNoticeClickMoreInfo:
                    eventListenerInner.OnNoticeClickMoreInfo(new NoticeClickMoreInfoEvent());
                    break;
                case DDMEventType.DDMEventTypeNoticeClickViewVendors:
                    eventListenerInner.OnNoticeClickViewVendors(new NoticeClickViewVendorsEvent());
                    break;
                case DDMEventType.DDMEventTypeNoticeClickPrivacyPolicy:
                    eventListenerInner.OnNoticeClickPrivacyPolicy(new NoticeClickPrivacyPolicyEvent());
                    break;
                case DDMEventType.DDMEventTypeNoticeClickViewSPIPurposes:
                    eventListenerInner.OnNoticeClickViewSPIPurposes(new NoticeClickViewSPIPurposesEvent());
                    break;
                case DDMEventType.DDMEventTypeShowNotice:
                    eventListenerInner.OnShowNotice(new ShowNoticeEvent());
                    break;
                case DDMEventType.DDMEventTypeHideNotice:
                    eventListenerInner.OnHideNotice(new HideNoticeEvent());
                    break;
                case DDMEventType.DDMEventTypeShowPreferences:
                    eventListenerInner.OnShowPreferences(new ShowPreferencesEvent());
                    break;
                case DDMEventType.DDMEventTypeHidePreferences:
                    eventListenerInner.OnHidePreferences(new HidePreferencesEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickAgreeToAll:
                    eventListenerInner.OnPreferencesClickAgreeToAll(new PreferencesClickAgreeToAllEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickDisagreeToAll:
                    eventListenerInner.OnPreferencesClickDisagreeToAll(new PreferencesClickDisagreeToAllEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickViewPurposes:
                    eventListenerInner.OnPreferencesClickViewPurposes(new PreferencesClickViewPurposesEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickAgreeToAllPurposes:
                    eventListenerInner.OnPreferencesClickAgreeToAllPurposes(new PreferencesClickAgreeToAllPurposesEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickDisagreeToAllPurposes:
                    eventListenerInner.OnPreferencesClickDisagreeToAllPurposes(new PreferencesClickDisagreeToAllPurposesEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickResetAllPurposes:
                    eventListenerInner.OnPreferencesClickResetAllPurposes(new PreferencesClickResetAllPurposesEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickPurposeAgree:
                    eventListenerInner.OnPreferencesClickPurposeAgree(new PreferencesClickPurposeAgreeEvent(argument));
                    break;
                case DDMEventType.DDMEventTypePreferencesClickPurposeDisagree:
                    eventListenerInner.OnPreferencesClickPurposeDisagree(new PreferencesClickPurposeDisagreeEvent(argument));
                    break;
                case DDMEventType.DDMEventTypePreferencesClickCategoryAgree:
                    eventListenerInner.OnPreferencesClickCategoryAgree(new PreferencesClickCategoryAgreeEvent(argument));
                    break;
                case DDMEventType.DDMEventTypePreferencesClickCategoryDisagree:
                    eventListenerInner.OnPreferencesClickCategoryDisagree(new PreferencesClickCategoryDisagreeEvent(argument));
                    break;
                case DDMEventType.DDMEventTypePreferencesClickViewSPIPurposes:
                    eventListenerInner.OnPreferencesClickViewSPIPurposes(new PreferencesClickViewSPIPurposesEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickViewVendors:
                    eventListenerInner.OnPreferencesClickViewVendors(new PreferencesClickViewVendorsEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickSaveChoices:
                    eventListenerInner.OnPreferencesClickSaveChoices(new PreferencesClickSaveChoicesEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickSPIPurposeAgree:
                    eventListenerInner.OnPreferencesClickSPIPurposeAgree(new PreferencesClickSPIPurposeAgreeEvent(argument));
                    break;
                case DDMEventType.DDMEventTypePreferencesClickSPIPurposeDisagree:
                    eventListenerInner.OnPreferencesClickSPIPurposeDisagree(new PreferencesClickSPIPurposeDisagreeEvent(argument));
                    break;
                case DDMEventType.DDMEventTypePreferencesClickSPICategoryAgree:
                    eventListenerInner.OnPreferencesClickSPICategoryAgree(new PreferencesClickSPICategoryAgreeEvent(argument));
                    break;
                case DDMEventType.DDMEventTypePreferencesClickSPICategoryDisagree:
                    eventListenerInner.OnPreferencesClickSPICategoryDisagree(new PreferencesClickSPICategoryDisagreeEvent(argument));
                    break;
                case DDMEventType.DDMEventTypePreferencesClickSPIPurposeSaveChoices:
                    eventListenerInner.OnPreferencesClickSPIPurposeSaveChoices(new PreferencesClickSPIPurposeSaveChoicesEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickAgreeToAllVendors:
                    eventListenerInner.OnPreferencesClickAgreeToAllVendors(new PreferencesClickAgreeToAllVendorsEvent());
                    break;
                case DDMEventType.DDMEventTypePreferencesClickDisagreeToAllVendors:
                    eventListenerInner.OnPreferencesClickDisagreeToAllVendors(new PreferencesClickDisagreeToAllVendorsEvent());
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
                case DDMEventType.DDMEventTypeSyncDone:
                    eventListenerInner.OnSyncDone(new SyncDoneEvent(argument));
                    break;
                case DDMEventType.DDMEventTypeSyncError:
                    eventListenerInner.OnSyncError(new SyncErrorEvent(argument));
                    break;
                case DDMEventType.DDMEventTypeLanguageUpdated:
                    eventListenerInner.OnLanguageUpdated(new LanguageUpdatedEvent(argument));
                    break;
                case DDMEventType.DDMEventTypeLanguageUpdateFailed:
                    eventListenerInner.OnLanguageUpdateFailed(new LanguageUpdateFailedEvent(argument));
                    break;
                default:
                    break;
            }
        }

        [AOT.MonoPInvokeCallback(typeof(OnSyncReadyEventListenerDelegate))]
        static void CallOnSyncReadyEventListenerDelegate(int eventType, int statusApplied, int syncAcknowledgedCallbackIndex)
        {
            DDMEventType eventTypeEnum = (DDMEventType)eventType;
            switch (eventTypeEnum)
            {
                case DDMEventType.DDMEventTypeSyncReady:
                    var eventTriggered = eventListenerInner.OnSyncReady(new SyncReadyEvent(
                        statusApplied > 0,
                        () => {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
                            return syncAcknowledgedCallback(syncAcknowledgedCallbackIndex) > 0;
#else
                            return false;
#endif
                        }));
                    if (!eventTriggered)
                    {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
                        removeSyncAcknowledgedCallback(syncAcknowledgedCallbackIndex);
#endif
                    }
                    break;
            }
        }

        public delegate void OnVendorStatusListenerDelegate(string vendorStatus);

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void addVendorStatusListener(string vendorId, OnVendorStatusListenerDelegate vendorStatusListenerDelegate);
#endif

        public static void AddVendorStatusListener(string vendorId, DidomiVendorStatusListener vendorStatusListener)
        {
            vendorStatusListenersInner.Add(vendorId, vendorStatusListener);
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            addVendorStatusListener(vendorId, CallOnVendorStatusListenerDelegate);
#endif
        }

        static Dictionary<string, DidomiVendorStatusListener> vendorStatusListenersInner = new Dictionary<string, DidomiVendorStatusListener>();

        [AOT.MonoPInvokeCallback(typeof(OnVendorStatusListenerDelegate))]
        static void CallOnVendorStatusListenerDelegate(string vendorStatusJson)
        {
            CurrentUserStatus.VendorStatus vendorStatus = IOSObjectMapper.ConvertToVendorStatus(vendorStatusJson);
            var vendorStatusListener = vendorStatusListenersInner[vendorStatus.Id];
            if (vendorStatusListener != null)
            {
                vendorStatusListener.OnVendorStatusChanged(vendorStatus);
            }
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void removeVendorStatusListener(string vendorId);
#endif

        public static void RemoveVendorStatusListener(string vendorId)
        {
            vendorStatusListenersInner.Remove(vendorId);
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            removeVendorStatusListener(vendorId);
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getRequiredPurposeIds();
#endif

        public static string GetRequiredPurposeIds()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return getRequiredPurposeIds();
#else
            return String.Empty;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getRequiredPurposes();
#endif

        public static string GetRequiredPurposes()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return getRequiredPurposes();
#else
            return String.Empty;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getPurpose(string purposeId);
#endif

        public static string GetPurpose(string purposeId)
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return getPurpose(purposeId);
#else
            return String.Empty;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getRequiredVendorIds();
#endif

        public static string GetRequiredVendorIds()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return getRequiredVendorIds();
#else
            return String.Empty;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getRequiredVendors();
#endif

        public static string GetRequiredVendors()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return getRequiredVendors();
#else
            return String.Empty;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getVendor(string vendorId);
#endif

        public static string GetVendor(string vendorId)
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return getVendor(vendorId);
#else
            return String.Empty;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getJavaScriptForWebView();
#endif

        public static string GetJavaScriptForWebView()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return getJavaScriptForWebView();
#else
            return String.Empty;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string getText(string key);
#endif

        public static string GetText(string key)
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            return getText(key);
#else
            return String.Empty;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int setUserStatus(string enabledConsentPurposeIds,
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
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            var result = setUserStatus(
                enabledConsentPurposeIds,
                disabledConsentPurposeIds,
                enabledLIPurposeIds,
                disabledLIPurposeIds,
                enabledConsentVendorIds,
                disabledConsentVendorIds,
                enabledLIVendorIds,
                disabledLIVendorIds);
            return result == 1;
#else
            return false;
#endif
        }


#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern int setUserStatus1(bool purposesConsentStatus, bool purposesLIStatus, bool vendorsConsentStatus, bool vendorsLIStatus);
#endif

        public static bool SetUserStatus(bool purposesConsentStatus, bool purposesLIStatus, bool vendorsConsentStatus, bool vendorsLIStatus)
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            var result = setUserStatus1(purposesConsentStatus, purposesLIStatus, vendorsConsentStatus, vendorsLIStatus);
            return result == 1;
#else
            return false;
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setUser(string organizationUserId);
#endif

        public static void SetUser(string organizationUserId)
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            setUser(organizationUserId);
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setUserAndSetupUI(string organizationUserId);
#endif

        public static void SetUserAndSetupUI(string organizationUserId)
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            setUserAndSetupUI(organizationUserId);
#endif
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
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
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
                setUserWithEncryptionParamsWithExpiration(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.InitializationVector, expiration.Value);
#endif
            }
            else
            {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
                setUserWithEncryptionParams(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.InitializationVector);
#endif
            }
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
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
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
                setUserWithEncryptionParamsWithExpirationAndSetupUI(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.InitializationVector, expiration.Value);
#endif
            }
            else
            {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
                setUserWithEncryptionParamsAndSetupUI(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.InitializationVector);
#endif
            }
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
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
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
                setUserWithHashParamsWithExpiration(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.Digest, parameters.Salt, expiration.Value);
#endif
            }
            else
            {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
                setUserWithHashParams(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.Digest, parameters.Salt);
#endif
            }
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
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
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
                setUserWithHashParamsWithExpirationAndSetupUI(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.Digest, parameters.Salt, expiration.Value);
#endif
            }
            else
            {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
                setUserWithHashParamsAndSetupUI(parameters.Id, parameters.Algorithm, parameters.SecretId, parameters.Digest, parameters.Salt);
#endif
            }
        }

#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void clearUser();
#endif

        public static void ClearUser()
        {
#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR
            clearUser();
#endif
        }
    }
}