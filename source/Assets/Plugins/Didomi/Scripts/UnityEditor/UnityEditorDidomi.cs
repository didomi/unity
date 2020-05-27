using IO.Didomi.SDK.Events;
using IO.Didomi.SDK.Interfaces;
using System;
using System.Collections.Generic;

namespace IO.Didomi.SDK.UnityEditor
{
    public class UnityEditorDidomi : IDidomi
    {
        public bool _isInitialized = false;
        private DidomiCallable _didomiCallable = null;

        public void AddEventListener(EventListener eventListener)
        {
            throw new NotImplementedException();
        }

        public ISet<string> GetDisabledPurposeIds()
        {
            throw new NotImplementedException();
        }

        public ISet<Purpose> GetDisabledPurposes()
        {
            throw new NotImplementedException();
        }

        public ISet<string> GetDisabledVendorIds()
        {
            throw new NotImplementedException();
        }

        public ISet<Vendor> GetDisabledVendors()
        {
            throw new NotImplementedException();
        }

        public ISet<string> GetEnabledPurposeIds()
        {
            throw new NotImplementedException();
        }

        public ISet<Purpose> GetEnabledPurposes()
        {
            throw new NotImplementedException();
        }

        public ISet<string> GetEnabledVendorIds()
        {
            throw new NotImplementedException();
        }

        public ISet<Vendor> GetEnabledVendors()
        {
            throw new NotImplementedException();
        }

        public string GetJavaScriptForWebView()
        {
            throw new NotImplementedException();
        }

        public Purpose GetPurpose(string purposeId)
        {
            throw new NotImplementedException();
        }

        public ISet<string> GetRequiredPurposeIds()
        {
            throw new NotImplementedException();
        }

        public ISet<Purpose> GetRequiredPurposes()
        {
            throw new NotImplementedException();
        }

        public ISet<string> GetRequiredVendorIds()
        {
            throw new NotImplementedException();
        }

        public ISet<Vendor> GetRequiredVendors()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> GetText(string key)
        {
            throw new NotImplementedException();
        }

        public string GetTranslatedText(string key)
        {
            throw new NotImplementedException();
        }

        public bool GetUserConsentStatusForPurpose(string purposeId)
        {
            throw new NotImplementedException();
        }

        public bool GetUserConsentStatusForVendor(string vendorId)
        {
            throw new NotImplementedException();
        }

        public bool GetUserConsentStatusForVendorAndRequiredPurposes(string vendorId)
        {
            throw new NotImplementedException();
        }

        public Vendor GetVendor(string vendorId)
        {
            throw new NotImplementedException();
        }

        public void HideNotice()
        {
            throw new NotImplementedException();
        }

        public void HidePreferences()
        {
            throw new NotImplementedException();
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
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;

            if (_didomiCallable != null)
            {
                _didomiCallable.OnCall();
            }
        }

        public bool IsConsentRequired()
        {
            throw new NotImplementedException();
        }

        public bool IsNoticeVisible()
        {
            throw new NotImplementedException();
        }

        public bool IsPreferencesVisible()
        {
            throw new NotImplementedException();
        }

        public bool IsReady()
        {
            return _isInitialized;
        }

        public void OnReady(DidomiCallable didomiCallable)
        {
            _didomiCallable = didomiCallable;

            if (_didomiCallable != null && _isInitialized)
            {
                _didomiCallable.OnCall();
            }
        }

        public void SetupUI()
        {
            throw new NotImplementedException();
        }

        public void ShowPreferences()
        {
            throw new NotImplementedException();
        }

        public bool IsUserConsentStatusPartial()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public bool SetUserAgreeToAll()
        {
            throw new NotImplementedException();
        }

        public bool SetUserConsentStatus(ISet<string> enabledPurposeIds, ISet<string> disabledPurposeIds, ISet<string> enabledVendorIds, ISet<string> disabledVendorIds)
        {
            throw new NotImplementedException();
        }

        public bool SetUserDisagreeToAll()
        {
            throw new NotImplementedException();
        }

        public bool ShouldConsentBeCollected()
        {
            throw new NotImplementedException();
        }

        public void ShowNotice()
        {
            throw new NotImplementedException();
        }

        public void UpdateSelectedLanguage(string languageCode)
        {
            throw new NotImplementedException();
        }
    }
}
