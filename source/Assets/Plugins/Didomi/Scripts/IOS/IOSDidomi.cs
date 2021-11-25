using IO.Didomi.SDK.Events;
using IO.Didomi.SDK.Interfaces;
using System;
using System.Collections.Generic;

namespace IO.Didomi.SDK.IOS
{
    /// <summary>
    /// Convert objects from Objective-C to Unity and back
    /// </summary>
    class IOSDidomi : IDidomi
    {
        private const string NotCallableForObjectiveC = "The function is not available on iOS. Please check the plugin documentation for more information.";

		public IOSDidomi()
        {
            DidomiFramework.SetUserAgent();
        }
		
        public ISet<Purpose> GetDisabledPurposes()
        {
            throw new NotImplementedException(NotCallableForObjectiveC);
        }

        public ISet<Vendor> GetDisabledVendors()
        {
            throw new NotImplementedException(NotCallableForObjectiveC);
        }

        public ISet<Purpose> GetEnabledPurposes()
        {
            throw new NotImplementedException(NotCallableForObjectiveC);
        }

        public ISet<Vendor> GetEnabledVendors()
        {
            throw new NotImplementedException(NotCallableForObjectiveC);
        }

        public ISet<Purpose> GetRequiredPurposes()
        {
            throw new NotImplementedException(NotCallableForObjectiveC);
        }

        public ISet<Vendor> GetRequiredVendors()
        {
            throw new NotImplementedException(NotCallableForObjectiveC);
        }

        public Purpose GetPurpose(string purposeId)
        {
            throw new NotImplementedException(NotCallableForObjectiveC);
        }

        public Vendor GetVendor(string vendorId)
        {
            throw new NotImplementedException(NotCallableForObjectiveC);
        }

        public void AddEventListener(DidomiEventListener eventListener)
        {
            DidomiFramework.AddEventListener(eventListener);
        }

        public ISet<string> GetDisabledPurposeIds()
        {
            var jsonText = DidomiFramework.GetDisabledPurposeIds();

            return IOSObjectMapper.ConvertToSetString(jsonText);
        }

        public ISet<string> GetDisabledVendorIds()
        {
            var jsonText = DidomiFramework.GetDisabledVendorIds();

            return IOSObjectMapper.ConvertToSetString(jsonText);
        }

        public ISet<string> GetEnabledPurposeIds()
        {
            var jsonText = DidomiFramework.GetEnabledPurposeIds();

            return IOSObjectMapper.ConvertToSetString(jsonText);
        }

        public ISet<string> GetEnabledVendorIds()
        {
            var jsonText = DidomiFramework.GetEnabledVendorIds();

            return IOSObjectMapper.ConvertToSetString(jsonText);
        }

        public string GetJavaScriptForWebView()
        {
            return DidomiFramework.GetJavaScriptForWebView();
        }

        public ISet<string> GetRequiredPurposeIds()
        {
            var jsonText = DidomiFramework.GetRequiredPurposeIds();

            return IOSObjectMapper.ConvertToSetString(jsonText);
        }

        public ISet<string> GetRequiredVendorIds()
        {
            var jsonText = DidomiFramework.GetRequiredVendorIds();

            return IOSObjectMapper.ConvertToSetString(jsonText);
        }

        public IDictionary<string, string> GetText(string key)
        {
            var jsonText = DidomiFramework.GetText(key);

            return IOSObjectMapper.ConvertToDictionary(jsonText);
        }

        public string GetTranslatedText(string key)
        {
            return DidomiFramework.GetTranslatedText(key);
        }

        public bool GetUserConsentStatusForPurpose(string purposeId)
        {
            return DidomiFramework.GetUserConsentStatusForPurpose(purposeId);
        }

        public bool GetUserConsentStatusForVendor(string vendorId)
        {
            return DidomiFramework.GetUserConsentStatusForVendor(vendorId);
        }

        public bool GetUserConsentStatusForVendorAndRequiredPurposes(string vendorId)
        {
            return DidomiFramework.GetUserConsentStatusForVendorAndRequiredPurposes(vendorId);
        }

        //public enum ConsenStatus { Enable = 0, Disable = 1, Unknown = 2 }
        public bool GetUserLegitimateInterestStatusForPurpose(string purposeId)
        {
            var result = DidomiFramework.GetUserLegitimateInterestStatusForPurpose(purposeId);

            return result == 0;
        }

        public bool GetUserLegitimateInterestStatusForVendor(string vendorId)
        {
            var result = DidomiFramework.GetUserLegitimateInterestStatusForVendor(vendorId);

            return result == 0;
        }

        public bool GetUserLegitimateInterestStatusForVendorAndRequiredPurposes(string vendorId)
        {
            var result = DidomiFramework.GetUserLegitimateInterestStatusForVendorAndRequiredPurposes(vendorId);
            return result == 0;
        }

        public UserStatus GetUserStatus()
        {
            throw new NotImplementedException();
        }

        public void HideNotice()
        {
            DidomiFramework.HideNotice();
        }

        public void HidePreferences()
        {
            DidomiFramework.HidePreferences();
        }

        public void Initialize(
          string apiKey,
          string localConfigurationPath,
          string remoteConfigurationPath,
          string providerId,
          bool disableDidomiRemoteConfig,
          string languageCode
          )
        {
            DidomiFramework.Initialize(apiKey, localConfigurationPath, remoteConfigurationPath, providerId, disableDidomiRemoteConfig, languageCode);
        }

        public void Initialize(
          string apiKey,
          string localConfigurationPath,
          string remoteConfigurationPath,
          string providerId,
          bool disableDidomiRemoteConfig,
          string languageCode,
          string noticeId
          )
        {
            DidomiFramework.Initialize(apiKey, localConfigurationPath, remoteConfigurationPath, providerId, disableDidomiRemoteConfig, languageCode, noticeId);
        }

        public bool IsConsentRequired()
        {
            return DidomiFramework.IsConsentRequired();
        }

        public bool IsNoticeVisible()
        {
            return DidomiFramework.IsPreferencesVisible();
        }

        public bool IsPreferencesVisible()
        {
            return DidomiFramework.IsPreferencesVisible();
        }

        public bool IsReady()
        {
            return DidomiFramework.CallIsReadyMethod();
        }

        public void OnError(Action didomiCallable)
        {
            DidomiFramework.OnError(didomiCallable);
        }

        public void OnReady(Action didomiCallable)
        {
            DidomiFramework.OnReady(didomiCallable);
        }

        public void SetupUI()
        {
            DidomiFramework.SetupUI();
        }

        public void ShowPreferences()
        {
             DidomiFramework.ShowPreferences();
        }

        public bool IsUserConsentStatusPartial()
        {
            return DidomiFramework.IsUserConsentStatusPartial();
        }

        public void Reset()
        {
            DidomiFramework.Reset();
        }

        public bool SetUserAgreeToAll()
        {
            return DidomiFramework.SetUserAgreeToAll();
        }

        [ObsoleteAttribute("This method is deprecated. Use SetUserStatus instead.")]
        public bool SetUserConsentStatus(ISet<string> enabledPurposeIds, ISet<string> disabledPurposeIds, ISet<string> enabledVendorIds, ISet<string> disabledVendorIds)
        {
            return DidomiFramework.SetUserConsentStatus(
              IOSObjectMapper.ConvertFromHasSetStringToJson(enabledPurposeIds),
              IOSObjectMapper.ConvertFromHasSetStringToJson(disabledPurposeIds),
              IOSObjectMapper.ConvertFromHasSetStringToJson(enabledVendorIds),
              IOSObjectMapper.ConvertFromHasSetStringToJson(disabledVendorIds));
        }

        public bool SetUserDisagreeToAll()
        {
            return DidomiFramework.SetUserDisagreeToAll();
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
            return DidomiFramework.SetUserStatus(
             IOSObjectMapper.ConvertFromHasSetStringToJson(enabledConsentPurposeIds),
             IOSObjectMapper.ConvertFromHasSetStringToJson(disabledConsentPurposeIds),
             IOSObjectMapper.ConvertFromHasSetStringToJson(enabledLIPurposeIds),
             IOSObjectMapper.ConvertFromHasSetStringToJson(disabledLIPurposeIds),
             IOSObjectMapper.ConvertFromHasSetStringToJson(enabledConsentVendorIds),
             IOSObjectMapper.ConvertFromHasSetStringToJson(disabledConsentVendorIds),
             IOSObjectMapper.ConvertFromHasSetStringToJson(enabledLIVendorIds),
             IOSObjectMapper.ConvertFromHasSetStringToJson(disabledLIVendorIds));
        }

        public bool SetUserStatus(
            bool purposesConsentStatus,
            bool purposesLIStatus,
            bool vendorsConsentStatus,
            bool vendorsLIStatus)
        {
            return DidomiFramework.SetUserStatus(
                purposesConsentStatus,
                purposesLIStatus,
                vendorsConsentStatus,
                vendorsLIStatus);
        }

        public bool ShouldConsentBeCollected()
        {
            return DidomiFramework.ShouldConsentBeCollected();
        }

        public void ShowNotice()
        {
            DidomiFramework.ShowNotice();
        }

        public void UpdateSelectedLanguage(string languageCode)
        {
            DidomiFramework.UpdateSelectedLanguage(languageCode);
        }
    }
}
