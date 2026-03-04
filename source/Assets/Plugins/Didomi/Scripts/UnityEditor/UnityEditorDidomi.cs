using IO.Didomi.SDK.Events;
using IO.Didomi.SDK.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IO.Didomi.SDK.UnityEditor
{
    /// <summary>
    /// Stub implementation of IDidomi interface that is called when the app is run in Unity Editor.
    /// Returns empty/default values for all methods.
    /// </summary>
    public class UnityEditorDidomi : IDidomi
    {
        private const string StubTag = "[Didomi Stub]";
        private bool _isInitialized = false;
        private bool _disableMockUI = false;
        private Action _onReadyAction = null;

        private void LogStub(string methodName)
        {
            Debug.Log($"{StubTag} {methodName} called - returning stub value (Unity Editor)");
        }

        public void AddEventListener(DidomiEventListener eventListener)
        {
            LogStub("AddEventListener");
        }

        public void AddVendorStatusListener(string vendorId, DidomiVendorStatusListener vendorStatusListener)
        {
            LogStub("AddVendorStatusListener");
        }

        public void RemoveVendorStatusListener(string vendorId)
        {
            LogStub("RemoveVendorStatusListener");
        }

        /// <summary>
        /// Disables showing mock UIs If platform is Unity Editor.
        /// </summary>
        public void DisableMockUI(bool disable)
        {
            _disableMockUI = disable;
        }

        public string GetJavaScriptForWebView()
        {
            LogStub("GetJavaScriptForWebView");
            return "";
        }

        public Purpose GetPurpose(string purposeId)
        {
            LogStub("GetPurpose");
            return new Purpose("", "", "");
        }

        public ISet<string> GetRequiredPurposeIds()
        {
            LogStub("GetRequiredPurposeIds");
            return new HashSet<string>();
        }

        public ISet<Purpose> GetRequiredPurposes()
        {
            LogStub("GetRequiredPurposes");
            return new HashSet<Purpose>();
        }

        public ISet<string> GetRequiredVendorIds()
        {
            LogStub("GetRequiredVendorIds");
            return new HashSet<string>();
        }

        public ISet<Vendor> GetRequiredVendors()
        {
            LogStub("GetRequiredVendors");
            return new HashSet<Vendor>();
        }

        public IDictionary<string, string> GetText(string key)
        {
            LogStub("GetText");
            return new Dictionary<string, string>();
        }

        public string GetTranslatedText(string key)
        {
            LogStub("GetTranslatedText");
            return "";
        }

        public CurrentUserStatus GetCurrentUserStatus()
        {
            LogStub("GetCurrentUserStatus");
            return new CurrentUserStatus();
        }

        public bool SetCurrentUserStatus(CurrentUserStatus status)
        {
            LogStub("SetCurrentUserStatus");
            return false;
        }

        public bool CommitCurrentUserStatusTransaction(
             ISet<string> enabledVendors,
             ISet<string> disabledVendors,
             ISet<string> enabledPurposes,
             ISet<string> disabledPurposes
        )
        {
            LogStub("CommitCurrentUserStatusTransaction");
            return false;
        }

        public UserStatus GetUserStatus()
        {
            LogStub("GetUserStatus");
            return new UserStatus();
        }

        public string GetApplicableRegulation()
        {
            LogStub("GetApplicableRegulation");
            return "";
        }

        public Vendor GetVendor(string vendorId)
        {
            LogStub("GetVendor");
            return new Vendor(
                "",
                "",
                null,
                null,
                new List<string>(),
                new List<string>(),
                new List<string>(),
                new List<string>(),
                new List<string>(),
                new List<string>(),
                null
            );
        }

        public int GetTotalVendorCount()
        {
            LogStub("GetTotalVendorCount");
            return 0;
        }

        public int GetIABVendorCount()
        {
            LogStub("GetIABVendorCount");
            return 0;
        }

        public int GetNonIABVendorCount()
        {
            LogStub("GetNonIABVendorCount");
            return 0;
        }

        public void HideNotice()
        {
            LogStub("HideNotice");
        }

        public void HidePreferences()
        {
            LogStub("HidePreferences");
        }

        public void Initialize(DidomiInitializeParameters parameters)
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;

            _onReadyAction?.Invoke();
        }

        public bool IsConsentRequired()
        {
            LogStub("IsConsentRequired");
            return false;
        }

        public bool IsNoticeVisible()
        {
            LogStub("IsNoticeVisible");
            return false;
        }

        public bool IsPreferencesVisible()
        {
            LogStub("IsPreferencesVisible");
            return false;
        }

        public bool IsReady()
        {
            return _isInitialized;
        }

        public void OnError(Action didomiCallable)
        {
            LogStub("OnError");
        }

        public void OnReady(Action didomiCallable)
        {
            _onReadyAction = didomiCallable;

            if (_isInitialized)
            {
                _onReadyAction?.Invoke();
            }
        }

        public void SetupUI()
        {
            if (_disableMockUI)
            {
                return;
            }

            ShowNoticeMockUI();
        }

        public void ShowPreferences(Didomi.Views view)
        {
            if (_disableMockUI)
            {
                return;
            }

            ShowPreferencesMockUI();
        }

        public bool IsUserConsentStatusPartial()
        {
            LogStub("IsUserConsentStatusPartial");
            return false;
        }

        public bool IsUserLegitimateInterestStatusPartial()
        {
            LogStub("IsUserLegitimateInterestStatusPartial");
            return false;
        }

        public bool IsUserStatusPartial()
        {
            LogStub("IsUserStatusPartial");
            return false;
        }

        public void Reset()
        {
            LogStub("Reset");
        }

        public bool SetUserAgreeToAll()
        {
            LogStub("SetUserAgreeToAll");
            return true;
        }

        public bool SetUserDisagreeToAll()
        {
            LogStub("SetUserDisagreeToAll");
            return true;
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
            LogStub("SetUserStatus");
            return true;
        }

        public bool SetUserStatus(
            bool purposesConsentStatus,
            bool purposesLIStatus,
            bool vendorsConsentStatus,
            bool vendorsLIStatus)
        {
            LogStub("SetUserStatus");
            return true;
        }

        public bool ShouldConsentBeCollected()
        {
            LogStub("ShouldConsentBeCollected");
            return false;
        }

        public bool ShouldUserStatusBeCollected()
        {
            LogStub("ShouldUserStatusBeCollected");
            return false;
        }

        public void ShowNotice()
        {
            if (_disableMockUI)
            {
                return;
            }

            ShowNoticeMockUI();
        }

        private void ShowNoticeMockUI()
        {
            GetMockUIScript().ShowNoticeUI();
        }

        private void ShowPreferencesMockUI()
        {
            GetMockUIScript().ShowPurposesUI();
        }

        private UnityEditorMockUI GetMockUIScript()
        {
            GameObject mockUI = new GameObject("DidomiPluginMockUI");
            return mockUI.AddComponent<UnityEditorMockUI>();
        }

        public void UpdateSelectedLanguage(string languageCode)
        {
            LogStub("UpdateSelectedLanguage");
        }

        public void SetUser(DidomiUserParameters userParameters)
        {
            LogStub("SetUser");
        }

        public void SetUserAndSetupUI(DidomiUserParameters userParameters)
        {
            LogStub("SetUserAndSetupUI");
        }

        public void ClearUser()
        {
            LogStub("ClearUser");
        }
    }
}
