using IO.Didomi.SDK.Events;
using System;
using System.Collections.Generic;

namespace IO.Didomi.SDK.Interfaces
{
    /// <summary>
    /// Functions exposed to Unity apps by the Didomi plugin
    /// </summary>
    public interface IDidomi
    {
        void AddEventListener(DidomiEventListener eventListener);
        ISet<string> GetDisabledPurposeIds();
        ISet<Purpose> GetDisabledPurposes();
        ISet<string> GetDisabledVendorIds();
        ISet<Vendor> GetDisabledVendors();
        ISet<string> GetEnabledPurposeIds();
        ISet<Purpose> GetEnabledPurposes();
        ISet<string> GetEnabledVendorIds();
        ISet<Vendor> GetEnabledVendors();
        string GetJavaScriptForWebView();
        Purpose GetPurpose(string purposeId);
        ISet<string> GetRequiredPurposeIds();
        ISet<Purpose> GetRequiredPurposes();
        ISet<string> GetRequiredVendorIds();
        ISet<Vendor> GetRequiredVendors();
        IDictionary<string, string> GetText(string key);
        string GetTranslatedText(string key);
        bool GetUserConsentStatusForPurpose(string purposeId);
        bool GetUserConsentStatusForVendor(string vendorId);
        bool GetUserConsentStatusForVendorAndRequiredPurposes(string vendorId);
        bool GetUserLegitimateInterestStatusForPurpose(string purposeId);
        bool GetUserLegitimateInterestStatusForVendor(string vendorId);
        bool GetUserLegitimateInterestStatusForVendorAndRequiredPurposes(string vendorId);
        UserStatus GetUserStatus();
        Vendor GetVendor(string vendorId);
        void HideNotice();
        void HidePreferences();
        void Initialize(
          string apiKey,
          string localConfigurationPath,
          string remoteConfigurationURL,
          string providerId,
          bool disableDidomiRemoteConfig,
          string languageCode
          );
        void Initialize(
          string apiKey,
          string localConfigurationPath,
          string remoteConfigurationURL,
          string providerId,
          bool disableDidomiRemoteConfig,
          string languageCode,
          string noticeId
          );
        void Initialize(DidomiInitializeParameters parameters);
        bool IsConsentRequired();
        bool IsUserConsentStatusPartial();
        bool IsNoticeVisible();
        bool IsPreferencesVisible();
        bool IsReady();
        void OnError(Action didomiCallable);
        void OnReady(Action didomiCallable);
        void SetupUI();
        void ShowNotice();
        void ShowPreferences();
        void Reset();
        bool SetUserAgreeToAll();
        [ObsoleteAttribute("This method is deprecated. Use SetUserStatus instead.")]
        bool SetUserConsentStatus(ISet<string> enabledPurposeIds, ISet<string> disabledPurposeIds, ISet<string> enabledVendorIds, ISet<string> disabledVendorIds);
        bool SetUserDisagreeToAll();
        bool SetUserStatus(
            ISet<string> enabledConsentPurposeIds,
            ISet<string> disabledConsentPurposeIds,
            ISet<string> enabledLIPurposeIds,
            ISet<string> disabledLIPurposeIds,
            ISet<string> enabledConsentVendorIds,
            ISet<string> disabledConsentVendorIds,
            ISet<string> enabledLIVendorIds,
            ISet<string> disabledLIVendorIds);
        bool SetUserStatus(
            bool purposesConsentStatus,
            bool purposesLIStatus,
            bool vendorsConsentStatus,
            bool vendorsLIStatus);
        bool ShouldConsentBeCollected();
        bool shouldUserStatusBeCollected();
        void UpdateSelectedLanguage(string languageCode);
        void SetUser(string organizationUserId);
        void SetUser(UserAuthParams userAuthParams);
        void SetUserAndSetupUI(string organizationUserId);
        void SetUserAndSetupUI(UserAuthParams userAuthParams);
        void ClearUser();
    }
}
