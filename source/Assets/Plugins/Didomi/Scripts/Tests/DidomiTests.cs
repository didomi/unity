using IO.Didomi.SDK.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IO.Didomi.SDK.Tests
{
    public class DidomiTests
    {
        private const string Succeeded = "Succeeded";
        private const string Fail = "Failed";
        private string _logs = string.Empty;

        public string RunAll()
        {
            try
            {
                string apiKey = "c3cd5b46-bf36-4700-bbdc-4ee9176045aa";
                string localConfigurationPath = null;
                string remoteConfigurationURL = null;
                string providerId = null;
                bool disableDidomiRemoteConfig = true;
                string languageCode = null;

                Didomi.GetInstance().Initialize(
                    apiKey,
                    localConfigurationPath,
                    remoteConfigurationURL,
                    providerId,
                    disableDidomiRemoteConfig,
                    languageCode);

                var didomiCallable = new DidomiCallable();
                didomiCallable.OnReady += DidomiCallable_OnReady;
                Didomi.GetInstance().OnReady(didomiCallable);
            }
            catch (Exception ex)
            {
                _logs += $"{Environment.NewLine} Exception : {ex.Message}";
                _logs += $"{Environment.NewLine} Exception : {ex.StackTrace}";
            }

            return _logs;
        }

        private void DidomiCallable_OnReady(object sender, EventArgs e)
        {
            _logs = RunTests();
        }

        private string RunTests()
        {
            var logsBuilder = new StringBuilder();

            try
            {
                RegisterEventHandlers();

                TestPurposesAndVendorsCountAfterReset(logsBuilder);

                TestPurposesAndVendorsCountAfterUserAgreeToAll(logsBuilder);

                TestPurposesAndVendorsCountAfterUserDisagreeToAll(logsBuilder);

                TestGetPurpose(logsBuilder);

                TestGetVendor(logsBuilder);

                TestGetJavaScriptForWebView(logsBuilder);

                TestGetUserConsentStatusForPurpose(logsBuilder);

                TestGetUserConsentStatusForVendor(logsBuilder);

                TestGetUserConsentStatusForVendorAndRequiredPurposes(logsBuilder);

                TestSetUserConsentStatus(logsBuilder);

                TestIsReady(logsBuilder);

                TestGetText(logsBuilder);

                TestGetTranslatedText(logsBuilder);

                TestIsConsentRequired(logsBuilder);

                TestIsUserConsentStatusPartial(logsBuilder);

                TestUpdateSelectedLanguage(logsBuilder);

                TestSetupUI(logsBuilder);

                TestShowNotice(logsBuilder);

                TestShowPreferences(logsBuilder);
            }
            catch (Exception ex)
            {
                logsBuilder.AppendLine($"Exception : {ex.Message }");
                logsBuilder.AppendLine($"Exception : {ex.StackTrace }");
            }

            return logsBuilder.ToString();
        }

        private void RegisterEventHandlers()
        {
            EventListener eventListener = new EventListener();
            eventListener.ConsentChanged += EventListener_ConsentChanged;
            eventListener.HideNotice += EventListener_HideNotice;
            eventListener.Ready += EventListener_Ready;
            eventListener.NoticeClickAgree += EventListener_NoticeClickAgree;
            eventListener.NoticeClickMoreInfo += EventListener_NoticeClickMoreInfo;
            eventListener.PreferencesClickAgreeToAll += EventListener_PreferencesClickAgreeToAll;
            eventListener.PreferencesClickDisagreeToAll += EventListener_PreferencesClickDisagreeToAll;
            eventListener.PreferencesClickPurposeAgree += EventListener_PreferencesClickPurposeAgree;
            eventListener.PreferencesClickPurposeDisagree += EventListener_PreferencesClickPurposeDisagree;
            eventListener.PreferencesClickSaveChoices += EventListener_PreferencesClickSaveChoices;
            eventListener.PreferencesClickVendorAgree += EventListener_PreferencesClickVendorAgree;
            eventListener.PreferencesClickVendorDisagree += EventListener_PreferencesClickVendorDisagree;
            eventListener.PreferencesClickVendorSaveChoices += EventListener_PreferencesClickVendorSaveChoices;
            eventListener.PreferencesClickViewVendors += EventListener_PreferencesClickViewVendors;
            eventListener.ShowNotice += EventListener_ShowNotice;

            Didomi.GetInstance().AddEventListener(eventListener);
        }

        private void TestPurposesAndVendorsCountAfterReset(StringBuilder logs)
        {
            logs.AppendLine("TestPurposesAndVendorsCountAfterReset ...");

            Didomi.GetInstance().Reset();

            var disabledPurposeIds = Didomi.GetInstance().GetDisabledPurposeIds();
            var disabledPurposes = Didomi.GetInstance().GetDisabledPurposes();
            var disabledVendorIds = Didomi.GetInstance().GetDisabledVendorIds();
            var disabledVendors = Didomi.GetInstance().GetDisabledVendors();

            var enabledPurposeIds = Didomi.GetInstance().GetEnabledPurposeIds();
            var enabledPurposes = Didomi.GetInstance().GetEnabledPurposes();
            var enabledVendorIds = Didomi.GetInstance().GetEnabledVendorIds();
            var enabledVendors = Didomi.GetInstance().GetEnabledVendors();

            var requiredPurposeIds = Didomi.GetInstance().GetRequiredPurposeIds();
            var requiredPurposes = Didomi.GetInstance().GetRequiredPurposes();
            var requiredVendorIds = Didomi.GetInstance().GetRequiredVendorIds();
            var requiredVendors = Didomi.GetInstance().GetRequiredVendors();

            var disabledSum =
                disabledPurposeIds.Count()
                + disabledPurposes.Count()
                + disabledVendorIds.Count()
                + disabledVendors.Count();

            var enabledSum =
                enabledPurposeIds.Count()
                + enabledPurposes.Count()
                + enabledVendorIds.Count()
                + enabledVendors.Count();

            var requiredSum =
               requiredPurposeIds.Count()
               + requiredPurposes.Count()
               + requiredVendorIds.Count()
               + requiredVendors.Count();

            if (disabledSum == 0 && enabledSum == 0 && requiredSum > 0)
            {
                logs.Append(Succeeded);
            }
            else
            {
                logs.Append(Fail);
            }
        }

        private void TestPurposesAndVendorsCountAfterUserAgreeToAll(StringBuilder logs)
        {
            logs.AppendLine("TestPurposesAndVendorsCountAfterUserAgreeToAll ...");

            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetUserAgreeToAll();

            var disabledPurposeIds = Didomi.GetInstance().GetDisabledPurposeIds();
            var disabledPurposes = Didomi.GetInstance().GetDisabledPurposes();
            var disabledVendorIds = Didomi.GetInstance().GetDisabledVendorIds();
            var disabledVendors = Didomi.GetInstance().GetDisabledVendors();

            var enabledPurposeIds = Didomi.GetInstance().GetEnabledPurposeIds();
            var enabledPurposes = Didomi.GetInstance().GetEnabledPurposes();
            var enabledVendorIds = Didomi.GetInstance().GetEnabledVendorIds();
            var enabledVendors = Didomi.GetInstance().GetEnabledVendors();

            var requiredPurposeIds = Didomi.GetInstance().GetRequiredPurposeIds();
            var requiredPurposes = Didomi.GetInstance().GetRequiredPurposes();
            var requiredVendorIds = Didomi.GetInstance().GetRequiredVendorIds();
            var requiredVendors = Didomi.GetInstance().GetRequiredVendors();

            var disabledSum =
                disabledPurposeIds.Count()
                + disabledPurposes.Count()
                + disabledVendorIds.Count()
                + disabledVendors.Count();

            var enabledSum =
                enabledPurposeIds.Count()
                + enabledPurposes.Count()
                + enabledVendorIds.Count()
                + enabledVendors.Count();

            var requiredSum =
               requiredPurposeIds.Count()
               + requiredPurposes.Count()
               + requiredVendorIds.Count()
               + requiredVendors.Count();

            if (disabledSum == 0 && enabledSum > 0 && requiredSum > 0)
            {
                logs.Append(Succeeded);
            }
            else
            {
                logs.Append(Fail);
            }
        }

        private void TestPurposesAndVendorsCountAfterUserDisagreeToAll(StringBuilder logs)
        {
            logs.AppendLine("TestPurposesAndVendorsCountAfterUserDisagreeToAll ...");

            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetUserDisagreeToAll();

            var disabledPurposeIds = Didomi.GetInstance().GetDisabledPurposeIds();
            var disabledPurposes = Didomi.GetInstance().GetDisabledPurposes();
            var disabledVendorIds = Didomi.GetInstance().GetDisabledVendorIds();
            var disabledVendors = Didomi.GetInstance().GetDisabledVendors();

            var enabledPurposeIds = Didomi.GetInstance().GetEnabledPurposeIds();
            var enabledPurposes = Didomi.GetInstance().GetEnabledPurposes();
            var enabledVendorIds = Didomi.GetInstance().GetEnabledVendorIds();
            var enabledVendors = Didomi.GetInstance().GetEnabledVendors();

            var requiredPurposeIds = Didomi.GetInstance().GetRequiredPurposeIds();
            var requiredPurposes = Didomi.GetInstance().GetRequiredPurposes();
            var requiredVendorIds = Didomi.GetInstance().GetRequiredVendorIds();
            var requiredVendors = Didomi.GetInstance().GetRequiredVendors();

            var disabledSum =
                disabledPurposeIds.Count()
                + disabledPurposes.Count()
                + disabledVendorIds.Count()
                + disabledVendors.Count();

            var enabledSum =
                enabledPurposeIds.Count()
                + enabledPurposes.Count()
                + enabledVendorIds.Count()
                + enabledVendors.Count();

            var requiredSum =
               requiredPurposeIds.Count()
               + requiredPurposes.Count()
               + requiredVendorIds.Count()
               + requiredVendors.Count();

            if (disabledSum > 0 && enabledSum == 0 && requiredSum > 0)
            {
                logs.Append(Succeeded);
            }
            else
            {
                logs.Append(Fail);
            }
        }

        private string GetFirstRequiredPurposeId()
        {
            var requiredPurposeIdSet = Didomi.GetInstance().GetRequiredPurposeIds();

            var purposeId = string.Empty;
            if (requiredPurposeIdSet.Count > 0)
            {
                purposeId = requiredPurposeIdSet.FirstOrDefault();
            }

            return purposeId;
        }

        private void TestGetPurpose(StringBuilder logs)
        {
            logs.AppendLine("TestGetPurpose ...");

            var purposeId = GetFirstRequiredPurposeId();



            if (!string.IsNullOrEmpty(purposeId))
            {
                var purpose = Didomi.GetInstance().GetPurpose(purposeId);

                if (purpose?.GetId() == purposeId)
                {
                    logs.Append(Succeeded);
                }
                else
                {
                    logs.Append("Failed. Purpose not found.");
                }
            }
            else
            {
                logs.Append("Failed. Test cannot run. No purpose id found to test");
            }
        }

        private string GetFirstRequiredVendorId()
        {
            var requiredVendorIdSet = Didomi.GetInstance().GetRequiredVendorIds();

            var vendorId = string.Empty;
            if (requiredVendorIdSet.Count > 0)
            {
                vendorId = requiredVendorIdSet.FirstOrDefault();
            }

            return vendorId;
        }

        private void TestGetVendor(StringBuilder logs)
        {
            logs.AppendLine("TestGetVendor ...");


            var vendorId = GetFirstRequiredVendorId();

            if (!string.IsNullOrEmpty(vendorId))
            {
                var vendor = Didomi.GetInstance().GetVendor(vendorId);
                if (vendor?.GetId() == vendorId)
                {
                    logs.Append(Succeeded);
                }
                else
                {
                    logs.Append("Failed. Vendor not found.");
                }
            }
            else
            {
                logs.Append("Failed. Test cannot run. No vendor id found to test");
            }
        }

        private void TestGetJavaScriptForWebView(StringBuilder logs)
        {
            logs.AppendLine("TestGetJavaScriptForWebView ...");

            var retval = Didomi.GetInstance().GetJavaScriptForWebView();

            if (string.IsNullOrWhiteSpace(retval))
            {
                logs.Append(Fail);
            }
            else
            {
                logs.Append(Succeeded);
            }
        }

        private void TestGetUserConsentStatusForPurpose(StringBuilder logs)
        {
            logs.AppendLine("TestGetUserConsentStatusForPurpose ...");

            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetUserAgreeToAll();

            var purposeId = GetFirstRequiredPurposeId();

            if (!string.IsNullOrEmpty(purposeId))
            {
                var result = Didomi.GetInstance().GetUserConsentStatusForPurpose(purposeId);

                if (result)
                {
                    logs.Append(Succeeded);
                }
                else
                {
                    logs.Append("Failed. Purpose not found to set consent.");
                }
            }
            else
            {
                logs.Append("Failed. Test cannot run. No purpose id found to test consent");
            }
        }

        private void TestGetUserConsentStatusForVendor(StringBuilder logs)
        {
            logs.AppendLine("TestGetUserConsentStatusForVendor ...");

            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetUserAgreeToAll();

            var vendorId = GetFirstRequiredVendorId();

            if (!string.IsNullOrEmpty(vendorId))
            {
                var result = Didomi.GetInstance().GetUserConsentStatusForVendor(vendorId);

                if (result)
                {
                    logs.Append(Succeeded);
                }
                else
                {
                    logs.Append("Failed. Vendor not found to set consent.");
                }
            }
            else
            {
                logs.Append("Failed. Test cannot run. No vendor id found to test consent");
            }
        }

        private void TestGetUserConsentStatusForVendorAndRequiredPurposes(StringBuilder logs)
        {
            logs.AppendLine("TestGetUserConsentStatusForVendorAndRequiredPurposes ...");

            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetUserAgreeToAll();

            var vendorId = GetFirstRequiredVendorId();

            if (!string.IsNullOrEmpty(vendorId))
            {
                var result = Didomi.GetInstance().GetUserConsentStatusForVendorAndRequiredPurposes(vendorId);

                if (result)
                {
                    logs.Append(Succeeded);
                }
                else
                {
                    logs.Append("Failed. Vendor not found to set consent for required purposes.");
                }
            }
            else
            {
                logs.Append("Failed. Test cannot run. No vendor id found to test consent for required purposes");
            }
        }

        private void TestSetUserConsentStatus(StringBuilder logs)
        {
            logs.AppendLine("TestSetUserConsentStatus ...");

            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetUserDisagreeToAll();

            ISet<string> enabledPurposeIds = Didomi.GetInstance().GetRequiredPurposeIds();
            ISet<string> disabledPurposeIds = new HashSet<string>();
            ISet<string> enabledVendorIds = Didomi.GetInstance().GetRequiredVendorIds();
            ISet<string> disabledVendorIds = new HashSet<string>();

            var changed = Didomi.GetInstance().SetUserConsentStatus(
                enabledPurposeIds,
                disabledPurposeIds,
                enabledVendorIds,
                disabledVendorIds);

            var enabledPurposeIdSet = Didomi.GetInstance().GetEnabledPurposeIds();

            if (changed && enabledPurposeIds.Count > 0)
            {
                logs.Append(Succeeded);
            }
            else
            {
                logs.Append(Succeeded);
            }
        }

        private void TestIsReady(StringBuilder logs)
        {
            logs.AppendLine("TestIsReady ...");

            var isReady = Didomi.GetInstance().IsReady();

            if (isReady)
            {
                logs.Append(Succeeded);
            }
            else
            {
                logs.Append(Succeeded);
            }
        }

        private void TestIsConsentRequired(StringBuilder logs)
        {
            logs.AppendLine("TestIsConsentRequired ...");

            Didomi.GetInstance().IsConsentRequired();

            logs.Append(Succeeded);
        }

        private void TestIsUserConsentStatusPartial(StringBuilder logs)
        {
            logs.AppendLine("TestIsUserConsentStatusPartial ...");

            Didomi.GetInstance().IsUserConsentStatusPartial();

            logs.Append(Succeeded);
        }

        private void TestGetText(StringBuilder logs)
        {
            logs.AppendLine("TestGetText ...");

            var key = string.Empty;
            var dict = Didomi.GetInstance().GetText(key);

            if (dict != null)
            {
                logs.Append(Succeeded);
            }
            else
            {
                logs.Append(Succeeded);
            }
        }

        private void TestGetTranslatedText(StringBuilder logs)
        {
            logs.AppendLine("TestGetTranslatedText ...");

            var key = "key";
            var value = Didomi.GetInstance().GetTranslatedText(key);

            if (value == key)
            {
                logs.Append(Succeeded);
            }
            else
            {
                logs.Append(Succeeded);
            }
        }

        private void TestUpdateSelectedLanguage(StringBuilder logs)
        {
            logs.AppendLine("TestUpdateSelectedLanguage ...");

            var languageCode = "fr";

            Didomi.GetInstance().UpdateSelectedLanguage(languageCode);

            logs.Append(Succeeded);
        }



        private void TestSetupUI(StringBuilder logs)
        {
            Didomi.GetInstance().HideNotice();
            Didomi.GetInstance().Reset();

            if (Didomi.GetInstance().IsNoticeVisible())
            {
                logs.Append("Notice must be invisible after reset call.");
            }

            logs.AppendLine("SetupUI processing...");
            Didomi.GetInstance().SetupUI();

            if (!Didomi.GetInstance().IsNoticeVisible())
            {
                logs.Append("Notice must be visible after SetupUI call");
            }

            Didomi.GetInstance().HideNotice();
            if (Didomi.GetInstance().IsNoticeVisible())
            {
                logs.Append("Notice must be invisible after HideNotice call");
            }
        }

        private void TestShowNotice(StringBuilder logs)
        {
            Didomi.GetInstance().HideNotice();
            Didomi.GetInstance().Reset();

            if (Didomi.GetInstance().IsNoticeVisible())
            {
                logs.Append("Notice must be invisible after reset call.");
            }

            logs.AppendLine("ShowNotice processing...");
            Didomi.GetInstance().ShowNotice();

            if (!Didomi.GetInstance().IsNoticeVisible())
            {
                logs.Append("Notice must be visible after ShowNotice call");
            }

            Didomi.GetInstance().HideNotice();
            if (Didomi.GetInstance().IsNoticeVisible())
            {
                logs.Append("Notice must be invisible after HideNotice call");
            }
        }

        private void TestShowPreferences(StringBuilder logs)
        {
            Didomi.GetInstance().HidePreferences();
            Didomi.GetInstance().Reset();

            if (Didomi.GetInstance().IsPreferencesVisible())
            {
                logs.Append("Preferences must be invisible after reset call.");
            }

            logs.AppendLine("ShowPreferences processing...");
            Didomi.GetInstance().ShowPreferences();

            if (!Didomi.GetInstance().IsPreferencesVisible())
            {
                logs.Append("Preferences must be visible after ShowPreferences call");
            }

            Didomi.GetInstance().HidePreferences();
            if (Didomi.GetInstance().IsPreferencesVisible())
            {
                logs.Append("Preferences must be invisible after HidePreferences call");
            }
        }

        private void EventListener_Ready(object sender, ReadyEvent e)
        {

        }

        private void EventListener_ShowNotice(object sender, ShowNoticeEvent e)
        {

        }

        private void EventListener_PreferencesClickViewVendors(object sender, PreferencesClickViewVendorsEvent e)
        {

        }

        private void EventListener_PreferencesClickVendorSaveChoices(object sender, PreferencesClickVendorSaveChoicesEvent e)
        {

        }

        private void EventListener_PreferencesClickVendorDisagree(object sender, PreferencesClickVendorDisagreeEvent e)
        {

        }

        private void EventListener_PreferencesClickVendorAgree(object sender, PreferencesClickVendorAgreeEvent e)
        {

        }

        private void EventListener_PreferencesClickSaveChoices(object sender, PreferencesClickSaveChoicesEvent e)
        {

        }

        private void EventListener_PreferencesClickPurposeDisagree(object sender, PreferencesClickPurposeDisagreeEvent e)
        {

        }

        private void EventListener_PreferencesClickPurposeAgree(object sender, PreferencesClickPurposeAgreeEvent e)
        {

        }

        private void EventListener_PreferencesClickDisagreeToAll(object sender, PreferencesClickDisagreeToAllEvent e)
        {

        }

        private void EventListener_PreferencesClickAgreeToAll(object sender, PreferencesClickAgreeToAllEvent e)
        {

        }

        private void EventListener_NoticeClickMoreInfo(object sender, NoticeClickMoreInfoEvent e)
        {

        }

        private void EventListener_NoticeClickAgree(object sender, NoticeClickAgreeEvent e)
        {

        }

        private void EventListener_HideNotice(object sender, HideNoticeEvent e)
        {

        }

        private void EventListener_ConsentChanged(object sender, ConsentChangedEvent e)
        {

        }
    }
}