using System;
using UnityEngine;

namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Base class for event listeners passed to our SDK
    /// We defined all event handlers as empty functions so that apps can only override the ones that they can about
    /// </summary>
    public class DidomiEventListener
    {
        public event EventHandler<ConsentChangedEvent> ConsentChanged;
        public event EventHandler<ErrorEvent> Error;
        public event EventHandler<HideNoticeEvent> HideNotice;
        public event EventHandler<ReadyEvent> Ready;
        public event EventHandler<ShowNoticeEvent> ShowNotice;
        public event EventHandler<HidePreferencesEvent> HidePreferences;
        public event EventHandler<ShowPreferencesEvent> ShowPreferences;
        public event EventHandler<NoticeClickAgreeEvent> NoticeClickAgree;
        public event EventHandler<NoticeClickDisagreeEvent> NoticeClickDisagree;
        public event EventHandler<NoticeClickMoreInfoEvent> NoticeClickMoreInfo;
        public event EventHandler<NoticeClickViewSPIPurposesEvent> NoticeClickViewSPIPurposes;
        public event EventHandler<PreferencesClickAgreeToAllEvent> PreferencesClickAgreeToAll;
        public event EventHandler<PreferencesClickDisagreeToAllEvent> PreferencesClickDisagreeToAll;
        public event EventHandler<PreferencesClickPurposeAgreeEvent> PreferencesClickPurposeAgree;
        public event EventHandler<PreferencesClickPurposeDisagreeEvent> PreferencesClickPurposeDisagree;
        public event EventHandler<PreferencesClickCategoryAgreeEvent> PreferencesClickCategoryAgree;
        public event EventHandler<PreferencesClickCategoryDisagreeEvent> PreferencesClickCategoryDisagree;
        public event EventHandler<PreferencesClickViewSPIPurposesEvent> PreferencesClickViewSPIPurposes;
        public event EventHandler<PreferencesClickViewVendorsEvent> PreferencesClickViewVendors;
        public event EventHandler<PreferencesClickSaveChoicesEvent> PreferencesClickSaveChoices;
        public event EventHandler<PreferencesClickSPIPurposeAgreeEvent> PreferencesClickSPIPurposeAgree;
        public event EventHandler<PreferencesClickSPIPurposeDisagreeEvent> PreferencesClickSPIPurposeDisagree;
        public event EventHandler<PreferencesClickSPICategoryAgreeEvent> PreferencesClickSPICategoryAgree;
        public event EventHandler<PreferencesClickSPICategoryDisagreeEvent> PreferencesClickSPICategoryDisagree;
        public event EventHandler<PreferencesClickSPIPurposeSaveChoicesEvent> PreferencesClickSPIPurposeSaveChoices;
        public event EventHandler<PreferencesClickVendorAgreeEvent> PreferencesClickVendorAgree;
        public event EventHandler<PreferencesClickVendorDisagreeEvent> PreferencesClickVendorDisagree;
        public event EventHandler<PreferencesClickVendorSaveChoicesEvent> PreferencesClickVendorSaveChoices;
        public event EventHandler<SyncDoneEvent> SyncDone;
        public event EventHandler<SyncErrorEvent> SyncError;
        public event EventHandler<LanguageUpdatedEvent> LanguageUpdated;
        public event EventHandler<LanguageUpdateFailedEvent> LanguageUpdateFailed;

        public DidomiEventListener() { }

        public void OnConsentChanged(ConsentChangedEvent @event)
        {
            ConsentChanged?.Invoke(this, @event);
            // The consent status of the user has changed
        }

        public void OnError(ErrorEvent @event)
        {
            Error?.Invoke(this, @event);
            // The consent status of the user has changed
        }

        public void OnHideNotice(HideNoticeEvent @event)
        {
            HideNotice?.Invoke(this, @event);
            // The notice is being hidden
        }

        public void OnReady(ReadyEvent @event)
        {
            Ready?.Invoke(this, @event);
            // The Didomi is ready
        }

        public void OnShowNotice(ShowNoticeEvent @event)
        {
            ShowNotice?.Invoke(this, @event);
            // The notice is being shown
        }

        public void OnHidePreferences(HidePreferencesEvent @event)
        {
            HidePreferences?.Invoke(this, @event);
            // The preferences screen is being shown
        }

        public void OnShowPreferences(ShowPreferencesEvent @event)
        {
            ShowPreferences?.Invoke(this, @event);
            // The preferences screen is being shown
        }

        public void OnNoticeClickAgree(NoticeClickAgreeEvent @event)
        {
            NoticeClickAgree?.Invoke(this, @event);
            // Click on agree on notice
        }

        public void OnNoticeClickDisagree(NoticeClickDisagreeEvent @event)
        {
            NoticeClickDisagree?.Invoke(this, @event);
            // Click on disagree on notice
        }

        public void OnNoticeClickViewSPIPurposes(NoticeClickViewSPIPurposesEvent @event)
        {
            NoticeClickViewSPIPurposes?.Invoke(this, @event);
            // Click on sensitive personal information on notice
        }

        public void OnNoticeClickMoreInfo(NoticeClickMoreInfoEvent @event)
        {
            NoticeClickMoreInfo?.Invoke(this, @event);
            // Click on learn more on notice
        }
       
        public void OnPreferencesClickAgreeToAll(PreferencesClickAgreeToAllEvent @event)
        {
            PreferencesClickAgreeToAll?.Invoke(this, @event);
            // Click on agree to all on preferences popup
        }

        public void OnPreferencesClickDisagreeToAll(PreferencesClickDisagreeToAllEvent @event)
        {
            PreferencesClickDisagreeToAll?.Invoke(this, @event);
            // Click on disagree to all on preferences popup
        }


        public void OnPreferencesClickPurposeAgree(PreferencesClickPurposeAgreeEvent @event)
        {
            PreferencesClickPurposeAgree?.Invoke(this, @event);
            // Click on agree to a purpose on preferences popup
        }

        public void OnPreferencesClickPurposeDisagree(PreferencesClickPurposeDisagreeEvent @event)
        {
            PreferencesClickPurposeDisagree?.Invoke(this, @event);
            // Click on disagree to a purpose on preferences popup
        }

        public void OnPreferencesClickCategoryAgree(PreferencesClickCategoryAgreeEvent @event)
        {
            PreferencesClickCategoryAgree?.Invoke(this, @event);
            // Click on agree to a category on preferences popup
        }

        public void OnPreferencesClickCategoryDisagree(PreferencesClickCategoryDisagreeEvent @event)
        {
            PreferencesClickCategoryDisagree?.Invoke(this, @event);
            // Click on disagree to a category on preferences popup
        }

        public void OnPreferencesClickViewSPIPurposes(PreferencesClickViewSPIPurposesEvent @event)
        {
            PreferencesClickViewSPIPurposes?.Invoke(this, @event);
            // Click view sensitive personal information on purposes view on preferences popup
        }

        public void OnPreferencesClickViewVendors(PreferencesClickViewVendorsEvent @event)
        {
            PreferencesClickViewVendors?.Invoke(this, @event);
            // Click view vendors on purposes view on preferences popup
        }

        public void OnPreferencesClickSaveChoices(PreferencesClickSaveChoicesEvent @event)
        {
            PreferencesClickSaveChoices?.Invoke(this, @event);
            // Click on save on the purposes view on preferences popup
        }

        public void OnPreferencesClickSPIPurposeAgree(PreferencesClickSPIPurposeAgreeEvent @event)
        {
            PreferencesClickSPIPurposeAgree?.Invoke(this, @event);
            // Click on agree to a purpose on sensitive personal information screen
        }

        public void OnPreferencesClickSPIPurposeDisagree(PreferencesClickSPIPurposeDisagreeEvent @event)
        {
            PreferencesClickSPIPurposeDisagree?.Invoke(this, @event);
            // Click on disagree to a purpose on sensitive personal information screen
        }

        public void OnPreferencesClickSPICategoryAgree(PreferencesClickSPICategoryAgreeEvent @event)
        {
            PreferencesClickSPICategoryAgree?.Invoke(this, @event);
            // Click on agree to a category on sensitive personal information screen
        }

        public void OnPreferencesClickSPICategoryDisagree(PreferencesClickSPICategoryDisagreeEvent @event)
        {
            PreferencesClickSPICategoryDisagree?.Invoke(this, @event);
            // Click on disagree to a category on sensitive personal information screen
        }

        public void OnPreferencesClickSPIPurposeSaveChoices(PreferencesClickSPIPurposeSaveChoicesEvent @event)
        {
            PreferencesClickSPIPurposeSaveChoices?.Invoke(this, @event);
            // Click on save on sensitive personal information screen
        }

        public void OnPreferencesClickVendorAgree(PreferencesClickVendorAgreeEvent @event)
        {
            PreferencesClickVendorAgree?.Invoke(this, @event);
            // Click on agree to a vendor on preferences popup
        }

        public void OnPreferencesClickVendorDisagree(PreferencesClickVendorDisagreeEvent @event)
        {
            PreferencesClickVendorDisagree?.Invoke(this, @event);
            // Click on disagree to a vendor on preferences popup
        }

        public void OnPreferencesClickVendorSaveChoices(PreferencesClickVendorSaveChoicesEvent @event)
        {
            PreferencesClickVendorSaveChoices?.Invoke(this, @event);
            // Click on save on the vendors view on preferences popup
        }

        public void OnSyncDone(SyncDoneEvent @event)
        {
            SyncDone?.Invoke(this, @event);
            // The user consent was synchronized
        }

        public void OnSyncError(SyncErrorEvent @event)
        {
            SyncError?.Invoke(this, @event);
            // There was an error while synchronizing user consent
        }

        public void OnLanguageUpdated(LanguageUpdatedEvent @event)
        {
            LanguageUpdated?.Invoke(this, @event);
            // Language update was complete
        }

        public void OnLanguageUpdateFailed(LanguageUpdateFailedEvent @event)
        {
            LanguageUpdateFailed?.Invoke(this, @event);
            // Language update was not successful
        }
    }
}
