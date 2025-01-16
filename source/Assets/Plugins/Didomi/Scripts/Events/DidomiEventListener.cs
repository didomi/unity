using System;

namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Base class for event listeners passed to our SDK
    /// We defined all event handlers as empty functions so that apps can only override the ones that they can about
    /// </summary>
    public class DidomiEventListener
    {
        /// <summary>
        /// Consent status has changed
        /// </summary>
        public event EventHandler<ConsentChangedEvent> ConsentChanged;
        /// <summary>
        /// An error occurred within the SDK
        /// </summary>
        public event EventHandler<ErrorEvent> Error;
        /// <summary>
        /// Notice has been hidden
        /// </summary>
        public event EventHandler<HideNoticeEvent> HideNotice;
        /// <summary>
        /// The SDK is initialized and ready
        /// Warning: This only gets fired once. Use the `Didomi.onReady` function to make sure your callback always gets executed.
        /// </summary>
        public event EventHandler<ReadyEvent> Ready;
        /// <summary>
        /// The notice is being shown (or needs to be shown)
        /// </summary>
        public event EventHandler<ShowNoticeEvent> ShowNotice;
        /// <summary>
        /// The preferences screen is being hidden
        /// </summary>
        public event EventHandler<HidePreferencesEvent> HidePreferences;
        /// <summary>
        /// The preferences screen is being shown
        /// </summary>
        public event EventHandler<ShowPreferencesEvent> ShowPreferences;
        /// <summary>
        /// Click on agree on notice
        /// </summary>
        public event EventHandler<NoticeClickAgreeEvent> NoticeClickAgree;
        /// <summary>
        /// Click on disagree on notice
        /// </summary>
        public event EventHandler<NoticeClickDisagreeEvent> NoticeClickDisagree;
        /// <summary>
        /// Click on learn more on notice
        /// </summary> 
        public event EventHandler<NoticeClickMoreInfoEvent> NoticeClickMoreInfo;
        /// <summary>
        /// Click on vendors link or button
        /// </summary>
        public event EventHandler<NoticeClickViewVendorsEvent> NoticeClickViewVendors;
        /// <summary>
        /// Click on "Limit the use of my Sensitive Personal Information" on notice
        /// </summary>
        [ObsoleteAttribute("SPI purposes are now displayed in preferences screen. Use NoticeClickMoreInfo instead.")]
        public event EventHandler<NoticeClickViewSPIPurposesEvent> NoticeClickViewSPIPurposes;
        /// <summary>
        /// Click on privacy policy button (TV only)
        /// </summary>
        public event EventHandler<NoticeClickPrivacyPolicyEvent> NoticeClickPrivacyPolicy;
        /// <summary>
        /// Click on agree to all on preferences popup
        /// </summary>
        public event EventHandler<PreferencesClickAgreeToAllEvent> PreferencesClickAgreeToAll;
        /// <summary>
        /// Click on disagree to all on preferences popup
        /// </summary>
        public event EventHandler<PreferencesClickDisagreeToAllEvent> PreferencesClickDisagreeToAll;
        /// <summary>
        /// Click on Purposes tab in preferences screen (TV only)
        /// </summary>
        public event EventHandler<PreferencesClickViewPurposesEvent> PreferencesClickViewPurposes;
        /// <summary>
        /// Toggle to Agree to all Purposes on preferences screen
        /// </summary>
        public event EventHandler<PreferencesClickAgreeToAllPurposesEvent> PreferencesClickAgreeToAllPurposes;
        /// <summary>
        /// Toggle to Disagree to all Purposes on preferences screen
        /// </summary>
        public event EventHandler<PreferencesClickDisagreeToAllPurposesEvent> PreferencesClickDisagreeToAllPurposes;
        /// <summary>
        /// Toggle to Reset all Purposes on preferences screen
        /// </summary>
        public event EventHandler<PreferencesClickResetAllPurposesEvent> PreferencesClickResetAllPurposes;
        /// <summary>
        /// Toggle to agree to a purpose on preferences popup
        /// </summary>
        public event EventHandler<PreferencesClickPurposeAgreeEvent> PreferencesClickPurposeAgree;
        /// <summary>
        /// Toggle to disagree to a purpose on preferences popup
        /// </summary>
        public event EventHandler<PreferencesClickPurposeDisagreeEvent> PreferencesClickPurposeDisagree;
        /// <summary>
        /// Click on agree to a category on preferences popup
        /// </summary>
        public event EventHandler<PreferencesClickCategoryAgreeEvent> PreferencesClickCategoryAgree;
        /// <summary>
        /// Click on disagree to a category on preferences popup
        /// </summary>
        public event EventHandler<PreferencesClickCategoryDisagreeEvent> PreferencesClickCategoryDisagree;
        /// <summary>
        /// Click on "Limit the use of my Sensitive Personal Information" on preferences screen
        /// </summary>
        [ObsoleteAttribute("SPI purposes are now displayed in preferences screen.")]
        public event EventHandler<PreferencesClickViewSPIPurposesEvent> PreferencesClickViewSPIPurposes;
        /// <summary>
        /// Click view vendors on purposes view on preferences popup
        /// </summary>
        public event EventHandler<PreferencesClickViewVendorsEvent> PreferencesClickViewVendors;
        /// <summary>
        /// Click on save on the purposes view on preferences popup
        /// </summary>
        public event EventHandler<PreferencesClickSaveChoicesEvent> PreferencesClickSaveChoices;
        /// <summary>
        /// Click on agree to a purpose on sensitive personal information screen
        /// </summary>
        [ObsoleteAttribute("SPI purposes are now treated as other purposes.Use PreferencesClickPurposeAgree instead.")]
        public event EventHandler<PreferencesClickSPIPurposeAgreeEvent> PreferencesClickSPIPurposeAgree;
        /// <summary>
        /// Click on disagree to a purpose on sensitive personal information screen
        /// </summary>
        [ObsoleteAttribute("SPI purposes are now treated as other purposes.Use PreferencesClickPurposeDisagree instead.")]
        public event EventHandler<PreferencesClickSPIPurposeDisagreeEvent> PreferencesClickSPIPurposeDisagree;
        /// <summary>
        /// Click on agree to a category on sensitive personal information screen
        /// </summary>
        [ObsoleteAttribute("SPI purposes are now treated as other purposes.Use PreferencesClickCategoryAgree instead.")]
        public event EventHandler<PreferencesClickSPICategoryAgreeEvent> PreferencesClickSPICategoryAgree;
        /// <summary>
        /// Click on disagree to a category on sensitive personal information screen
        /// </summary>
        [ObsoleteAttribute("SPI purposes are now treated as other purposes.Use PreferencesClickCategoryDisagree instead.")]
        public event EventHandler<PreferencesClickSPICategoryDisagreeEvent> PreferencesClickSPICategoryDisagree;
        /// <summary>
        /// Click on save on the sensitive personal information screen
        /// </summary>
        [ObsoleteAttribute("SPI purposes are now displayed in preferences screen. Use PreferencesClickSaveChoices instead.")]
        public event EventHandler<PreferencesClickSPIPurposeSaveChoicesEvent> PreferencesClickSPIPurposeSaveChoices;
        /// <summary>
        /// Toggle to Agree to all Vendors on preferences screen
        /// </summary>
        public event EventHandler<PreferencesClickAgreeToAllVendorsEvent> PreferencesClickAgreeToAllVendors;
        /// <summary>
        /// Toggle to Disagree to all Vendors on preferences screen
        /// </summary>
        public event EventHandler<PreferencesClickDisagreeToAllVendorsEvent> PreferencesClickDisagreeToAllVendors;
        /// <summary>
        /// Toggle to agree to a vendor on preferences popup
        /// </summary>
        public event EventHandler<PreferencesClickVendorAgreeEvent> PreferencesClickVendorAgree;
        /// <summary>
        ///  Toggle to disagree to a vendor on preferences popup
        /// </summary>
        public event EventHandler<PreferencesClickVendorDisagreeEvent> PreferencesClickVendorDisagree;
        /// <summary>
        /// Click on save on the vendors view on preferences popup
        /// </summary>
        public event EventHandler<PreferencesClickVendorSaveChoicesEvent> PreferencesClickVendorSaveChoices;
        [ObsoleteAttribute("This event is deprecated. Use SyncReady instead.")]
        public event EventHandler<SyncDoneEvent> SyncDone;
        /// <summary>
        /// Synchronization was done successfully
        /// </summary>
        public event EventHandler<SyncReadyEvent> SyncReady;
        /// <summary>
        /// An error occurred while trying to synchronize
        /// </summary>
        public event EventHandler<SyncErrorEvent> SyncError;
        /// <summary>
        /// The language update is complete
        /// </summary>
        public event EventHandler<LanguageUpdatedEvent> LanguageUpdated;
        /// <summary>
        /// The language update was not completed
        /// </summary>
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

        public void OnNoticeClickViewVendors(NoticeClickViewVendorsEvent @event)
        {
            NoticeClickViewVendors?.Invoke(this, @event);
            // Click on view vendors on notice
        }

        public void OnNoticeClickPrivacyPolicy(NoticeClickPrivacyPolicyEvent @event)
        {
            NoticeClickPrivacyPolicy?.Invoke(this, @event);
            // Click on privacy policy on notice (TV)
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

        public void OnPreferencesClickViewPurposes(PreferencesClickViewPurposesEvent @event)
        {
            PreferencesClickViewPurposes?.Invoke(this, @event);
            // Select purposes tab on preferences screen (TV)
        }

        public void OnPreferencesClickAgreeToAllPurposes(PreferencesClickAgreeToAllPurposesEvent @event)
        {
            PreferencesClickAgreeToAllPurposes?.Invoke(this, @event);
            // Toggle to agree to all purposes on preferences popup
        }

        public void OnPreferencesClickDisagreeToAllPurposes(PreferencesClickDisagreeToAllPurposesEvent @event)
        {
            PreferencesClickDisagreeToAllPurposes?.Invoke(this, @event);
            // Toggle to disagree to all purposes on preferences popup
        }

        public void OnPreferencesClickResetAllPurposes(PreferencesClickResetAllPurposesEvent @event)
        {
            PreferencesClickResetAllPurposes?.Invoke(this, @event);
            // Toggle to reset all purposes on preferences popup
        }

        public void OnPreferencesClickPurposeAgree(PreferencesClickPurposeAgreeEvent @event)
        {
            PreferencesClickPurposeAgree?.Invoke(this, @event);
            // Toggle to agree to a purpose on preferences popup
        }

        public void OnPreferencesClickPurposeDisagree(PreferencesClickPurposeDisagreeEvent @event)
        {
            PreferencesClickPurposeDisagree?.Invoke(this, @event);
            // Toggle to disagree to a purpose on preferences popup
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

        public void OnPreferencesClickAgreeToAllVendors(PreferencesClickAgreeToAllVendorsEvent @event)
        {
            PreferencesClickAgreeToAllVendors?.Invoke(this, @event);
            // Toggle to agree to all vendors on preferences popup
        }

        public void OnPreferencesClickDisagreeToAllVendors(PreferencesClickDisagreeToAllVendorsEvent @event)
        {
            PreferencesClickDisagreeToAllVendors?.Invoke(this, @event);
            // Toggle to disagree to all vendors on preferences popup
        }

        public void OnPreferencesClickVendorAgree(PreferencesClickVendorAgreeEvent @event)
        {
            PreferencesClickVendorAgree?.Invoke(this, @event);
            // Toggle to agree to a vendor on preferences popup
        }

        public void OnPreferencesClickVendorDisagree(PreferencesClickVendorDisagreeEvent @event)
        {
            PreferencesClickVendorDisagree?.Invoke(this, @event);
            // Toggle to disagree to a vendor on preferences popup
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

        public bool OnSyncReady(SyncReadyEvent @event)
        {
            SyncReady?.Invoke(this, @event);
            // The user consent was synchronized
            return SyncReady != null;   // Return false if no listener was present
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
