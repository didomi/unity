
namespace Assets.Plugins.Scripts.IOS
{
    /// <summary>
    /// List of event types mapping to event types from the iOS SDK.
    /// </summary>
    public enum DDMEventType
    {
        DDMEventTypeConsentChanged = 0,
        DDMEventTypeHideNotice = 1,
        DDMEventTypeReady = 2,
        DDMEventTypeShowNotice = 3,
        DDMEventTypeNoticeClickAgree = 4,
        DDMEventTypeNoticeClickDisagree = 5,
        DDMEventTypeNoticeClickMoreInfo = 6,
        DDMEventTypeNoticeClickViewVendors = 7,
        DDMEventTypeNoticeClickPrivacyPolicy = 8,
        DDMEventTypePreferencesClickAgreeToAll = 9,
        DDMEventTypePreferencesClickDisagreeToAll = 10,
        DDMEventTypePreferencesClickAgreeToAllPurposes = 11,
        DDMEventTypePreferencesClickDisagreeToAllPurposes = 12,
        DDMEventTypePreferencesClickResetAllPurposes = 13,
        DDMEventTypePreferencesClickAgreeToAllVendors = 14,
        DDMEventTypePreferencesClickDisagreeToAllVendors = 15,
        DDMEventTypePreferencesClickPurposeAgree = 16,
        DDMEventTypePreferencesClickPurposeDisagree = 17,
        DDMEventTypePreferencesClickCategoryAgree = 18,
        DDMEventTypePreferencesClickCategoryDisagree = 19,
        DDMEventTypePreferencesClickViewVendors = 20,
        DDMEventTypePreferencesClickViewPurposes = 21,
        DDMEventTypePreferencesClickSaveChoices = 22,
        DDMEventTypePreferencesClickVendorAgree = 23,
        DDMEventTypePreferencesClickVendorDisagree = 24,
        DDMEventTypePreferencesClickVendorSaveChoices = 25,
        DDMEventTypeSyncDone = 26,
        DDMEventTypeError = 1000,
    };
}