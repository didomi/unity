using IO.Didomi.SDK.Events;
using System;
using UnityEngine;

namespace IO.Didomi.SDK.Android
{
    /// <summary>
    /// Base class for event listeners passed to our SDK
    /// We defined all event handlers as empty functions so that apps can only override the ones that they can about
    /// Class used to represent an AndroidJavaProxy object for <io.didomi.sdk.functionalinterfaces.DidomiEventListener> interface. 
    /// With EventListenerProxy we are able to give an event handler DidomiEventListener to Didomi-Android-Sdk.
    /// When new event is added to DidomiEventListener, this file must be updated.
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
            var consentChangedEvent = ConvertToConsentChangedEvent(@event);

            _eventListener.OnConsentChanged(consentChangedEvent);
            // The consent status of the user has changed
        }

        public void hideNotice(AndroidJavaObject @event)
        {
            var hideNoticeEvent = ConvertToHideNoticeEvent(@event);

            _eventListener.OnHideNotice(hideNoticeEvent);
            
            // The notice is being hidden
        }

        public void ready(AndroidJavaObject @event)
        {
            var readyEvent = ConvertToReadyEvent(@event);

            _eventListener.OnReady(readyEvent);

            // The notice is being hidden
        }

        public void showNotice(AndroidJavaObject @event)
        {
            var showNoticeEvent = ConvertToShowNoticeEvent(@event);

            _eventListener.OnShowNotice(showNoticeEvent);
            // The notice is being shown
        }
       
        public void noticeClickAgree(AndroidJavaObject @event)
        {
            var noticeClickAgreeEvent = ConvertToNoticeClickAgreeEvent(@event);

            _eventListener.OnNoticeClickAgree(noticeClickAgreeEvent);
            // Click on agree on notice
        }
        
        public void noticeClickMoreInfo(AndroidJavaObject @event)
        {
            var noticeClickMoreInfoEvent = ConvertToNoticeClickMoreInfoEvent(@event);

            _eventListener.OnNoticeClickMoreInfo(noticeClickMoreInfoEvent);
            // Click on learn more on notice
        }
      
        public void preferencesClickAgreeToAll(AndroidJavaObject @event)
        {
            var preferencesClickAgreeToAllEvent = ConvertToPreferencesClickAgreeToAllEvent(@event);

            _eventListener.OnPreferencesClickAgreeToAll(preferencesClickAgreeToAllEvent);
            // Click on agree to all on preferences popup
        }
       
        public void preferencesClickDisagreeToAll(AndroidJavaObject @event)
        {
            var preferencesClickDisagreeToAllEvent = ConvertToPreferencesClickDisagreeToAllEvent(@event);

            _eventListener.OnPreferencesClickDisagreeToAll(preferencesClickDisagreeToAllEvent);
            // Click on disagree to all on preferences popup
        }
       
        public void preferencesClickPurposeAgree(AndroidJavaObject @event)
        {
            var preferencesClickPurposeAgreeEvent = ConvertToPreferencesClickPurposeAgreeEvent(@event);

            _eventListener.OnPreferencesClickPurposeAgree(preferencesClickPurposeAgreeEvent);

            string vendorId = preferencesClickPurposeAgreeEvent.getPurposeId();
            // Click on agree to a purpose on preferences popup
        }
        
        public void preferencesClickPurposeDisagree(AndroidJavaObject @event)
        {
            var preferencesClickPurposeDisagreeEvent = ConvertToPreferencesClickPurposeDisagreeEvent(@event);

            _eventListener.OnPreferencesClickPurposeDisagree(preferencesClickPurposeDisagreeEvent);

            string vendorId = preferencesClickPurposeDisagreeEvent.getPurposeId();
            // Click on disagree to a purpose on preferences popup
        }
        
        public void preferencesClickViewVendors(AndroidJavaObject @event)
        {
            var preferencesClickViewVendorsEvent = ConvertToPreferencesClickViewVendorsEvent(@event);

            _eventListener.OnPreferencesClickViewVendors(preferencesClickViewVendorsEvent);
            // Click view vendors on purposes view on preferences popup
        }
       
        public void preferencesClickSaveChoices(AndroidJavaObject @event)
        {
            var preferencesClickSaveChoicesEvent = ConvertToPreferencesClickSaveChoicesEvent(@event);

            _eventListener.OnPreferencesClickSaveChoices(preferencesClickSaveChoicesEvent);
            // Click on save on the purposes view on preferences popup
        }

        public void preferencesClickVendorAgree(AndroidJavaObject @event)
        {
            var preferencesClickVendorAgreeEvent = ConvertToPreferencesClickVendorAgreeEvent(@event);

            _eventListener.OnPreferencesClickVendorAgree(preferencesClickVendorAgreeEvent);

            string vendorId = preferencesClickVendorAgreeEvent.getVendorId();
            // Click on agree to a vendor on preferences popup
        }
       
        public void preferencesClickVendorDisagree(AndroidJavaObject @event)
        {
            var preferencesClickVendorDisagreeEvent = ConvertToPreferencesClickVendorDisagreeEvent(@event);

            _eventListener.OnPreferencesClickVendorDisagree(preferencesClickVendorDisagreeEvent);

            string vendorId = preferencesClickVendorDisagreeEvent.getVendorId();
            // Click on disagree to a vendor on preferences popup
        }
       
        public void preferencesClickVendorSaveChoices(AndroidJavaObject @event)
        {
            var preferencesClickVendorSaveChoicesEvent = ConvertToPreferencesClickVendorSaveChoicesEvent(@event);

            _eventListener.OnPreferencesClickVendorSaveChoices(preferencesClickVendorSaveChoicesEvent);

            // Click on save on the vendors view on preferences popup
        }

        private static ConsentChangedEvent ConvertToConsentChangedEvent(AndroidJavaObject @event)
        {
            return new ConsentChangedEvent();
        }

        private static HideNoticeEvent ConvertToHideNoticeEvent(AndroidJavaObject @event)
        {
            return new HideNoticeEvent();
        }

        private static ReadyEvent ConvertToReadyEvent(AndroidJavaObject @event)
        {
            return new ReadyEvent();
        }

        private static ShowNoticeEvent ConvertToShowNoticeEvent(AndroidJavaObject @event)
        {
            return new ShowNoticeEvent();
        }

        private static NoticeClickAgreeEvent ConvertToNoticeClickAgreeEvent(AndroidJavaObject @event)
        {
            return new NoticeClickAgreeEvent();
        }

        private static NoticeClickMoreInfoEvent ConvertToNoticeClickMoreInfoEvent(AndroidJavaObject @event)
        {
            return new NoticeClickMoreInfoEvent();
        }

        private static PreferencesClickAgreeToAllEvent ConvertToPreferencesClickAgreeToAllEvent(AndroidJavaObject @event)
        {
            return new PreferencesClickAgreeToAllEvent();
        }

        private static PreferencesClickDisagreeToAllEvent ConvertToPreferencesClickDisagreeToAllEvent(AndroidJavaObject @event)
        {
            return new PreferencesClickDisagreeToAllEvent();
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

        private static PreferencesClickViewVendorsEvent ConvertToPreferencesClickViewVendorsEvent(AndroidJavaObject @event)
        {
            return new PreferencesClickViewVendorsEvent();
        }

        private static PreferencesClickSaveChoicesEvent ConvertToPreferencesClickSaveChoicesEvent(AndroidJavaObject @event)
        {
            return new PreferencesClickSaveChoicesEvent();
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

        private static PreferencesClickVendorSaveChoicesEvent ConvertToPreferencesClickVendorSaveChoicesEvent(AndroidJavaObject @event)
        {
            return new PreferencesClickVendorSaveChoicesEvent();
        }

        private static string GetPurposeId(AndroidJavaObject @event)
        {
           return AndroidObjectMapper.GetMethodStringValue(@event, "getPurposeId");
        }

        private static string GetVendorId(AndroidJavaObject @event)
        {
            return AndroidObjectMapper.GetMethodStringValue(@event, "getVendorId");
        }
    }
}
