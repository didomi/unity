using IO.Didomi.SDK.Events;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IO.Didomi.SDK.Tests
{
    /// <summary>
    /// Automated tests
    /// </summary>
    public class DidomiTests
    {
        private const string Succeeded = "Succeeded";
        private const string Fail = "! Failed !";
        private string _logs = string.Empty;

        private bool testsComplete = false;
        private bool testsFailure = false;

        public IEnumerator RunAll(bool remoteNotice = false)
        {
            _logs = string.Empty;
            try
            {
                string apiKey = remoteNotice ? "9bf8a7e4-db9a-4ff2-a45c-ab7d2b6eadba" : "c3cd5b46-bf36-4700-bbdc-4ee9176045aa";
                string noticeId = remoteNotice ? "Ar7NPQ72" : null;
                string localConfigurationPath = null;
                string remoteConfigurationURL = null;
                string providerId = null;
                bool disableDidomiRemoteConfig = !remoteNotice;
                string languageCode = null;

                Debug.Log("Tests: Initializing sdk");
                _logs += $"{Environment.NewLine}Initializing sdk - ";

                Didomi didomi = Didomi.GetInstance();
                if (didomi.IsReady())
                {
                    throw new Exception("SDK was already initialized. To run the tests, close and restart the app without initializing SDK.");
                }

                didomi.Initialize(
                    apiKey,
                    localConfigurationPath,
                    remoteConfigurationURL,
                    providerId,
                    disableDidomiRemoteConfig,
                    languageCode,
                    noticeId);

                didomi.OnReady(
                    () => {
                        _logs += RunTests(remoteNotice);
                        testsComplete = true;
                    }
                    );
                didomi.OnError(
                    () => {
                        _logs += $"{Environment.NewLine}Error initializing SDK.";
                        testsComplete = true;
                    }
                    );
            }
            catch (Exception ex)
            {
                _logs += $"{Environment.NewLine}Exception : {ex.Message}";
                _logs += $"{Environment.NewLine}Exception : {ex.StackTrace}";
                testsFailure = true;
                testsComplete = true;
            }

            yield return new WaitUntil(() => testsComplete);
        }

        /// <summary>
        /// Get the tests result log
        /// </summary>
        /// <returns>Results as a multi-line string</returns>
        public string GetResults()
        {
            return _logs;
        }

        /// <summary>
        /// Tests failure status
        /// </summary>
        /// <returns>true if any of the tests failed</returns>
        public bool DidTestsFail()
        {
            return testsFailure;
        }

        private string RunTests(bool remoteNotice)
        {
            var logsBuilder = new StringBuilder(Environment.NewLine);

            AddLogLine(logsBuilder, "Starting tests -");

            try
            {
                testsFailure = false;

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

                TestGetUserStatus(logsBuilder, remoteNotice);

                TestSetupUI(logsBuilder);

                TestShowNotice(logsBuilder);

                TestShowPreferences(logsBuilder);

                AddLogLine(logsBuilder, "Tests complete.");
            }
            catch (Exception ex)
            {
                AddLogLine(logsBuilder, $"Exception : {ex.Message }");
                AddLogLine(logsBuilder, $"Exception : {ex.StackTrace }");
            }

            return logsBuilder.ToString();
        }

        private void AddLogLine(StringBuilder logs, string text)
        {
            Debug.Log(text);
            logs.AppendLine(text);
        }

        private void TestSucceeded(StringBuilder logs)
        {
            AddLogLine(logs, Succeeded);
        }

        private void TestFailed(StringBuilder logs, string text = Fail)
        {
            AddLogLine(logs, text);
            testsFailure = true;
        }

        private bool AssertNotEmpty(StringBuilder logs, string element, string failureMessage)
        {
            if (element == null || element.Length == 0)
            {
                TestFailed(logs, text: $"{Fail} - {failureMessage}");
                return false;
            }
            return true;
        }

        private bool AssertContains(StringBuilder logs, ISet<string> set, string element, string failureMessage)
        {
            if (!set.Contains(element))
            {
                TestFailed(logs, text: $"{Fail} - {failureMessage}");
                return false;
            }
            return true;
        }


        private bool AssertDoesNotContain(StringBuilder logs, ISet<string> set, string element, string failureMessage)
        {
            if (set.Contains(element))
            {
                TestFailed(logs, text: $"{Fail} - {failureMessage}");
                return false;
            }
            return true;
        }

        private void RegisterEventHandlers()
        {
            DidomiEventListener eventListener = new DidomiEventListener();
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
            AddLogLine(logs, "TestPurposesAndVendorsCountAfterReset ...");

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
                TestSucceeded(logs);
            }
            else
            {
                TestFailed(logs);
            }
        }

        private void TestPurposesAndVendorsCountAfterUserAgreeToAll(StringBuilder logs)
        {
            AddLogLine(logs, "TestPurposesAndVendorsCountAfterUserAgreeToAll ...");

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
                TestSucceeded(logs);
            }
            else
            {
                TestFailed(logs);
            }
        }

        private void TestPurposesAndVendorsCountAfterUserDisagreeToAll(StringBuilder logs)
        {
            AddLogLine(logs, "TestPurposesAndVendorsCountAfterUserDisagreeToAll ...");

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
                TestSucceeded(logs);
            }
            else
            {
                TestFailed(logs);
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
            AddLogLine(logs, "TestGetPurpose ...");

            var purposeId = GetFirstRequiredPurposeId();



            if (!string.IsNullOrEmpty(purposeId))
            {
                var purpose = Didomi.GetInstance().GetPurpose(purposeId);

                if (purpose?.GetId() == purposeId)
                {
                    TestSucceeded(logs);
                }
                else
                {
                    TestFailed(logs, text: "Failed. Purpose not found.");
                }
            }
            else
            {
                TestFailed(logs, text: "Failed. Test cannot run. No purpose id found to test");
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
            AddLogLine(logs, "TestGetVendor ...");


            var vendorId = GetFirstRequiredVendorId();

            if (!string.IsNullOrEmpty(vendorId))
            {
                var vendor = Didomi.GetInstance().GetVendor(vendorId);
                if (vendor?.GetId() == vendorId)
                {
                    TestSucceeded(logs);
                }
                else
                {
                    TestFailed(logs, text: "Failed. Vendor not found.");
                }
            }
            else
            {
                TestFailed(logs, text: "Failed. Test cannot run. No vendor id found to test");
            }
        }

        private void TestGetJavaScriptForWebView(StringBuilder logs)
        {
            AddLogLine(logs, "TestGetJavaScriptForWebView ...");

            var retval = Didomi.GetInstance().GetJavaScriptForWebView();

            if (string.IsNullOrWhiteSpace(retval))
            {
                AddLogLine(logs, Fail);
            }
            else
            {
                TestSucceeded(logs);
            }
        }

        private void TestGetUserConsentStatusForPurpose(StringBuilder logs)
        {
            AddLogLine(logs, "TestGetUserConsentStatusForPurpose ...");

            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetUserAgreeToAll();

            var purposeId = GetFirstRequiredPurposeId();

            if (!string.IsNullOrEmpty(purposeId))
            {
                var result = Didomi.GetInstance().GetUserConsentStatusForPurpose(purposeId);

                if (result)
                {
                    TestSucceeded(logs);
                }
                else
                {
                    TestFailed(logs, text: "Failed. Purpose not found to set consent.");
                }
            }
            else
            {
                TestFailed(logs, text: "Failed. Test cannot run. No purpose id found to test consent");
            }
        }

        private void TestGetUserConsentStatusForVendor(StringBuilder logs)
        {
            AddLogLine(logs, "TestGetUserConsentStatusForVendor ...");

            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetUserAgreeToAll();

            var vendorId = GetFirstRequiredVendorId();

            if (!string.IsNullOrEmpty(vendorId))
            {
                var result = Didomi.GetInstance().GetUserConsentStatusForVendor(vendorId);

                if (result)
                {
                    TestSucceeded(logs);
                }
                else
                {
                    TestFailed(logs, text: "Failed. Vendor not found to set consent.");
                }
            }
            else
            {
                TestFailed(logs, text: "Failed. Test cannot run. No vendor id found to test consent");
            }
        }

        private void TestGetUserConsentStatusForVendorAndRequiredPurposes(StringBuilder logs)
        {
            AddLogLine(logs, "TestGetUserConsentStatusForVendorAndRequiredPurposes ...");

            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetUserAgreeToAll();

            var vendorId = GetFirstRequiredVendorId();

            if (!string.IsNullOrEmpty(vendorId))
            {
                var result = Didomi.GetInstance().GetUserConsentStatusForVendorAndRequiredPurposes(vendorId);

                if (result)
                {
                    TestSucceeded(logs);
                }
                else
                {
                    TestFailed(logs, text: "Failed. Vendor not found to set consent for required purposes.");
                }
            }
            else
            {
                TestFailed(logs, text: "Failed. Test cannot run. No vendor id found to test consent for required purposes");
            }
        }

        private void TestSetUserConsentStatus(StringBuilder logs)
        {
            AddLogLine(logs, "TestSetUserConsentStatus ...");

            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetUserDisagreeToAll();

            ISet<string> enabledPurposeIds = Didomi.GetInstance().GetRequiredPurposeIds();
            ISet<string> disabledPurposeIds = new HashSet<string>();
            ISet<string> enabledVendorIds = Didomi.GetInstance().GetRequiredVendorIds();
            ISet<string> disabledVendorIds = new HashSet<string>();

#pragma warning disable CS0618 // Disable obsolete warning in tests
            var changed = Didomi.GetInstance().SetUserConsentStatus(
                enabledPurposeIds,
                disabledPurposeIds,
                enabledVendorIds,
                disabledVendorIds);
#pragma warning restore CS0618

            var enabledPurposeIdSet = Didomi.GetInstance().GetEnabledPurposeIds();

            if (changed && enabledPurposeIds.Count > 0)
            {
                TestSucceeded(logs);
            }
            else
            {
                TestFailed(logs);
            }
        }

        private void TestIsReady(StringBuilder logs)
        {
            AddLogLine(logs, "TestIsReady ...");

            var isReady = Didomi.GetInstance().IsReady();

            if (isReady)
            {
                TestSucceeded(logs);
            }
            else
            {
                TestFailed(logs);
            }
        }

        private void TestIsConsentRequired(StringBuilder logs)
        {
            AddLogLine(logs, "TestIsConsentRequired ...");

            Didomi.GetInstance().IsConsentRequired();

            TestSucceeded(logs);
        }

        private void TestIsUserConsentStatusPartial(StringBuilder logs)
        {
            AddLogLine(logs, "TestIsUserConsentStatusPartial ...");

            Didomi.GetInstance().IsUserConsentStatusPartial();

            TestSucceeded(logs);
        }

        private void TestGetText(StringBuilder logs)
        {
            AddLogLine(logs, "TestGetText ...");

            var key = "notice.content.notice";
            var dict = Didomi.GetInstance().GetText(key);

            if (dict != null)
            {
                TestSucceeded(logs);
            }
            else
            {
                TestFailed(logs);
            }
        }

        private void TestGetTranslatedText(StringBuilder logs)
        {
            AddLogLine(logs, "TestGetTranslatedText ...");

            var key = "notice.content.notice";
            var value = Didomi.GetInstance().GetTranslatedText(key);

            if (!string.IsNullOrWhiteSpace(value))
            {
                TestSucceeded(logs);
            }
            else
            {
                TestFailed(logs);
            }
        }

        private void TestUpdateSelectedLanguage(StringBuilder logs)
        {
            AddLogLine(logs, "TestUpdateSelectedLanguage ...");

            var languageCode = "fr";

            Didomi.GetInstance().UpdateSelectedLanguage(languageCode);

            TestSucceeded(logs);
        }

        private void TestGetUserStatus(StringBuilder logs, bool tcfEnabled)
        {
            AddLogLine(logs, "GetUserStatus processing...");

            // Tested vendor: Adssets AB
            string vendorId = "205";
            string purposeId = "select_personalized_ads";

            Didomi didomi = Didomi.GetInstance();

            bool success = true;

            didomi.SetUserStatus(
                purposesConsentStatus: true,
                purposesLIStatus: true,
                vendorsConsentStatus: true,
                vendorsLIStatus: true
                );

            UserStatus userStatus = didomi.GetUserStatus();

            if (tcfEnabled)
            {
                success &= AssertNotEmpty(logs, userStatus.GetConsentString(), "No consent string");
            }
            success &= AssertNotEmpty(logs, userStatus.GetUserId(), "No user id");
            success &= AssertNotEmpty(logs, userStatus.GetCreated(), "No created date");
            success &= AssertNotEmpty(logs, userStatus.GetUpdated(), "No updated date");

            success &= AssertContains(logs, userStatus.GetVendors().GetGlobal().GetEnabled(), vendorId, "Vendor should be in global.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetGlobal().GetDisabled(), vendorId, "Vendor should not be in global.disabled");

            success &= AssertContains(logs, userStatus.GetVendors().GetGlobalConsent().GetEnabled(), vendorId, "Vendor should be in globalConsent.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetGlobalConsent().GetDisabled(), vendorId, "Vendor should not be in globalConsent.disabled");

            success &= AssertContains(logs, userStatus.GetVendors().GetGlobalLegitimateInterest().GetEnabled(), vendorId, "Vendor should be in globalLegitimateInterest.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetGlobalLegitimateInterest().GetDisabled(), vendorId, "Vendor should not be in globalLegitimateInterest.disabled");

            success &= AssertContains(logs, userStatus.GetVendors().GetConsent().GetEnabled(), vendorId, "Vendor should be in consent.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetConsent().GetDisabled(), vendorId, "Vendor should not be in consent.disabled");

            success &= AssertContains(logs, userStatus.GetVendors().GetLegitimateInterest().GetEnabled(), vendorId, "Vendor should be in LegitimateInterest.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetLegitimateInterest().GetDisabled(), vendorId, "Vendor should not be in legitimateInterest.disabled");

            success &= AssertContains(logs, userStatus.GetPurposes().GetGlobal().GetEnabled(), purposeId, "Purpose should be in global.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetPurposes().GetGlobal().GetDisabled(), purposeId, "Purpose should not be in global.disabled");

            success &= AssertContains(logs, userStatus.GetPurposes().GetConsent().GetEnabled(), purposeId, "Purpose should be in consent.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetPurposes().GetConsent().GetDisabled(), purposeId, "Purpose should not be in consent.disabled");

            success &= AssertContains(logs, userStatus.GetPurposes().GetLegitimateInterest().GetEnabled(), purposeId, "Purpose should be in legitimateInterest.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetPurposes().GetLegitimateInterest().GetDisabled(), purposeId, "Purpose should not be in legitimateInterest.disabled");

            didomi.SetUserStatus(
                purposesConsentStatus: false,
                purposesLIStatus: false,
                vendorsConsentStatus: true,
                vendorsLIStatus: true
                );

            userStatus = didomi.GetUserStatus();

            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetGlobal().GetEnabled(), vendorId, "Vendor should not be in global.enabled");
            success &= AssertContains(logs, userStatus.GetVendors().GetGlobal().GetDisabled(), vendorId, "Vendor should be in global.disabled");

            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetGlobalConsent().GetEnabled(), vendorId, "Vendor should not be in globalConsent.enabled");
            success &= AssertContains(logs, userStatus.GetVendors().GetGlobalConsent().GetDisabled(), vendorId, "Vendor should be in globalConsent.disabled");

            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetGlobalLegitimateInterest().GetEnabled(), vendorId, "Vendor should not be in globalLegitimateInterest.enabled");
            success &= AssertContains(logs, userStatus.GetVendors().GetGlobalLegitimateInterest().GetDisabled(), vendorId, "Vendor should be in globalLegitimateInterest.disabled");

            success &= AssertContains(logs, userStatus.GetVendors().GetConsent().GetEnabled(), vendorId, "Vendor should be in consent.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetConsent().GetDisabled(), vendorId, "Vendor should not be in consent.disabled");

            success &= AssertContains(logs, userStatus.GetVendors().GetLegitimateInterest().GetEnabled(), vendorId, "Vendor should be in LegitimateInterest.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetLegitimateInterest().GetDisabled(), vendorId, "Vendor should not be in legitimateInterest.disabled");

            success &= AssertDoesNotContain(logs, userStatus.GetPurposes().GetGlobal().GetEnabled(), purposeId, "Purpose should not be in global.enabled");
            success &= AssertContains(logs, userStatus.GetPurposes().GetGlobal().GetDisabled(), purposeId, "Purpose should be in global.disabled");

            success &= AssertDoesNotContain(logs, userStatus.GetPurposes().GetConsent().GetEnabled(), purposeId, "Purpose should not be in consent.enabled");
            success &= AssertContains(logs, userStatus.GetPurposes().GetConsent().GetDisabled(), purposeId, "Purpose should be in consent.disabled");

            success &= AssertDoesNotContain(logs, userStatus.GetPurposes().GetLegitimateInterest().GetEnabled(), purposeId, "Purpose should not be in legitimateInterest.enabled");
            success &= AssertContains(logs, userStatus.GetPurposes().GetLegitimateInterest().GetDisabled(), purposeId, "Purpose should be in legitimateInterest.disabled");

            didomi.SetUserStatus(
                purposesConsentStatus: true,
                purposesLIStatus: false,
                vendorsConsentStatus: true,
                vendorsLIStatus: false
                );

            userStatus = didomi.GetUserStatus();

            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetGlobal().GetEnabled(), vendorId, "Vendor should not be in global.enabled");
            success &= AssertContains(logs, userStatus.GetVendors().GetGlobal().GetDisabled(), vendorId, "Vendor should be in global.disabled");

            success &= AssertContains(logs, userStatus.GetVendors().GetGlobalConsent().GetEnabled(), vendorId, "Vendor should be in globalConsent.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetGlobalConsent().GetDisabled(), vendorId, "Vendor should not be in globalConsent.disabled");

            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetGlobalLegitimateInterest().GetEnabled(), vendorId, "Vendor should not be in globalLegitimateInterest.enabled");
            success &= AssertContains(logs, userStatus.GetVendors().GetGlobalLegitimateInterest().GetDisabled(), vendorId, "Vendor should be in globalLegitimateInterest.disabled");

            success &= AssertContains(logs, userStatus.GetVendors().GetConsent().GetEnabled(), vendorId, "Vendor should not be in consent.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetConsent().GetDisabled(), vendorId, "Vendor should be in consent.disabled");

            success &= AssertDoesNotContain(logs, userStatus.GetVendors().GetLegitimateInterest().GetEnabled(), vendorId, "Vendor should not be in LegitimateInterest.enabled");
            success &= AssertContains(logs, userStatus.GetVendors().GetLegitimateInterest().GetDisabled(), vendorId, "Vendor should be in legitimateInterest.disabled");

            success &= AssertContains(logs, userStatus.GetPurposes().GetGlobal().GetEnabled(), purposeId, "Purpose should be in global.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetPurposes().GetGlobal().GetDisabled(), purposeId, "Purpose should not be in global.disabled");

            success &= AssertContains(logs, userStatus.GetPurposes().GetConsent().GetEnabled(), purposeId, "Purpose should be in consent.enabled");
            success &= AssertDoesNotContain(logs, userStatus.GetPurposes().GetConsent().GetDisabled(), purposeId, "Purpose should not be in consent.disabled");

            success &= AssertDoesNotContain(logs, userStatus.GetPurposes().GetLegitimateInterest().GetEnabled(), purposeId, "Purpose should not be in legitimateInterest.enabled");
            success &= AssertContains(logs, userStatus.GetPurposes().GetLegitimateInterest().GetDisabled(), purposeId, "Purpose should be in legitimateInterest.disabled");

            if (success)
            {
                TestSucceeded(logs);
            }
        }

        private void TestSetupUI(StringBuilder logs)
        {
            AddLogLine(logs, "SetupUI processing...");

            Didomi.GetInstance().HideNotice();
            Didomi.GetInstance().Reset();
            var success = true;

            if (Didomi.GetInstance().IsNoticeVisible())
            {
                TestFailed(logs, "Notice must be invisible after reset call.");
                success = false;
            }

            Didomi.GetInstance().SetupUI();

            if (!Didomi.GetInstance().IsNoticeVisible())
            {
                TestFailed(logs, "Notice must be visible after SetupUI call");
                success = false;
            }

            Didomi.GetInstance().HideNotice();
            if (Didomi.GetInstance().IsNoticeVisible())
            {
                TestFailed(logs, "Notice must be invisible after HideNotice call");
                success = false;
            }

            if (success)
            {
                TestSucceeded(logs);
            }
        }

        private void TestShowNotice(StringBuilder logs)
        {
            AddLogLine(logs, "ShowNotice processing...");

            Didomi.GetInstance().HideNotice();
            Didomi.GetInstance().Reset();
            var success = true;


            if (Didomi.GetInstance().IsNoticeVisible())
            {
                TestFailed(logs, "Notice must be invisible after reset call.");
                success = false;
            }

            Didomi.GetInstance().ShowNotice();

            if (!Didomi.GetInstance().IsNoticeVisible())
            {
                TestFailed(logs, "Notice must be visible after ShowNotice call");
                success = false;
            }

            Didomi.GetInstance().HideNotice();
            if (Didomi.GetInstance().IsNoticeVisible())
            {
                TestFailed(logs, "Notice must be invisible after HideNotice call");
                success = false;
            }

            if (success)
            {
                TestSucceeded(logs);
            }
        }

        private void TestShowPreferences(StringBuilder logs)
        {
            AddLogLine(logs, "ShowPreferences processing...");

            Didomi.GetInstance().HidePreferences();
            Didomi.GetInstance().Reset();

            var success = true;

            if (Didomi.GetInstance().IsPreferencesVisible())
            {
                TestFailed(logs, "Preferences must be invisible after reset call.");
                success = false;
            }

            Didomi.GetInstance().ShowPreferences();

            if (!Didomi.GetInstance().IsPreferencesVisible())
            {
                TestFailed(logs, "Preferences must be visible after ShowPreferences call");
                success = false;
            }

            Didomi.GetInstance().HidePreferences();
            if (Didomi.GetInstance().IsPreferencesVisible())
            {
                TestFailed(logs, "Preferences must be invisible after HidePreferences call");
                success = false;
            }

            if (success)
            {
                TestSucceeded(logs);
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