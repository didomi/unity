using IO.Didomi.SDK.Events;
using IO.Didomi.SDK.Interfaces;
using System;
using System.Collections.Generic;

namespace IO.Didomi.SDK.IOS
{
    class IOSDidomi : IDidomi
    {
        private const string NotCallableForObjectiveC = "The function is not callable for IOS platform. Original functions in Didomi-IOS SDK are not available for Objective-C, Please Check IOS-SDK documentation.";

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

        public void AddEventListener(EventListener eventListener)
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

        public void OnReady(DidomiCallable didomiCallable)
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
