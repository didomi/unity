
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
        DDMEventTypeSyncDone = 28,
        DDMEventTypeSyncReady = 29,
        DDMEventTypeSyncError = 30,
        DDMEventTypeLanguageUpdated = 31,
        DDMEventTypeLanguageUpdateFailed = 32,
        DDMEventTypeNoticeClickViewSPIPurposes = 33,
        DDMEventTypePreferencesClickViewSPIPurposes = 34,
        DDMEventTypePreferencesClickSPIPurposeAgree = 35,
        DDMEventTypePreferencesClickSPIPurposeDisagree = 36,
        DDMEventTypePreferencesClickSPICategoryAgree = 37,
        DDMEventTypePreferencesClickSPICategoryDisagree = 38,
        DDMEventTypePreferencesClickSPIPurposeSaveChoices = 39,
        DDMEventTypeError = 1000,
    };
}
