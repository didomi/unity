
namespace Assets.Plugins.Scripts.IOS
{
    /// <summary>
    /// List of event types mapping to event types from the iOS SDK.
    /// Note: The events must be in the same order as EventTypes.swift in the iOS project.
    /// </summary>
    public enum DDMEventType
    {
        DDMEventTypeConsentChanged = 0,
        DDMEventTypeHideNotice = 1,
        DDMEventTypeReady = 2,
        DDMEventTypeShowNotice = 3,
        DDMEventTypeShowPreferences = 4,
        DDMEventTypeHidePreferences = 5,
        DDMEventTypeNoticeClickAgree = 6,
        DDMEventTypeNoticeClickDisagree = 7,
        DDMEventTypeNoticeClickMoreInfo = 8,
        DDMEventTypeNoticeClickViewVendors = 9,
        DDMEventTypeNoticeClickPrivacyPolicy = 10,
        DDMEventTypePreferencesClickAgreeToAll = 11,
        DDMEventTypePreferencesClickDisagreeToAll = 12,
        DDMEventTypePreferencesClickAgreeToAllPurposes = 13,
        DDMEventTypePreferencesClickDisagreeToAllPurposes = 14,
        DDMEventTypePreferencesClickResetAllPurposes = 15,
        DDMEventTypePreferencesClickAgreeToAllVendors = 16,
        DDMEventTypePreferencesClickDisagreeToAllVendors = 17,
        DDMEventTypePreferencesClickPurposeAgree = 18,
        DDMEventTypePreferencesClickPurposeDisagree = 19,
        DDMEventTypePreferencesClickCategoryAgree = 20,
        DDMEventTypePreferencesClickCategoryDisagree = 21,
        DDMEventTypePreferencesClickViewVendors = 22,
        DDMEventTypePreferencesClickViewPurposes = 23,
        DDMEventTypePreferencesClickSaveChoices = 24,
        DDMEventTypePreferencesClickVendorAgree = 25,
        DDMEventTypePreferencesClickVendorDisagree = 26,
        DDMEventTypePreferencesClickVendorSaveChoices = 27,
        DDMEventTypeSyncUserChanged = 28,
        DDMEventTypeSyncDone = 29,
        DDMEventTypeSyncReady = 30,
        DDMEventTypeSyncError = 31,
        DDMEventTypeLanguageUpdated = 32,
        DDMEventTypeLanguageUpdateFailed = 33,
        DDMEventTypeNoticeClickViewSPIPurposes = 34,
        DDMEventTypePreferencesClickViewSPIPurposes = 35,
        DDMEventTypePreferencesClickSPIPurposeAgree = 36,
        DDMEventTypePreferencesClickSPIPurposeDisagree = 37,
        DDMEventTypePreferencesClickSPICategoryAgree = 38,
        DDMEventTypePreferencesClickSPICategoryDisagree = 39,
        DDMEventTypePreferencesClickSPIPurposeSaveChoices = 40,
        DDMEventTypeDCSSignatureError = 41,
        DDMEventTypeDCSSignatureReady = 42,
        DDMEventTypeIntegrationError = 43,
        DDMEventTypeError = 1000,
    };
}
