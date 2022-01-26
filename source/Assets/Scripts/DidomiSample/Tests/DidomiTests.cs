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

        private MonoBehaviour mono;
        private bool testsComplete = false;
        private bool testsFailure = false;

        // Test events
        private bool noticeDisplayed = false;
        private bool noticeHidden = false;
        private bool preferencesDisplayed = false;
        private bool preferencesHidden = false;
        private bool consentChanged = false;

        public IEnumerator RunAll(MonoBehaviour mono, bool remoteNotice = false)
        {
            this.mono = mono;
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
                if (Application.platform == RuntimePlatform.Android && didomi.IsReady())
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
                        mono.StartCoroutine(RunTests(remoteNotice));
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

        private IEnumerator RunTests(bool remoteNotice)
        {
            var logsBuilder = new StringBuilder(Environment.NewLine);

            AddLogLine(logsBuilder, "Starting tests -");

            // Security wait before beginning tests
            yield return new WaitForSeconds(1);

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
            }
            catch (Exception ex)
            {
                AddLogLine(logsBuilder, $"Exception : {ex.Message }");
                TestFailed(logsBuilder, description: $"Exception : {ex.StackTrace }");
            }

            yield return TestSetupUI(logsBuilder);

            yield return TestShowNotice(logsBuilder);

            yield return TestShowPreferences(logsBuilder);

            yield return TestEventListener(logsBuilder);

            AddLogLine(logsBuilder, "Tests complete.");

            _logs += logsBuilder.ToString();
            testsComplete = true;
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

        private void TestFailed(StringBuilder logs, string description = null)
        {
            string message = description != null ? $"{Fail} - {description}" : Fail;
            AddLogLine(logs, message);
            testsFailure = true;
        }

        private bool AssertEmptiness<T>(StringBuilder logs, ISet<T> element, bool expectContent, string checkedElement)
        {
            if (element != null)
            {
                if ((element.Count > 0) == expectContent)
                {
                    return true;
                }
                TestFailed(logs, description: $"{checkedElement} count = {element.Count}, expected content? {expectContent}");
            } else
            {
                TestFailed(logs, description: $"{checkedElement} is null");
            }
            return false;
        }

        private bool AssertNotEmpty(StringBuilder logs, string element, string failureMessage)
        {
            if (element == null || element.Length == 0)
            {
                TestFailed(logs, description: failureMessage);
                return false;
            }
            return true;
        }

        private bool AssertContains(StringBuilder logs, ISet<string> set, string element, string failureMessage)
        {
            if (!set.Contains(element))
            {
                TestFailed(logs, description: failureMessage);
                return false;
            }
            return true;
        }


        private bool AssertDoesNotContain(StringBuilder logs, ISet<string> set, string element, string failureMessage)
        {
            if (set.Contains(element))
            {
                TestFailed(logs, description: failureMessage);
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
            eventListener.HidePreferences += EventListener_HidePreferences;
            eventListener.ShowPreferences += EventListener_ShowPreferences;

            Didomi.GetInstance().AddEventListener(eventListener);
        }

        private void TestPurposesAndVendorsCountAfterReset(StringBuilder logs)
        {
            AddLogLine(logs, "TestPurposesAndVendorsCountAfterReset ...");

            Didomi.GetInstance().Reset();

            AssertPurposesAndVendorsCount(logs, true, false, false);
        }

        private void AssertPurposesAndVendorsCount(
            StringBuilder logs,
            bool hasRequiredElements,
            bool hasEnabledElements,
            bool hasDisabledElements)
        {

            bool success = true;

            success &= AssertEmptiness(logs, Didomi.GetInstance().GetRequiredPurposeIds(), hasRequiredElements, "requiredPurposeIds");
            success &= AssertEmptiness(logs, Didomi.GetInstance().GetRequiredVendorIds(), hasRequiredElements, "requiredVendorIds");

            success &= AssertEmptiness(logs, Didomi.GetInstance().GetEnabledPurposeIds(), hasEnabledElements, "enabledPurposeIds");
            success &= AssertEmptiness(logs, Didomi.GetInstance().GetEnabledVendorIds(), hasEnabledElements, "enabledVendorIds");

            success &= AssertEmptiness(logs, Didomi.GetInstance().GetDisabledPurposeIds(), hasDisabledElements, "disabledPurposeIds");
            success &= AssertEmptiness(logs, Didomi.GetInstance().GetDisabledVendorIds(), hasDisabledElements, "disabledVendorIds");

            // Vendor and purpose models are not available on iOS
            if (Application.platform == RuntimePlatform.Android)
            {
                success &= AssertEmptiness(logs, Didomi.GetInstance().GetRequiredPurposes(), hasRequiredElements, "requiredPurposes");
                success &= AssertEmptiness(logs, Didomi.GetInstance().GetRequiredVendors(), hasRequiredElements, "requiredVendors");

                success &= AssertEmptiness(logs, Didomi.GetInstance().GetEnabledPurposes(), hasEnabledElements, "enabledPurposes");
                success &= AssertEmptiness(logs, Didomi.GetInstance().GetEnabledVendors(), hasEnabledElements, "enabledVendors");

                success &= AssertEmptiness(logs, Didomi.GetInstance().GetDisabledPurposes(), hasDisabledElements, "disabledPurposes");
                success &= AssertEmptiness(logs, Didomi.GetInstance().GetDisabledVendors(), hasDisabledElements, "disabledVendors");
            }

            if (success)
            {
                TestSucceeded(logs);
            }
        }

        private void TestPurposesAndVendorsCountAfterUserAgreeToAll(StringBuilder logs)
        {
            AddLogLine(logs, "TestPurposesAndVendorsCountAfterUserAgreeToAll ...");

            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetUserAgreeToAll();

            AssertPurposesAndVendorsCount(logs, true, true, false);
        }

        private void TestPurposesAndVendorsCountAfterUserDisagreeToAll(StringBuilder logs)
        {
            AddLogLine(logs, "TestPurposesAndVendorsCountAfterUserDisagreeToAll ...");

            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetUserDisagreeToAll();

            AssertPurposesAndVendorsCount(logs, true, false, true);
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
                if (Application.platform == RuntimePlatform.Android)
                {
                    // Purpose model not available on iOS
                    var purpose = Didomi.GetInstance().GetPurpose(purposeId);

                    if (purpose?.GetId() == purposeId)
                    {
                        TestSucceeded(logs);
                    }
                    else
                    {
                        TestFailed(logs, description: "Purpose not found.");
                    }
                }
                else
                {
                    TestSucceeded(logs);
                }
            }
            else
            {
                TestFailed(logs, description: "Test cannot run. No purpose id found to test");
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
                if (Application.platform == RuntimePlatform.Android)
                {
                    // Vendor model not available on iOS
                    var vendor = Didomi.GetInstance().GetVendor(vendorId);
                    if (vendor?.GetId() == vendorId)
                    {
                        TestSucceeded(logs);
                    }
                    else
                    {
                        TestFailed(logs, description: "Vendor not found.");
                    }
                }
                else
                {
                    TestSucceeded(logs);
                }
            }
            else
            {
                TestFailed(logs, description: "Test cannot run. No vendor id found to test");
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
#pragma warning disable CS0618 // Disable obsolete warning in tests
                var result = Didomi.GetInstance().GetUserConsentStatusForPurpose(purposeId);
#pragma warning restore CS0618

                if (result)
                {
                    TestSucceeded(logs);
                }
                else
                {
                    TestFailed(logs, description: "Purpose not found to set consent.");
                }
            }
            else
            {
                TestFailed(logs, description: "Test cannot run. No purpose id found to test consent");
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
#pragma warning disable CS0618 // Disable obsolete warning in tests
                var result = Didomi.GetInstance().GetUserConsentStatusForVendor(vendorId);
#pragma warning restore CS0618

                if (result)
                {
                    TestSucceeded(logs);
                }
                else
                {
                    TestFailed(logs, description: "Vendor not found to set consent.");
                }
            }
            else
            {
                TestFailed(logs, description: "Test cannot run. No vendor id found to test consent");
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
#pragma warning disable CS0618 // Disable obsolete warning in tests
                var result = Didomi.GetInstance().GetUserConsentStatusForVendorAndRequiredPurposes(vendorId);
#pragma warning restore CS0618

                if (result)
                {
                    TestSucceeded(logs);
                }
                else
                {
                    TestFailed(logs, description: "Vendor not found to set consent for required purposes.");
                }
            }
            else
            {
                TestFailed(logs, description: "Test cannot run. No vendor id found to test consent for required purposes");
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

            if (changed && enabledPurposeIdSet.Count > 0)
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

        private IEnumerator TestSetupUI(StringBuilder logs)
        {
            AddLogLine(logs, "TestSetupUI processing...");

            Didomi.GetInstance().HideNotice();
            Didomi.GetInstance().Reset();
            var success = true;

            yield return new WaitForSeconds(1);
            if (Didomi.GetInstance().IsNoticeVisible())
            {
                TestFailed(logs, "Notice must be invisible after reset call.");
                success = false;
            }

            Didomi.GetInstance().SetupUI();

            yield return new WaitForSeconds(1);
            if (!Didomi.GetInstance().IsNoticeVisible())
            {
                TestFailed(logs, "Notice must be visible after SetupUI call");
                success = false;
            }

            Didomi.GetInstance().HideNotice();
            yield return new WaitForSeconds(1);
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

        private IEnumerator TestShowNotice(StringBuilder logs)
        {
            AddLogLine(logs, "TestShowNotice processing...");

            Didomi.GetInstance().HideNotice();
            Didomi.GetInstance().Reset();
            var success = true;

            yield return new WaitForSeconds(1);
            if (Didomi.GetInstance().IsNoticeVisible())
            {
                TestFailed(logs, "Notice must be invisible after reset call.");
                success = false;
            }

            Didomi.GetInstance().ShowNotice();

            yield return new WaitForSeconds(1);
            if (!Didomi.GetInstance().IsNoticeVisible())
            {
                TestFailed(logs, "Notice must be visible after ShowNotice call");
                success = false;
            }

            Didomi.GetInstance().HideNotice();

            yield return new WaitForSeconds(1);
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

        private IEnumerator TestShowPreferences(StringBuilder logs)
        {
            AddLogLine(logs, "TestShowPreferences processing...");

            Didomi.GetInstance().HidePreferences();
            Didomi.GetInstance().Reset();

            var success = true;

            yield return new WaitForSeconds(1);
            if (Didomi.GetInstance().IsPreferencesVisible())
            {
                TestFailed(logs, "Preferences must be invisible after reset call.");
                success = false;
            }

            Didomi.GetInstance().ShowPreferences();

            yield return new WaitForSeconds(1);
            if (!Didomi.GetInstance().IsPreferencesVisible())
            {
                TestFailed(logs, "Preferences must be visible after ShowPreferences call");
                success = false;
            }

            Didomi.GetInstance().HidePreferences();

            yield return new WaitForSeconds(1);
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

        private IEnumerator TestEventListener(StringBuilder logs)
        {
            AddLogLine(logs, "TestEventsListener processing...");

            Didomi.GetInstance().Reset();

            noticeDisplayed = false;
            noticeHidden = false;
            preferencesDisplayed = false;
            preferencesHidden = false;
            consentChanged = false;

            Didomi.GetInstance().ShowNotice();
            yield return new WaitForSeconds(1);
            var success = CheckEvents(logs, true, false, false, false, false);

            Didomi.GetInstance().HideNotice();
            yield return new WaitForSeconds(1);
            success &= CheckEvents(logs, true, true, false, false, false);

            Didomi.GetInstance().ShowPreferences();
            yield return new WaitForSeconds(1);
            success &= CheckEvents(logs, true, true, true, false, false);

            Didomi.GetInstance().HidePreferences();
            yield return new WaitForSeconds(1);
            success &= CheckEvents(logs, true, true, true, true, false);

            Didomi.GetInstance().SetUserAgreeToAll();
            success &= CheckEvents(logs, true, true, true, true, true);

            if (success)
            {
                TestSucceeded(logs);
            }
        }

        private bool CheckEvents(StringBuilder logs, bool expectNoticeDisplayed, bool expectNoticeHidden, bool expectPreferencesDisplayed, bool expectPreferencesHidden, bool expectConsentChanged)
        {
            bool success = true;

            if(noticeDisplayed != expectNoticeDisplayed)
            {
                TestFailed(logs, "Wrong value for noticeDisplayed event, expected " + expectNoticeDisplayed);
                success = false;
            }
            if (noticeHidden != expectNoticeHidden)
            {
                TestFailed(logs, "Wrong value for noticeHidden event, expected " + expectNoticeHidden);
                success = false;
            }
            if (preferencesDisplayed != expectPreferencesDisplayed)
            {
                TestFailed(logs, "Wrong value for preferencesDisplayed event, expected " + expectPreferencesDisplayed);
                success = false;
            }
            if (preferencesHidden != expectPreferencesHidden)
            {
                TestFailed(logs, "Wrong value for preferencesHidden event, expected " + expectPreferencesHidden);
                success = false;
            }
            if (consentChanged != expectConsentChanged)
            {
                TestFailed(logs, "Wrong value for consentChanged event, expected " + expectConsentChanged);
                success = false;
            }

            return success;
        }


        private void EventListener_Ready(object sender, ReadyEvent e)
        {
            
        }

        private void EventListener_ShowNotice(object sender, ShowNoticeEvent e)
        {
            noticeDisplayed = true;
        }

        private void EventListener_HidePreferences(object sender, HidePreferencesEvent e)
        {
            preferencesHidden = true;
        }

        private void EventListener_ShowPreferences(object sender, ShowPreferencesEvent e)
        {
            preferencesDisplayed = true;
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
            noticeHidden = true;
        }

        private void EventListener_ConsentChanged(object sender, ConsentChangedEvent e)
        {
            consentChanged = true;
        }
    }
}
