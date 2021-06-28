
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
        DDMEventTypeNoticeClickMoreInfo = 5,
        DDMEventTypePreferencesClickAgreeToAll = 6,
        DDMEventTypePreferencesClickDisagreeToAll = 7,
        DDMEventTypePreferencesClickPurposeAgree = 8,
        DDMEventTypePreferencesClickPurposeDisagree = 9,
        DDMEventTypePreferencesClickViewVendors = 10,
        DDMEventTypePreferencesClickSaveChoices = 11,
        DDMEventTypePreferencesClickVendorAgree = 12,
        DDMEventTypePreferencesClickVendorDisagree = 13,
        DDMEventTypePreferencesClickVendorSaveChoices = 14,
        DDMEventTypeError = 1000,
    };
}
