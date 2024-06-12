using IO.Didomi.SDK.Events;
using IO.Didomi.SDK.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IO.Didomi.SDK.Android
{
    /// <summary>
    /// Android implementation of the IDidomi interface
    /// This class uses JNI to make calls to the native Android SDK through the
    /// `AndroidJavaClass`, `AndroidJavaObject`, and `AndroidJavaProxy` classes.
    /// </summary>
    internal class AndroidDidomi : IDidomi
    {
        private const string PluginName = "io.didomi.sdk.Didomi";
        private const string UnityPlayerFullClassName = "com.unity3d.player.UnityPlayer";
		
		public AndroidDidomi()
        {
            CallVoidMethod("setUserAgent", Package.GetInstance().agentName, Package.GetInstance().version);
        }

        public void AddEventListener(DidomiEventListener eventListener)
        {
            var eventListenerProxy = new EventListenerProxy(eventListener);

            CallVoidMethod("addEventListener", eventListenerProxy);
        }

        public void AddVendorStatusListener(string vendorId, DidomiVendorStatusListener vendorStatusListener)
        {
            var vendorStatusListenerProxy = new DidomiVendorStatusListenerProxy(vendorStatusListener);

            CallVoidMethod("addVendorStatusListener", vendorId, vendorStatusListenerProxy);
        }

        public void RemoveVendorStatusListener(string vendorId)
        {
            CallVoidMethod("removeVendorStatusListener", vendorId);
        }

        public string GetJavaScriptForWebView()
        {
            return CallReturningStringMethod("getJavaScriptForWebView");
        }

        public Purpose GetPurpose(string purposeId)
        {
            var obj = CallReturningJavaObjectMethod("getPurpose", purposeId);

            return AndroidObjectMapper.ConvertToPurpose(obj);
        }

        public ISet<Purpose> GetRequiredPurposes()
        {
            var obj = CallReturningJavaObjectMethod("getRequiredPurposes");

            return AndroidObjectMapper.ConvertToSetPurpose(obj);
        }

        public ISet<string> GetRequiredPurposeIds()
        {
            var obj = CallReturningJavaObjectMethod("getRequiredPurposeIds");

            return AndroidObjectMapper.ConvertToSetString(obj);
        }

        public ISet<Vendor> GetRequiredVendors()
        {
            var obj = CallReturningJavaObjectMethod("getRequiredVendors");

            return AndroidObjectMapper.ConvertToSetVendor(obj);
        }

        public ISet<string> GetRequiredVendorIds()
        {
            var obj = CallReturningJavaObjectMethod("getRequiredVendorIds");

            return AndroidObjectMapper.ConvertToSetString(obj);
        }

        public IDictionary<string, string> GetText(string key)
        {
            var obj = CallReturningJavaObjectMethod("getText", key);

            return AndroidObjectMapper.ConvertToDictionary(obj);
        }

        public string GetTranslatedText(string key)
        {
            return CallReturningStringMethod("getTranslatedText", key);
        }

        public CurrentUserStatus GetCurrentUserStatus()
        {
            var currentUserStatusObject = CallReturningJavaObjectMethod("getCurrentUserStatus");

            return AndroidObjectMapper.ConvertToCurrentUserStatus(currentUserStatusObject);
        }

        public bool SetCurrentUserStatus(CurrentUserStatus status)
        {
            return CallReturningBoolMethod("setCurrentUserStatus",
                AndroidObjectMapper.ConvertToJavaCurrentUserStatus(status)
            );
        }

        public bool CommitCurrentUserStatusTransaction(
             ISet<string> enabledVendors,
             ISet<string> disabledVendors,
             ISet<string> enabledPurposes,
             ISet<string> disabledPurposes
        )
        {
            var transactionObject = CallReturningJavaObjectMethod("openCurrentUserStatusTransaction");
            SetCurrentUserStatusTransactionItems(transactionObject, enabledVendors, "enableVendor");
            SetCurrentUserStatusTransactionItems(transactionObject, disabledVendors, "disableVendor");
            SetCurrentUserStatusTransactionItems(transactionObject, enabledPurposes, "enablePurpose");
            SetCurrentUserStatusTransactionItems(transactionObject, disabledPurposes, "disablePurpose");
            return transactionObject.Call<bool>("commit");
        }

        private void SetCurrentUserStatusTransactionItems(AndroidJavaObject transactionObject, ISet<string> items, string javaMethod)
        {
            foreach (string item in items)
            {
                transactionObject.Call<AndroidJavaObject>(javaMethod, item);
            }
        }

        public UserStatus GetUserStatus()
        {
            var userStatusObject = CallReturningJavaObjectMethod("getUserStatus");

            return AndroidObjectMapper.ConvertToUserStatus(userStatusObject);
        }

        public Vendor GetVendor(string vendorId)
        {
            var obj = CallReturningJavaObjectMethod("getVendor", vendorId);

            return AndroidObjectMapper.ConvertToVendor(obj);
        }

        public void HideNotice()
        {
            CallVoidMethod("hideNotice");
        }

        public void HidePreferences()
        {
            CallVoidMethod("hidePreferences");
        }

        public void Initialize(DidomiInitializeParameters initializeParameters)
        {
            AndroidJavaObject nativeParams = AndroidObjectMapper.ConvertToJavaDidomiInitializeParameters(initializeParameters);

            CallVoidMethodForInitialize("initialize", nativeParams);
        }

        public bool IsConsentRequired()
        {
            return CallReturningBoolMethod("isConsentRequired");
        }

        public bool IsUserConsentStatusPartial()
        {
            return CallReturningBoolMethod("isUserConsentStatusPartial");
        }

        public bool IsUserLegitimateInterestStatusPartial()
        {
            return CallReturningBoolMethod("isUserLegitimateInterestStatusPartial");
        }

        public bool IsUserStatusPartial()
        {
            return CallReturningBoolMethod("isUserStatusPartial");
        }

        public bool IsNoticeVisible()
        {
            return CallReturningBoolMethod("isNoticeVisible");
        }

        public bool IsPreferencesVisible()
        {
            return CallReturningBoolMethod("isPreferencesVisible");
        }

        public bool IsReady()
        {
            return CallReturningBoolMethod("isReady");
        }

        public void OnError(Action action)
        {
            var didomiCallable = new DidomiCallable(action);

            CallVoidMethod("onError", didomiCallable);
        }

        public void OnReady(Action action)
        {
            var didomiCallable = new DidomiCallable(action);

            CallVoidMethod("onReady", didomiCallable);
        }

        /// <summary>
        /// Call native `setupUI` function from UI thread
        /// </summary>
        public void SetupUI()
        {
            try
            {
                using (var playerClass = new AndroidJavaClass(UnityPlayerFullClassName))
                {
                    using (var activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        activity.Call("runOnUiThread", new AndroidJavaRunnable(NativeSetupUI));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(string.Format("Exception:{0}", ex.ToString()));

                throw ex;
            }
        }

        public void ShowNotice()
        {
            CallVoidMethodWithActivityArg("showNotice");
        }

        public void ShowPreferences()
        {
            CallVoidMethodWithActivityArg("showPreferences");
        }

        public void Reset()
        {
            CallVoidMethod("reset");
        }

        public bool SetUserAgreeToAll()
        {
            return CallReturningBoolMethod("setUserAgreeToAll");
        }
   
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
            return CallReturningBoolMethod(
                "setUserStatus",
                AndroidObjectMapper.ConvertFromHashSetStringToSetAndroidJavaObject(enabledConsentPurposeIds),
                AndroidObjectMapper.ConvertFromHashSetStringToSetAndroidJavaObject(disabledConsentPurposeIds),
                AndroidObjectMapper.ConvertFromHashSetStringToSetAndroidJavaObject(enabledLIPurposeIds),
                AndroidObjectMapper.ConvertFromHashSetStringToSetAndroidJavaObject(disabledLIPurposeIds),
                AndroidObjectMapper.ConvertFromHashSetStringToSetAndroidJavaObject(enabledConsentVendorIds),
                AndroidObjectMapper.ConvertFromHashSetStringToSetAndroidJavaObject(disabledConsentVendorIds),
                AndroidObjectMapper.ConvertFromHashSetStringToSetAndroidJavaObject(enabledLIVendorIds),
                AndroidObjectMapper.ConvertFromHashSetStringToSetAndroidJavaObject(disabledLIVendorIds));
        }

        public bool SetUserStatus(
            bool purposesConsentStatus,
            bool purposesLIStatus,
            bool vendorsConsentStatus,
            bool vendorsLIStatus)
        {
            return CallReturningBoolMethod(
                "setUserStatus",
                purposesConsentStatus,
                purposesLIStatus,
                vendorsConsentStatus,
                vendorsLIStatus);
        }

        public bool SetUserDisagreeToAll()
        {
            return CallReturningBoolMethod("setUserDisagreeToAll");
        }

        [ObsoleteAttribute("This method is deprecated. Use shouldUserStatusBeCollected instead")]
        public bool ShouldConsentBeCollected()
        {
            return CallReturningBoolMethod("shouldConsentBeCollected");
        }

        public bool ShouldUserStatusBeCollected()
        {
            return CallReturningBoolMethod("shouldUserStatusBeCollected");
        }

        public void UpdateSelectedLanguage(string languageCode)
        {
            CallVoidMethod("updateSelectedLanguage", languageCode);
        }

        public void SetUser(string organizationUserId)
        {
            CallVoidMethod("setUser", organizationUserId);
        }

        public void SetUser(UserAuthParams userAuthParams, IList<UserAuthParams> synchronizedUsers)
        {
            AndroidJavaObject nativeUserAuthParams = AndroidObjectMapper.ConvertToJavaUserAuthParams(userAuthParams);
            AndroidJavaObject nativeSynchronizedUsersParams = AndroidObjectMapper.ConvertToJavaUserAuthList(synchronizedUsers);
            CallVoidMethod("setUser", nativeUserAuthParams, nativeSynchronizedUsersParams);
        }

        public void SetUserAndSetupUI(string organizationUserId)
        {
            CallVoidMethodWithActivityLastArg("setUser", organizationUserId);
        }

        public void SetUserAndSetupUI(UserAuthParams userAuthParams, IList<UserAuthParams> synchronizedUsers)
        {
            AndroidJavaObject nativeUserAuthParams = AndroidObjectMapper.ConvertToJavaUserAuthParams(userAuthParams);
            AndroidJavaObject nativeSynchronizedUsersParams = AndroidObjectMapper.ConvertToJavaUserAuthList(synchronizedUsers);
            CallVoidMethodWithActivityLastArg("setUser", nativeUserAuthParams, nativeSynchronizedUsersParams);
        }

        public void ClearUser()
        {
            CallVoidMethod("clearUser");
        }

        private static bool CallReturningBoolMethod(string methodName, params object[] args)
        {
            return CallReturningMethodBase<bool>(methodName, args);
        }

        private static string CallReturningStringMethod(string methodName, params object[] args)
        {
            return CallReturningMethodBase<string>(methodName, args);
        }

        private static AndroidJavaObject CallReturningJavaObjectMethod(string methodName, params object[] args)
        {
            return CallReturningMethodBase<AndroidJavaObject>(methodName, args);
        }

        private static void CallVoidMethodWithActivityArg(string methodName)
        {
            try
            {
                using (var playerClass = new AndroidJavaClass(UnityPlayerFullClassName))
                {
                    using (var activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        using (var _pluginClass = new AndroidJavaClass(PluginName))
                        {
                            var pluginInstance = _pluginClass.CallStatic<AndroidJavaObject>("getInstance");

                            pluginInstance.Call(methodName, activity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(string.Format("Exception:{0}", ex.ToString()));

                throw ex;
            }
        }

        /// <summary>
        /// Calls 'method(args..., activity)'
        /// </summary>
        private static void CallVoidMethodWithActivityLastArg(string methodName, params object[] args)
        {
            try
            {
                using (var playerClass = new AndroidJavaClass(UnityPlayerFullClassName))
                {
                    using (var activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        using (var _pluginClass = new AndroidJavaClass(PluginName))
                        {
                            var pluginInstance = _pluginClass.CallStatic<AndroidJavaObject>("getInstance");

                            MapNullValuesToJava(args);

                            var obj = new object[args.Length + 1];

                            for (int i = 0; i < obj.Length-1; i++)
                            {
                                obj[i] = args[i];
                            }

                            obj[obj.Length-1] = activity;

                            pluginInstance.Call(methodName, obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(string.Format("Exception:{0}", ex.ToString()));

                throw ex;
            }
        }

        /// <summary>
        /// Calls 'method(activity)'
        /// </summary>
        private static void CallReturningMethodWithActivityArg(string methodName)
        {
            try
            {
                using (var playerClass = new AndroidJavaClass(UnityPlayerFullClassName))
                {
                    using (var activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        using (var _pluginClass = new AndroidJavaClass(PluginName))
                        {
                            var pluginInstance = _pluginClass.CallStatic<AndroidJavaObject>("getInstance");

                            pluginInstance.Call<AndroidJavaObject>(methodName, activity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(string.Format("Exception:{0}", ex.ToString()));

                throw ex;
            }
        }

        /// <summary>
        /// Calls 'method(application, args...)'
        /// </summary>
        private static void CallVoidMethodForInitialize(string methodName, params object[] args)
        {
            try
            {
                using (var playerClass = new AndroidJavaClass(UnityPlayerFullClassName))
                {
                    using (var activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        using (var _pluginClass = new AndroidJavaClass(PluginName))
                        {
                            var application = activity.Call<AndroidJavaObject>("getApplication");
                            
                            var pluginInstance = _pluginClass.CallStatic<AndroidJavaObject>("getInstance");

                            MapNullValuesToJava(args);

                            var obj = new object[args.Length + 1];

                            for (int i = 1; i < obj.Length; i++)
                            {
                                obj[i] = args[i - 1];
                            }

                            obj[0] = application;
                            pluginInstance.Call(methodName, obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(string.Format("Exception:{0}", ex.ToString()));

                throw ex;
            }
        }

        private static void CallVoidMethod(string methodName, params object[] args)
        {
            try
            {
                using (var _pluginClass = new AndroidJavaClass(PluginName))
                {
                    var pluginInstance = _pluginClass.CallStatic<AndroidJavaObject>("getInstance");

                    MapNullValuesToJava(args);

                    pluginInstance.Call(methodName, args);
                }
            }
            catch (Exception ex)
            {
                Debug.Log(string.Format("Exception:{0}", ex.ToString()));

                throw ex;
            }
        }

        private static T CallReturningMethodBase<T>(string methodName, params object[] args)
        {
            T retval = default(T);

            try
            {
                using (var _pluginClass = new AndroidJavaClass(PluginName))
                {
                    var pluginInstance = _pluginClass.CallStatic<AndroidJavaObject>("getInstance");

                    MapNullValuesToJava(args);

                    retval = pluginInstance.Call<T>(methodName, args);
                }
            }
            catch (Exception ex)
            {
                Debug.Log(string.Format("Exception:{0}", ex.ToString()));

                throw ex;
            }

            return retval;
        }

        /// <summary>
        /// Android libraries in Unity cannot handle C# null values and throw a MethodNotFound exception
        /// when null is provided. This function maps null values to avoid that issue..
        /// </summary>
        /// <param name="args"></param>
        private static void MapNullValuesToJava(object[] args)
        {
            AndroidJavaObject nullObject = null;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null)
                {
                    args[i] = nullObject;
                }
            }
        }

        /// <summary>
        /// Calls 'setupUI' native method from current thread (must be called from UI thread)
        /// </summary>
        private void NativeSetupUI() {
            CallVoidMethodWithActivityArg("setupUI");
        }
    }
}
