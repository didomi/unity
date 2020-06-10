using IO.Didomi.SDK.Events;
using System;
using System.Collections.Generic;

namespace IO.Didomi.SDK.Interfaces
{
    /// <summary>
    /// IDidomi interface defines the functionality of Unity Plugin
    /// </summary>
    public interface IDidomi
    {
        void AddEventListener(EventListener eventListener);
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
        bool IsConsentRequired();
        bool IsUserConsentStatusPartial();
        bool IsNoticeVisible();
        bool IsPreferencesVisible();
        bool IsReady();
        void OnReady(Action didomiCallable);
        void SetupUI();
        void ShowNotice();
        void ShowPreferences();
        void Reset();
        bool SetUserAgreeToAll();
        bool SetUserConsentStatus(ISet<string> enabledPurposeIds, ISet<string> disabledPurposeIds, ISet<string> enabledVendorIds, ISet<string> disabledVendorIds);
        bool SetUserDisagreeToAll();
        bool ShouldConsentBeCollected();
        void UpdateSelectedLanguage(string languageCode);
    }
}