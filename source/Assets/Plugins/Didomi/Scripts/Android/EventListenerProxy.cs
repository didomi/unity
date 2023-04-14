using IO.Didomi.SDK.Events;
using System;
using UnityEngine;

namespace IO.Didomi.SDK.Android
{
    /// <summary>
    /// Event listeners
    /// All event handlers are defined as empty functions so that apps can override the ones that they care about.
    /// The `EventListenerProxy` is used to pass an event handler of type `DidomiEventListener` to the Android SDK.
    /// This `AndroidJavaProxy` object maps to the <io.didomi.sdk.functionalinterfaces.DidomiEventListener> interface from the Android SDK.
    /// When a new event type is added to the Android SDK, this file must be updated.
    /// </summary>
    public class EventListenerProxy : AndroidJavaProxy
    {
        private DidomiEventListener _eventListener;

        public EventListenerProxy(DidomiEventListener eventListener) : base("io.didomi.sdk.functionalinterfaces.DidomiEventListener")
        {
            _eventListener = eventListener;
        }

        public void consentChanged(AndroidJavaObject @event)
        {
            _eventListener.OnConsentChanged(new ConsentChangedEvent());
            // The consent status of the user has changed
        }

        public void error(AndroidJavaObject @event)
        {
            var errorEvent = ConvertToErrorEvent(@event);

            _eventListener.OnError(errorEvent);
            // The error occured.
        }

        public void hideNotice(AndroidJavaObject @event)
        {
            _eventListener.OnHideNotice(new HideNoticeEvent());
            // The notice is being hidden
        }

        public void ready(AndroidJavaObject @event)
        {
            _eventListener.OnReady(new ReadyEvent());
            // The notice is being hidden
        }

        public void showNotice(AndroidJavaObject @event)
        {
            _eventListener.OnShowNotice(new ShowNoticeEvent());
            // The notice is being shown
        }

        public void hidePreferences(AndroidJavaObject @event)
        {
            _eventListener.OnHidePreferences(new HidePreferencesEvent());
            // The preferences screen is being hidden
        }

        public void showPreferences(AndroidJavaObject @event)
        {
            _eventListener.OnShowPreferences(new ShowPreferencesEvent());
            // The preferences screen is being shown
        }

        public void noticeClickAgree(AndroidJavaObject @event)
        {
            _eventListener.OnNoticeClickAgree(new NoticeClickAgreeEvent());
            // Click on agree on notice
        }
        
        public void noticeClickDisagree(AndroidJavaObject @event)
        {
            _eventListener.OnNoticeClickDisagree(new NoticeClickDisagreeEvent());
            // Click on disagree on notice
        }

        public void noticeClickViewVendors(AndroidJavaObject @event) { }
        public void noticeClickPrivacyPolicy(AndroidJavaObject @event) { }
        public void preferencesClickViewPurposes(AndroidJavaObject @event) { }
        public void preferencesClickAgreeToAllPurposes(AndroidJavaObject @event) { }
        public void preferencesClickDisagreeToAllPurposes(AndroidJavaObject @event) { }
        public void preferencesClickResetAllPurposes(AndroidJavaObject @event) { }
        public void preferencesClickAgreeToAllVendors(AndroidJavaObject @event) { }
        public void preferencesClickDisagreeToAllVendors(AndroidJavaObject @event) { }

        public void noticeClickMoreInfo(AndroidJavaObject @event)
        {
            _eventListener.OnNoticeClickMoreInfo(new NoticeClickMoreInfoEvent());
            // Click on learn more on notice
        }

        public void noticeClickViewSPIPurposes(AndroidJavaObject @event)
        {
            _eventListener.OnNoticeClickViewSPIPurposes(new NoticeClickViewSPIPurposesEvent());
            // Click on learn more on notice
        }

        public void preferencesClickAgreeToAll(AndroidJavaObject @event)
        {
            _eventListener.OnPreferencesClickAgreeToAll(new PreferencesClickAgreeToAllEvent());
            // Click on agree to all on preferences popup
        }
       
        public void preferencesClickDisagreeToAll(AndroidJavaObject @event)
        {
            _eventListener.OnPreferencesClickDisagreeToAll(new PreferencesClickDisagreeToAllEvent());
            // Click on disagree to all on preferences popup
        }
       
        public void preferencesClickPurposeAgree(AndroidJavaObject @event)
        {
            var preferencesClickPurposeAgreeEvent = ConvertToPreferencesClickPurposeAgreeEvent(@event);

            _eventListener.OnPreferencesClickPurposeAgree(preferencesClickPurposeAgreeEvent);
            // Click on agree to a purpose on preferences popup
        }
        
        public void preferencesClickPurposeDisagree(AndroidJavaObject @event)
        {
            var preferencesClickPurposeDisagreeEvent = ConvertToPreferencesClickPurposeDisagreeEvent(@event);

            _eventListener.OnPreferencesClickPurposeDisagree(preferencesClickPurposeDisagreeEvent);
            // Click on disagree to a purpose on preferences popup
        }

        public void preferencesClickCategoryAgree(AndroidJavaObject @event)
        {
            var preferencesClickCategoryAgreeEvent = ConvertToPreferencesClickCategoryAgreeEvent(@event);

            _eventListener.OnPreferencesClickCategoryAgree(preferencesClickCategoryAgreeEvent);
            // Click on disagree to a category on preferences popup
        }

        public void preferencesClickCategoryDisagree(AndroidJavaObject @event)
        {
            var preferencesClickCategoryDisagreeEvent = ConvertToPreferencesClickCategoryDisagreeEvent(@event);

            _eventListener.OnPreferencesClickCategoryDisagree(preferencesClickCategoryDisagreeEvent);
            // Click on disagree to a category on preferences popup
        }

        public void preferencesClickViewSPIPurposes(AndroidJavaObject @event)
        {
            _eventListener.OnPreferencesClickViewSPIPurposes(new PreferencesClickViewSPIPurposesEvent());
            // Click view SPI on purposes view on preferences popup
        }

        public void preferencesClickViewVendors(AndroidJavaObject @event)
        {
            _eventListener.OnPreferencesClickViewVendors(new PreferencesClickViewVendorsEvent());
            // Click view vendors on purposes view on preferences popup
        }
       
        public void preferencesClickSaveChoices(AndroidJavaObject @event)
        {
            _eventListener.OnPreferencesClickSaveChoices(new PreferencesClickSaveChoicesEvent());
            // Click on save on the purposes view on preferences popup
        }

        public void preferencesClickSPIPurposeAgree(AndroidJavaObject @event)
        {
            var preferencesClickSPIPurposeAgreeEvent = ConvertToPreferencesClickSPIPurposeAgreeEvent(@event);

            _eventListener.OnPreferencesClickSPIPurposeAgree(preferencesClickSPIPurposeAgreeEvent);
            // Click on agree to a purpose on sensitive personal information screen
        }

        public void preferencesClickSPIPurposeDisagree(AndroidJavaObject @event)
        {
            var preferencesClickSPIPurposeDisagreeEvent = ConvertToPreferencesClickSPIPurposeDisagreeEvent(@event);

            _eventListener.OnPreferencesClickSPIPurposeDisagree(preferencesClickSPIPurposeDisagreeEvent);
            // Click on disagree to a purpose on sensitive personal information screen
        }

        public void preferencesClickSPICategoryAgree(AndroidJavaObject @event)
        {
            var preferencesClickSPICategoryAgreeEvent = ConvertToPreferencesClickSPICategoryAgreeEvent(@event);

            _eventListener.OnPreferencesClickSPICategoryAgree(preferencesClickSPICategoryAgreeEvent);
            // Click on agree to a category on sensitive personal information screen
        }

        public void preferencesClickSPICategoryDisagree(AndroidJavaObject @event)
        {
            var preferencesClickSPICategoryDisagreeEvent = ConvertToPreferencesClickSPICategoryDisagreeEvent(@event);

            _eventListener.OnPreferencesClickSPICategoryDisagree(preferencesClickSPICategoryDisagreeEvent);
            // Click on disagree to a category on sensitive personal information screen
        }

        public void preferencesClickSPIPurposeSaveChoices(AndroidJavaObject @event)
        {
            _eventListener.OnPreferencesClickSPIPurposeSaveChoices(new PreferencesClickSPIPurposeSaveChoicesEvent());
            // Click on save on the sensitive personal information screen
        }

        public void preferencesClickVendorAgree(AndroidJavaObject @event)
        {
            var preferencesClickVendorAgreeEvent = ConvertToPreferencesClickVendorAgreeEvent(@event);

            _eventListener.OnPreferencesClickVendorAgree(preferencesClickVendorAgreeEvent);
            // Click on agree to a vendor on preferences popup
        }
       
        public void preferencesClickVendorDisagree(AndroidJavaObject @event)
        {
            var preferencesClickVendorDisagreeEvent = ConvertToPreferencesClickVendorDisagreeEvent(@event);

            _eventListener.OnPreferencesClickVendorDisagree(preferencesClickVendorDisagreeEvent);
            // Click on disagree to a vendor on preferences popup
        }
       
        public void preferencesClickVendorSaveChoices(AndroidJavaObject @event)
        {
            _eventListener.OnPreferencesClickVendorSaveChoices(new PreferencesClickVendorSaveChoicesEvent());
            // Click on save on the vendors view on preferences popup
        }

        public void syncDone(AndroidJavaObject @event)
        {
            var SyncDoneEvent = ConvertToSyncDoneEvent(@event);

            _eventListener.OnSyncDone(SyncDoneEvent);
        }

        public void syncError(AndroidJavaObject @event)
        {
            var SyncErrorEvent = ConvertToSyncErrorEvent(@event);

            _eventListener.OnSyncError(SyncErrorEvent);
        }

        public void languageUpdated(AndroidJavaObject @event)
        {
            var LanguageUpdatedEvent = ConvertToLanguageUpdatedEvent(@event);

            _eventListener.OnLanguageUpdated(LanguageUpdatedEvent);
        }

        public void languageUpdateFailed(AndroidJavaObject @event)
        {
            var LanguageUpdateFailedEvent = ConvertToLanguageUpdateFailedEvent(@event);

            _eventListener.OnLanguageUpdateFailed(LanguageUpdateFailedEvent);
        }

        private static ErrorEvent ConvertToErrorEvent(AndroidJavaObject @event)
        {
            var errorMessage = GetErrorMessage(@event);

            return new ErrorEvent(errorMessage);
        }

        private static string GetErrorMessage(AndroidJavaObject @event)
        {
            return AndroidObjectMapper.GetMethodStringValue(@event, "getErrorMessage");
        }

        private static PreferencesClickPurposeAgreeEvent ConvertToPreferencesClickPurposeAgreeEvent(AndroidJavaObject @event)
        {
            var purposeId = GetPurposeId(@event);

            return new PreferencesClickPurposeAgreeEvent(purposeId);
        }

        private static PreferencesClickPurposeDisagreeEvent ConvertToPreferencesClickPurposeDisagreeEvent(AndroidJavaObject @event)
        {
            var purposeId = GetPurposeId(@event);

            return new PreferencesClickPurposeDisagreeEvent(purposeId);
        }

        private static PreferencesClickCategoryAgreeEvent ConvertToPreferencesClickCategoryAgreeEvent(AndroidJavaObject @event)
        {
            var categoryId = GetCategoryId(@event);

            return new PreferencesClickCategoryAgreeEvent(categoryId);
        }

        private static PreferencesClickCategoryDisagreeEvent ConvertToPreferencesClickCategoryDisagreeEvent(AndroidJavaObject @event)
        {
            var categoryId = GetCategoryId(@event);

            return new PreferencesClickCategoryDisagreeEvent(categoryId);
        }

        private static PreferencesClickSPIPurposeAgreeEvent ConvertToPreferencesClickSPIPurposeAgreeEvent(AndroidJavaObject @event)
        {
            var purposeId = GetPurposeId(@event);

            return new PreferencesClickSPIPurposeAgreeEvent(purposeId);
        }

        private static PreferencesClickSPIPurposeDisagreeEvent ConvertToPreferencesClickSPIPurposeDisagreeEvent(AndroidJavaObject @event)
        {
            var purposeId = GetPurposeId(@event);

            return new PreferencesClickSPIPurposeDisagreeEvent(purposeId);
        }

        private static PreferencesClickSPICategoryAgreeEvent ConvertToPreferencesClickSPICategoryAgreeEvent(AndroidJavaObject @event)
        {
            var categoryId = GetCategoryId(@event);

            return new PreferencesClickSPICategoryAgreeEvent(categoryId);
        }

        private static PreferencesClickSPICategoryDisagreeEvent ConvertToPreferencesClickSPICategoryDisagreeEvent(AndroidJavaObject @event)
        {
            var categoryId = GetCategoryId(@event);

            return new PreferencesClickSPICategoryDisagreeEvent(categoryId);
        }

        private static PreferencesClickVendorAgreeEvent ConvertToPreferencesClickVendorAgreeEvent(AndroidJavaObject @event)
        {
            var vendorId = GetVendorId(@event);

            return new PreferencesClickVendorAgreeEvent(vendorId);
        }

        private static PreferencesClickVendorDisagreeEvent ConvertToPreferencesClickVendorDisagreeEvent(AndroidJavaObject @event)
        {
            var vendorId = GetVendorId(@event);

            return new PreferencesClickVendorDisagreeEvent(vendorId);
        }

        private static SyncDoneEvent ConvertToSyncDoneEvent(AndroidJavaObject @event)
        {
            var organizationUserId = GetOrganizationUserId(@event);

            return new SyncDoneEvent(organizationUserId);
        }

        private static SyncErrorEvent ConvertToSyncErrorEvent(AndroidJavaObject @event)
        {
            var error = GetError(@event);

            return new SyncErrorEvent(error);
        }

        private static LanguageUpdatedEvent ConvertToLanguageUpdatedEvent(AndroidJavaObject @event)
        {
            var languageCode = GetLanguageCode(@event);

            return new LanguageUpdatedEvent(languageCode);
        }

        private static LanguageUpdateFailedEvent ConvertToLanguageUpdateFailedEvent(AndroidJavaObject @event)
        {
            var reason = GetReason(@event);

            return new LanguageUpdateFailedEvent(reason);
        }

        private static string GetPurposeId(AndroidJavaObject @event)
        {
           return AndroidObjectMapper.GetMethodStringValue(@event, "getPurposeId");
        }

        private static string GetCategoryId(AndroidJavaObject @event)
        {
            return AndroidObjectMapper.GetMethodStringValue(@event, "getCategoryId");
        }

        private static string GetVendorId(AndroidJavaObject @event)
        {
            return AndroidObjectMapper.GetMethodStringValue(@event, "getVendorId");
        }

        private static string GetOrganizationUserId(AndroidJavaObject @event)
        {
            return AndroidObjectMapper.GetMethodStringValue(@event, "getOrganizationUserId");
        }

        private static string GetError(AndroidJavaObject @event)
        {
            return AndroidObjectMapper.GetMethodStringValue(@event, "getError");
        }

        private static string GetLanguageCode(AndroidJavaObject @event)
        {
            return AndroidObjectMapper.GetMethodStringValue(@event, "getLanguageCode");
        }

        private static string GetReason(AndroidJavaObject @event)
        {
            return AndroidObjectMapper.GetMethodStringValue(@event, "getReason");
        }
    }
}
