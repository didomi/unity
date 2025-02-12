using IO.Didomi.SDK.Events;
using IO.Didomi.SDK.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IO.Didomi.SDK.IOS
{
    /// <summary>
    /// Convert objects from Objective-C to Unity and back
    /// </summary>
    class IOSDidomi : IDidomi
    {

		public IOSDidomi()
        {
            DidomiFramework.SetUserAgent();
        }

        public ISet<Purpose> GetRequiredPurposes()
        {
            var jsonText = DidomiFramework.GetRequiredPurposes();

            return IOSObjectMapper.ConvertToPurposeSet(jsonText);
        }

        public ISet<Vendor> GetRequiredVendors()
        {
            var jsonText = DidomiFramework.GetRequiredVendors();

            return IOSObjectMapper.ConvertToVendorSet(jsonText);
        }

        public Purpose GetPurpose(string purposeId)
        {
            var jsonText = DidomiFramework.GetPurpose(purposeId);

            return IOSObjectMapper.ConvertToPurpose(jsonText);
        }

        public Vendor GetVendor(string vendorId)
        {
            var jsonText = DidomiFramework.GetVendor(vendorId);

            return IOSObjectMapper.ConvertToVendor(jsonText);
        }

        public int GetTotalVendorCount()
        {
            return DidomiFramework.GetTotalVendorCount();
        }

        public int GetIABVendorCount()
        {
            return DidomiFramework.GetIABVendorCount();
        }

        public int GetNonIABVendorCount()
        {
            return DidomiFramework.GetNonIABVendorCount();
        }

        public void AddEventListener(DidomiEventListener eventListener)
        {
            DidomiFramework.AddEventListener(eventListener);
        }

        public void AddVendorStatusListener(string vendorId, DidomiVendorStatusListener vendorStatusListener)
        {
            DidomiFramework.AddVendorStatusListener(vendorId, vendorStatusListener);
        }

        public void RemoveVendorStatusListener(string vendorId)
        {
            DidomiFramework.RemoveVendorStatusListener(vendorId);
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

        public CurrentUserStatus GetCurrentUserStatus()
        {
            var jsonText = DidomiFramework.GetCurrentUserStatus();

            return IOSObjectMapper.ConvertToCurrentUserStatus(jsonText);
        }

        public bool SetCurrentUserStatus(CurrentUserStatus status)
        {
            var purposes = IOSObjectMapper.ConvertFromPurposeStatusDictionaryToJson(status.Purposes);
            var vendors = IOSObjectMapper.ConvertFromVendorsStatusDictionaryToJson(status.Vendors);
            return DidomiFramework.SetCurrentUserStatus(purposes, vendors);
        }

        public bool CommitCurrentUserStatusTransaction(
             ISet<string> enabledVendors,
             ISet<string> disabledVendors,
             ISet<string> enabledPurposes,
             ISet<string> disabledPurposes
        )
        {
            return DidomiFramework.CommitCurrentUserStatusTransaction(
                IOSObjectMapper.ConvertFromHashSetStringToJson(enabledVendors),
                IOSObjectMapper.ConvertFromHashSetStringToJson(disabledVendors),
                IOSObjectMapper.ConvertFromHashSetStringToJson(enabledPurposes),
                IOSObjectMapper.ConvertFromHashSetStringToJson(disabledPurposes)
            );
        }

        public UserStatus GetUserStatus()
        {
            var jsonText = DidomiFramework.GetUserStatus();

            return IOSObjectMapper.ConvertToUserStatus(jsonText);
        }

        public string GetApplicableRegulation()
        {
            var regulationEnum = DidomiFramework.GetApplicableRegulation();

            return IOSObjectMapper.ConvertFromRegulationEnumToString(regulationEnum);
        }

        public void HideNotice()
        {
            DidomiFramework.HideNotice();
        }

        public void HidePreferences()
        {
            DidomiFramework.HidePreferences();
        }

        public void Initialize(DidomiInitializeParameters initializeParameters)
        {
            DidomiFramework.Initialize(initializeParameters);
        }

        public bool IsConsentRequired()
        {
            return DidomiFramework.IsConsentRequired();
        }

        public bool IsNoticeVisible()
        {
            return DidomiFramework.IsNoticeVisible();
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

        public void ShowPreferences(Didomi.Views view)
        {
             DidomiFramework.ShowPreferences(view);
        }

        public bool IsUserConsentStatusPartial()
        {
            return DidomiFramework.IsUserConsentStatusPartial();
        }

        public bool IsUserLegitimateInterestStatusPartial()
        {
            return DidomiFramework.IsUserLegitimateInterestStatusPartial();
        }

        public bool IsUserStatusPartial()
        {
            return DidomiFramework.IsUserStatusPartial();
        }

        public void Reset()
        {
            DidomiFramework.Reset();
        }

        public bool SetUserAgreeToAll()
        {
            return DidomiFramework.SetUserAgreeToAll();
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
             IOSObjectMapper.ConvertFromHashSetStringToJson(enabledConsentPurposeIds),
             IOSObjectMapper.ConvertFromHashSetStringToJson(disabledConsentPurposeIds),
             IOSObjectMapper.ConvertFromHashSetStringToJson(enabledLIPurposeIds),
             IOSObjectMapper.ConvertFromHashSetStringToJson(disabledLIPurposeIds),
             IOSObjectMapper.ConvertFromHashSetStringToJson(enabledConsentVendorIds),
             IOSObjectMapper.ConvertFromHashSetStringToJson(disabledConsentVendorIds),
             IOSObjectMapper.ConvertFromHashSetStringToJson(enabledLIVendorIds),
             IOSObjectMapper.ConvertFromHashSetStringToJson(disabledLIVendorIds));
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

        [ObsoleteAttribute("This method is deprecated. Use shouldUserStatusBeCollected instead.")]
        public bool ShouldConsentBeCollected()
        {
            return DidomiFramework.ShouldConsentBeCollected();
        }

        public bool ShouldUserStatusBeCollected()
        {
            return DidomiFramework.ShouldUserStatusBeCollected();
        }

        public void ShowNotice()
        {
            DidomiFramework.ShowNotice();
        }

        public void UpdateSelectedLanguage(string languageCode)
        {
            DidomiFramework.UpdateSelectedLanguage(languageCode);
        }

        public void SetUser(DidomiUserParameters userParameters)
        {
            string userParametersJson = JsonConvert.SerializeObject(userParameters);
            DidomiFramework.SetUser(userParametersJson);
        }

        public void SetUserAndSetupUI(DidomiUserParameters userParameters)
        {
            string userParametersJson = JsonConvert.SerializeObject(userParameters);
            DidomiFramework.SetUserAndSetupUI(userParametersJson);
        }

        public void ClearUser()
        {
            DidomiFramework.ClearUser();
        }
    }
}
