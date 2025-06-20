﻿using IO.Didomi.SDK.Events;
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
        void AddVendorStatusListener(string vendorId, DidomiVendorStatusListener vendorStatusListener);
        void RemoveVendorStatusListener(string vendorId);
        string GetJavaScriptForWebView();
        Purpose GetPurpose(string purposeId);
        ISet<string> GetRequiredPurposeIds();
        ISet<Purpose> GetRequiredPurposes();
        ISet<string> GetRequiredVendorIds();
        ISet<Vendor> GetRequiredVendors();
        IDictionary<string, string> GetText(string key);
        string GetTranslatedText(string key);
        CurrentUserStatus GetCurrentUserStatus();
        bool SetCurrentUserStatus(CurrentUserStatus status);
        bool CommitCurrentUserStatusTransaction(
             ISet<string> enabledVendors,
             ISet<string> disabledVendors,
             ISet<string> enabledPurposes,
             ISet<string> disabledPurposes
        );
        UserStatus GetUserStatus();
        string GetApplicableRegulation();
        Vendor GetVendor(string vendorId);
        int GetTotalVendorCount();
        int GetIABVendorCount();
        int GetNonIABVendorCount();
        void HideNotice();
        void HidePreferences();
        void Initialize(DidomiInitializeParameters parameters);
        bool IsConsentRequired();
        bool IsUserConsentStatusPartial();
        bool IsUserLegitimateInterestStatusPartial();
        bool IsUserStatusPartial();
        bool IsNoticeVisible();
        bool IsPreferencesVisible();
        bool IsReady();
        void OnError(Action didomiCallable);
        void OnReady(Action didomiCallable);
        void SetupUI();
        void ShowNotice();
        void ShowPreferences(Didomi.Views view);
        void Reset();
        bool SetUserAgreeToAll();
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
        bool ShouldUserStatusBeCollected();
        void UpdateSelectedLanguage(string languageCode);
        void SetUser(DidomiUserParameters userParameters);
        void SetUserAndSetupUI(DidomiUserParameters userParameters);
        void ClearUser();
    }
}
