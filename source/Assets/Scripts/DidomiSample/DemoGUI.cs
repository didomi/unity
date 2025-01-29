using IO.Didomi.SDK;
using IO.Didomi.SDK.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum ViewKind
{
    Basic,
    Detailed
};

public enum FunctionCategory
{
    None,
    Purpose,
    Vendor,
    SetUser,
    SetUserStatus,
    CurrentUserStatusTransaction,
    Consent_3,
    Notice,
    Preferences,
    Language,
    Initialization,
    Events,
    GetCurrentUserStatus
};

public class DemoGUI : MonoBehaviour
{
    private const string NullText = "null";
    
    GameObject labelResult;
    GameObject panel;
    FunctionCategory functionCategory = FunctionCategory.None;
    ViewKind uiStyle = ViewKind.Basic;
    int centerScreenX;
    int centerScreenY;
    int buttonWidth = 0;
    int buttonHeight = 0;
    int yStep = 0;
    int xStep = 0;
    int functionButtonWidth = 0;
    int buttonFontSize = 10;
    string message = "Message Pane";

    void Start()
    {
        labelResult = GameObject.Find("Result");
        panel= GameObject.Find("Panel");
        SetResponsiveLayout();
        InitializeDidomi();
        Didomi.GetInstance().SetupUI();
    }

    void Update()
    {

    }

    void SetResponsiveLayout()
    {
        xStep = 10;
        centerScreenX = Screen.width / 2;
        centerScreenY = Screen.height / 2;

        buttonWidth = Screen.width / 3;
        buttonHeight = Screen.height / 12;

        yStep = buttonHeight + 10;
        functionButtonWidth = (int) (1.5f * buttonWidth);
        RectTransform rt = panel.GetComponent<RectTransform>();
        int panelTop = (int)(8f * yStep);
        int panelbottom = 1 * yStep;
        rt.offsetMax = new Vector2(rt.offsetMax.x, -panelTop);
        buttonFontSize = Screen.width/34;
    }



    private Rect GetGoBackRect() { return new Rect(10, 0.1f * yStep, Screen.width - 20, buttonHeight/2); }

    private Rect GetMiddleRect1() { return new Rect(centerScreenX - buttonWidth * 0.5f, 0.7f * yStep, buttonWidth, buttonHeight); }
    private Rect GetMiddleRect2() { return new Rect(centerScreenX - buttonWidth * 0.5f, 1.7f * yStep, buttonWidth, buttonHeight); }
    private Rect GetMiddleRect3() { return new Rect(centerScreenX - buttonWidth * 0.5f, 2.7f * yStep, buttonWidth, buttonHeight); }
    private Rect GetMiddleRect4() { return new Rect(centerScreenX - buttonWidth * 0.5f, 3.7f * yStep, buttonWidth, buttonHeight); }
    private Rect GetMiddleRect5() { return new Rect(centerScreenX - buttonWidth * 0.5f, 4.7f * yStep, buttonWidth, buttonHeight); }

    private Rect GetRightRect1() { return new Rect(centerScreenX + buttonWidth * 0.5f, 0.7f * yStep, buttonWidth, buttonHeight); }
    private Rect GetRightRect2() { return new Rect(centerScreenX + buttonWidth * 0.5f, 1.7f * yStep, buttonWidth, buttonHeight); }
    private Rect GetRightRect3() { return new Rect(centerScreenX + buttonWidth * 0.5f, 2.7f * yStep, buttonWidth, buttonHeight); }
    private Rect GetRightRect4() { return new Rect(centerScreenX + buttonWidth * 0.5f, 3.7f * yStep, buttonWidth, buttonHeight); }
    private Rect GetRightRect5() { return new Rect(centerScreenX + buttonWidth * 0.5f, 4.7f * yStep, buttonWidth, buttonHeight); }

    private Rect GetLeftRect1() { return new Rect(centerScreenX - buttonWidth * 1.5f, 0.7f * yStep, buttonWidth, buttonHeight); }
    private Rect GetLeftRect2() { return new Rect(centerScreenX - buttonWidth * 1.5f, 1.7f * yStep, buttonWidth, buttonHeight); }
    private Rect GetLeftRect3() { return new Rect(centerScreenX - buttonWidth * 1.5f, 2.7f * yStep, buttonWidth, buttonHeight); }
    private Rect GetLeftRect4() { return new Rect(centerScreenX - buttonWidth * 1.5f, 3.7f * yStep, buttonWidth, buttonHeight); }
    private Rect GetLeftRect5() { return new Rect(centerScreenX - buttonWidth * 1.5f, 4.7f * yStep, buttonWidth, buttonHeight); }

    private Rect GetFuncRect1() { return new Rect(centerScreenX - functionButtonWidth - xStep, 5.7f * yStep, functionButtonWidth, buttonHeight); }
    private Rect GetFuncRect2() { return new Rect(centerScreenX - functionButtonWidth - xStep, 6.7f * yStep, functionButtonWidth, buttonHeight); }
    private Rect GetFuncRect3() { return new Rect(centerScreenX + xStep, 5.7f * yStep, functionButtonWidth, buttonHeight); }
    private Rect GetFuncRect4() { return new Rect(centerScreenX + xStep, 6.7f * yStep, functionButtonWidth, buttonHeight); }

    private void OnGUI()
    {
        GUI.skin.button.fontSize = buttonFontSize;

        if (uiStyle == ViewKind.Basic)
        {
            ShowBasicFunctions();
        }
        else if (uiStyle == ViewKind.Detailed)
        {
            ShowGroups();
        }
       
        var text = labelResult.GetComponent<Text>();
        text.text = message;
    }

    void ShowGroups()
    {
        ShowGroupButtons();
        GUI.enabled = true;
        ShowFunctionButtons();
    }

    void ShowFunctionButtons()
    {
         if (functionCategory == FunctionCategory.Purpose)
        {
            Purpose();
        }
        else if (functionCategory == FunctionCategory.Vendor)
        {
            Vendor();
        }
        else if (functionCategory == FunctionCategory.SetUser)
        {
            SetUser();
        }
        else if (functionCategory == FunctionCategory.SetUserStatus)
        {
            SetUserStatus();
        }
        else if (functionCategory == FunctionCategory.CurrentUserStatusTransaction)
        {
            CurrentUserStatusTransaction();
        }
        else if (functionCategory == FunctionCategory.Consent_3)
        {
            Consent_3();
        }
        else if (functionCategory == FunctionCategory.Notice)
        {
            Notice();
        }
        else if (functionCategory == FunctionCategory.Preferences)
        {
            Preferences();
        }
        else if (functionCategory == FunctionCategory.Language)
        {
            Language();
        }
        else if (functionCategory == FunctionCategory.Initialization)
        {
            Initialization();
        }
        else if (functionCategory == FunctionCategory.Events)
        {
            Events();
        }
        else if (functionCategory == FunctionCategory.GetCurrentUserStatus)
        {
            GetCurrentUserStatus();
        }
    }

    private void ShowGroupButtons()
    {
        if(GUI.Button(GetGoBackRect(), "Go to Basic Functions View"))
        {
            uiStyle = ViewKind.Basic;
        }
      
        if (GUI.Button(GetLeftRect1(), "Notice"))
        {
            functionCategory = FunctionCategory.Notice;
        }

        if (GUI.Button(GetMiddleRect1(), "Preferences"))
        {
            functionCategory = FunctionCategory.Preferences;
        }

        if (GUI.Button(GetLeftRect2(), "Purpose"))
        {
            functionCategory= FunctionCategory.Purpose;
        }

        if (GUI.Button(GetMiddleRect2(), "Vendor"))
        {
            functionCategory = FunctionCategory.Vendor;
        }

        if (GUI.Button(GetLeftRect3(), "SetUser"))
        {
            functionCategory = FunctionCategory.SetUser;
        }

        if (GUI.Button(GetMiddleRect3(), "SetUserStatus"))
        {
            functionCategory = FunctionCategory.SetUserStatus;
        }

        if (GUI.Button(GetRightRect3(), "User Status Transaction"))
        {
            functionCategory = FunctionCategory.CurrentUserStatusTransaction;
        }

        if (GUI.Button(GetLeftRect4(), "Consent"))
        {
            functionCategory = FunctionCategory.Consent_3;
        }

        if (GUI.Button(GetMiddleRect4(), "Language"))
        {
            functionCategory = FunctionCategory.Language;
        }

        if (GUI.Button(GetRightRect4(), "Initialization"))
        {
            functionCategory = FunctionCategory.Initialization;
        }

        if (GUI.Button(GetLeftRect5(), "Events"))
        {
            functionCategory = FunctionCategory.Events;
        }

        if (GUI.Button(GetMiddleRect5(), "GetCurrentUserStatus"))
        {
            functionCategory = FunctionCategory.GetCurrentUserStatus;
        }
    }

    private void Purpose()
    {
        if (GUI.Button(GetFuncRect1(), "GetRequiredPurposes"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().GetRequiredPurposes();
            message = "GetRequiredPurposes" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect2(), "GetRequiredPurposeIds"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().GetRequiredPurposeIds();
            message += "GetRequiredPurposeIds" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect3(), "GetPurpose"))
        {
            message = string.Empty;
            var purposeId = GetFirstRequiredPurposeId();
            var retval = Didomi.GetInstance().GetPurpose(purposeId);
            message += "GetPurpose" + MessageForObject(retval);
        }
    }

    private void Vendor()
    {
        if (GUI.Button(GetFuncRect1(), "GetRequiredVendors"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().GetRequiredVendors();
            message = "GetRequiredVendors" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect2(), "GetVendor"))
        {
            message = string.Empty;
            var vendorId = GetFirstRequiredVendorId();
            var retval = Didomi.GetInstance().GetVendor(vendorId);
            message += "GetVendor" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect3(), "Vendors count"))
        {
            message = string.Empty;
            var total = Didomi.GetInstance().GetTotalVendorCount();
            var iab = Didomi.GetInstance().GetIABVendorCount();
            var nonIab = Didomi.GetInstance().GetNonIABVendorCount();
            message += "Vendors count : Total " + MessageForObject(total) + ", " +
                "IAB " + MessageForObject(iab) + ", " +
                "Non-IAB " + MessageForObject(nonIab);
        }
    }

    private void SetUser()
    {
        if (GUI.Button(GetFuncRect1(), "SetUser with id + setupUI"))
        {
            message = string.Empty;
            Didomi.GetInstance().SetUserAndSetupUI("vwxyz");
            message += "Calling SetUser with id";
        }

        if (GUI.Button(GetFuncRect2(), "SetUser with Hash"))
        {
            message = string.Empty;
            UserAuthParams parameters = new UserAuthWithHashParams("abcd", "algorithm", "secret", "digest", "salt", 3600000L);
            Didomi.GetInstance().SetUser(parameters);
            message += "Calling SetUser with Hash params";
        }

        if (GUI.Button(GetFuncRect3(), "SetUser with encryption"))
        {
            message = string.Empty;
            UserAuthParams parameters = new UserAuthWithEncryptionParams("efgh", "algorithm", "secret", "vector");
            Didomi.GetInstance().SetUser(parameters);
            message += "Calling SetUser with Encryption params";
        }
    }

    private void SetUserStatus()
    {
        if (GUI.Button(GetFuncRect1(), "SetUserAgreeToAll"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().SetUserAgreeToAll();
            message += "SetUserAgreeToAll" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect2(), "SetUserDisagreeToAll"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().SetUserDisagreeToAll();
            message += "SetUserDisagreeToAll" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect3(), "SetUserStatus"))
        {
            message = string.Empty;

            bool purposesConsentStatus = true;
            bool purposesLIStatus = false;
            bool vendorsConsentStatus = false;
            bool vendorsLIStatus = false;

            var retval = Didomi.GetInstance().SetUserStatus(
                purposesConsentStatus,
                purposesLIStatus,
                vendorsConsentStatus,
                vendorsLIStatus);

            message += "SetUserStatus " + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect4(), "SetCurrentUserStatus"))
        {
            message = string.Empty;

            var userStatus = Didomi.GetInstance().GetCurrentUserStatus();
            var purposeId = GetFirstRequiredPurposeId();
            var vendorId = GetFirstRequiredVendorId();

            // Enable all purposes except purposeId
            foreach(CurrentUserStatus.PurposeStatus purposeStatus in userStatus.Purposes.Values) {
                purposeStatus.Enabled = purposeStatus.Id != purposeId;
            }
            // Enable all vendors except vendorId
            foreach(CurrentUserStatus.VendorStatus vendorStatus in userStatus.Vendors.Values) {
                vendorStatus.Enabled = vendorStatus.Id != vendorId;
            }

            var retval = Didomi.GetInstance().SetCurrentUserStatus(userStatus);

            message += "SetCurrentUserStatus " + MessageForObject(retval);
        }
    }

    private void CurrentUserStatusTransaction()
    {
        if (GUI.Button(GetFuncRect1(), "Commit Transaction"))
        {
            var purposeId = GetFirstRequiredPurposeId();
            var vendorId = GetFirstRequiredVendorId();

            message = string.Empty;
            var transaction = Didomi.GetInstance().OpenCurrentUserStatusTransaction();
            var retval = transaction.EnablePurpose(purposeId)
                .DisableVendor(vendorId)
                .Commit();
            message += "Commit CurrentUserStatusTransaction" + MessageForObject(retval);
        }
    }

    private void Consent_3()
    {
        if (GUI.Button(GetFuncRect1(), "ShouldUserStatusBeCollected"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().ShouldUserStatusBeCollected();
            message += "IsConsentRequired" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect2(), "IsUserStatusPartial"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().IsUserStatusPartial();
            message += "IsUserStatusPartial" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect3(), "GetApplicableRegulation"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().GetApplicableRegulation();
            message += "GetApplicableRegulation" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect4(), "GetJavaScriptForWebView"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().GetJavaScriptForWebView();
            message += "GetJavaScriptForWebView" + MessageForObject(retval);
        }
    }

    private void Notice()
    {
        if (GUI.Button(GetFuncRect1(), "SetupUI"))
        {
            message = string.Empty;
            Didomi.GetInstance().Reset();
            Didomi.GetInstance().SetupUI();
            message += "SetupUI";
        }

        if (GUI.Button(GetFuncRect2(), "ShowNotice"))
        {
            message = string.Empty;
            Didomi.GetInstance().Reset();
            Didomi.GetInstance().ShowNotice();
            message += "ShowNotice";
        }

        if (GUI.Button(GetFuncRect3(), "HideNotice"))
        {
            message = string.Empty;
            Didomi.GetInstance().HideNotice();
            message += "HideNotice";
        }

        if (GUI.Button(GetFuncRect4(), "IsNoticeVisible"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().IsNoticeVisible();
            message += "IsNoticeVisible" + MessageForObject(retval);
        }
    }

    private void Preferences()
    {
        if (GUI.Button(GetFuncRect1(), "ShowPreferences"))
        {
            message = string.Empty;
            Didomi.GetInstance().ShowPreferences();
            message += "showPreferences";
        }

        if (GUI.Button(GetFuncRect2(), "ShowPreferences (vendors)"))
        {
            message = string.Empty;
            Didomi.GetInstance().ShowPreferences(Didomi.Views.Vendors);
            message += "showPreferences (vendors)";
        }

        if (GUI.Button(GetFuncRect3(), "HidePreferences"))
        {
            message = string.Empty;
            Didomi.GetInstance().HidePreferences();
            message += "HidePreferences";
        }

        if (GUI.Button(GetFuncRect4(), "IsPreferencesVisible"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().IsPreferencesVisible();
            message += "IsPreferencesVisible" + MessageForObject(retval);
        }
    }

    private void Language()
    {
        if (GUI.Button(GetFuncRect1(), "GetText"))
        {
            message = string.Empty;
            var key = "notice.content.notice";
            var dict = Didomi.GetInstance().GetText(key);
            message += "GetText" + MessageForObject(dict);
        }

        if (GUI.Button(GetFuncRect2(), "GetTranslatedText"))
        {
            message = string.Empty;

            var key = "notice.content.notice";
            var retval = Didomi.GetInstance().GetTranslatedText(key);

            message += "GetTranslatedText" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect3(), "UpdateSelectedLanguage"))
        {
            message = string.Empty;

            var languageCode = "fr";

            Didomi.GetInstance().UpdateSelectedLanguage(languageCode);

            message += "UpdateSelectedLanguage";
        }
    }

    void Initialization()
    {
        if (GUI.Button(GetFuncRect1(), "Initialize"))
        {
            InitializeDidomi();
        }

        if (GUI.Button(GetFuncRect2(), "InitializeLocal"))
        {
            InitializeDidomiLocal();
        }

        if (GUI.Button(GetFuncRect3(), "IsReady"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().IsReady();
            message += "IsReady" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect4(), "Reset"))
        {
            message = string.Empty;
            Didomi.GetInstance().Reset();
            message += "Reset called";
        }
    }

    private void InitializeDidomi()
    {
        message = string.Empty;

        Didomi.GetInstance().OnReady(
              () => { message = "Ready"; }
              );

        #if UNITY_TVOS
            Didomi.GetInstance().Initialize(
                new DidomiInitializeParameters(
                    apiKey: "9bf8a7e4-db9a-4ff2-a45c-ab7d2b6eadba",
                    noticeId: "DirGCFKy",
                    disableDidomiRemoteConfig: false
                )
            );
        #else   // iPhone or Android phone / Android TV
            Didomi.GetInstance().Initialize(
                new DidomiInitializeParameters(
                    apiKey: "9bf8a7e4-db9a-4ff2-a45c-ab7d2b6eadba",
                    noticeId: "Ar7NPQ72",
                    tvNoticeId: "DirGCFKy",
                    androidTvEnabled: true
                )
            );
        #endif
    }

    private void InitializeDidomiLocal()
    {
        message = string.Empty;

        Didomi.GetInstance().OnReady(
              () => { message = "Ready"; }
              );

        Didomi.GetInstance().Initialize(
            new DidomiInitializeParameters(
                apiKey: "9bf8a7e4-db9a-4ff2-a45c-ab7d2b6eadba",
                disableDidomiRemoteConfig: true
            )
        );
    }

    void Events()
    {
        if (GUI.Button(GetFuncRect1(), "AddEventListener"))
        {
            message = string.Empty;
            RegisterEventHandlers();
            message += "AddEventListener";
        }

        if (GUI.Button(GetFuncRect2(), "OnError"))
        {
            message = string.Empty;

            Didomi.GetInstance().OnError(
                   () => { message = "OnError Event Fired."; }
                   );
        }

        if (GUI.Button(GetFuncRect3(), "OnReady"))
        {
            message = string.Empty;

            Didomi.GetInstance().OnReady(
                   () => { message = "OnReady Event Fired."; }
                   );
        }
    }

    void GetCurrentUserStatus()
    {
        if (GUI.Button(GetFuncRect1(), "Purposes"))
        {
            message = string.Empty;
            var userStatus = Didomi.GetInstance().GetCurrentUserStatus();
            message += "Purposes: " +
                userStatus.Purposes.Where(entry => entry.Value.Enabled).Count() + " enabled, " +
                userStatus.Purposes.Where(entry => !entry.Value.Enabled).Count() + "disabled";
        }

        if (GUI.Button(GetFuncRect2(), "Vendors"))
        {
            message = string.Empty;
            var userStatus = Didomi.GetInstance().GetCurrentUserStatus();
            message += "Vendors: " +
                userStatus.Vendors.Where(entry => entry.Value.Enabled).Count() + " enabled, " +
                userStatus.Vendors.Where(entry => !entry.Value.Enabled).Count() + "disabled";
        }

        if (GUI.Button(GetFuncRect3(), "UserId"))
        {
            message = string.Empty;
            var userStatus = Didomi.GetInstance().GetCurrentUserStatus();
            message += "User id = " + userStatus.UserId;
        }

        if (GUI.Button(GetFuncRect4(), "ConsentString"))
        {
            message = string.Empty;
            var userStatus = Didomi.GetInstance().GetCurrentUserStatus();
            message += "ConsentString = " + userStatus.ConsentString;
        }
    }

    private void ShowBasicFunctions()
    {
        if (GUI.Button(GetMiddleRect1(), "ShowNotice"))
        {
            message = string.Empty;
            Didomi.GetInstance().Reset();
            Didomi.GetInstance().ShowNotice();
            message += "ShowNotice";
        }

        if (GUI.Button(GetMiddleRect2(), "ShowPreferences"))
        {
            message = string.Empty;
            Didomi.GetInstance().Reset();
            Didomi.GetInstance().ShowPreferences();
            message += "showPreferences";
        }

        if (GUI.Button(GetMiddleRect3(), "Reset"))
        {
            message = string.Empty;
            Didomi.GetInstance().Reset();
            message += "Reset called";
        }

        if (GUI.Button(GetMiddleRect4(), "More Functions"))
        {
            uiStyle = ViewKind.Detailed;
        }
    }

    private void RegisterEventHandlers()
    {
        DidomiEventListener eventListener = new DidomiEventListener();
        eventListener.Ready += EventListener_Ready;
        eventListener.Error += EventListener_Error;
        eventListener.ConsentChanged += EventListener_ConsentChanged;
        eventListener.NoticeClickAgree += EventListener_NoticeClickAgree;
        eventListener.NoticeClickMoreInfo += EventListener_NoticeClickMoreInfo;
        eventListener.NoticeClickViewVendors += EventListener_NoticeClickViewVendors;
        eventListener.NoticeClickPrivacyPolicy += EventListener_NoticeClickPrivacyPolicy;
        eventListener.ShowNotice += EventListener_ShowNotice;
        eventListener.HideNotice += EventListener_HideNotice;
        eventListener.HidePreferences += EventListener_HidePreferences;
        eventListener.ShowPreferences += EventListener_ShowPreferences;
        eventListener.PreferencesClickAgreeToAll += EventListener_PreferencesClickAgreeToAll;
        eventListener.PreferencesClickDisagreeToAll += EventListener_PreferencesClickDisagreeToAll;
        eventListener.PreferencesClickViewPurposes += EventListener_PreferencesClickViewPurposes;
        eventListener.PreferencesClickAgreeToAllPurposes += EventListener_PreferencesClickAgreeToAllPurposes;
        eventListener.PreferencesClickDisagreeToAllPurposes += EventListener_PreferencesClickDisagreeToAllPurposes;
        eventListener.PreferencesClickResetAllPurposes += EventListener_PreferencesClickResetAllPurposes;
        eventListener.PreferencesClickPurposeAgree += EventListener_PreferencesClickPurposeAgree;
        eventListener.PreferencesClickPurposeDisagree += EventListener_PreferencesClickPurposeDisagree;
        eventListener.PreferencesClickCategoryAgree += EventListener_PreferencesClickCategoryAgree;
        eventListener.PreferencesClickCategoryDisagree += EventListener_PreferencesClickCategoryDisagree;
        eventListener.PreferencesClickViewVendors += EventListener_PreferencesClickViewVendors;
        eventListener.PreferencesClickSaveChoices += EventListener_PreferencesClickSaveChoices;
        eventListener.PreferencesClickDisagreeToAllVendors += EventListener_PreferencesClickDisagreeToAllVendors;
        eventListener.PreferencesClickAgreeToAllVendors += EventListener_PreferencesClickAgreeToAllVendors;
        eventListener.PreferencesClickVendorAgree += EventListener_PreferencesClickVendorAgree;
        eventListener.PreferencesClickVendorDisagree += EventListener_PreferencesClickVendorDisagree;
        eventListener.PreferencesClickVendorSaveChoices += EventListener_PreferencesClickVendorSaveChoices;
        eventListener.SyncReady += EventListener_SyncReady;
        eventListener.SyncError += EventListener_SyncError;
        eventListener.LanguageUpdated += EventListener_LanguageUpdated;
        eventListener.LanguageUpdateFailed += EventListener_LanguageUpdateFailed;

        Didomi.GetInstance().AddEventListener(eventListener);
    }

    private void EventListener_Ready(object sender, ReadyEvent e)
    {
        message += "\nEvent: Ready";
    }

    private void EventListener_Error(object sender, ErrorEvent e)
    {
        message += "\nEvent: Error, Message=" + e.getErrorMessage();
    }

    private void EventListener_ConsentChanged(object sender, ConsentChangedEvent e)
    {
        message += "\nEvent: ConsentChangedEvent";
    }

    private void EventListener_NoticeClickMoreInfo(object sender, NoticeClickMoreInfoEvent e)
    {
        message += "\nEvent: NoticeClickMoreInfo";
    }

    private void EventListener_NoticeClickViewVendors(object sender, NoticeClickViewVendorsEvent e)
    {
        message += "\nEvent: NoticeClickViewVendors";
    }

    private void EventListener_NoticeClickPrivacyPolicy(object sender, NoticeClickPrivacyPolicyEvent e)
    {
        message += "\nEvent: NoticeClickPrivacyPolicy";
    }

    private void EventListener_NoticeClickAgree(object sender, NoticeClickAgreeEvent e)
    {
        message += "\nEvent: NoticeClickAgree";
    }

    private void EventListener_ShowNotice(object sender, ShowNoticeEvent e)
    {
        message += "\nEvent: ShowNotice";
    }

    private void EventListener_HideNotice(object sender, HideNoticeEvent e)
    {
        message += "\nEvent: HideNotice";
    }

    private void EventListener_ShowPreferences(object sender, ShowPreferencesEvent e)
    {
        message += "\nEvent: ShowPreferences";
    }

    private void EventListener_HidePreferences(object sender, HidePreferencesEvent e)
    {
        message += "\nEvent: HidePreferences";
    }

    private void EventListener_PreferencesClickViewPurposes(object sender, PreferencesClickViewPurposesEvent e)
    {
        message += "\nEvent: PreferencesClickViewPurposes";
    }

    private void EventListener_PreferencesClickDisagreeToAllPurposes(object sender, PreferencesClickDisagreeToAllPurposesEvent e)
    {
        message += "\nEvent: PreferencesClickDisagreeToAllPurposes";
    }

    private void EventListener_PreferencesClickAgreeToAllPurposes(object sender, PreferencesClickAgreeToAllPurposesEvent e)
    {
        message += "\nEvent: PreferencesClickAgreeToAllPurposes";
    }

    private void EventListener_PreferencesClickResetAllPurposes(object sender, PreferencesClickResetAllPurposesEvent e)
    {
        message += "\nEvent: PreferencesClickResetAllPurposes";
    }

    private void EventListener_PreferencesClickPurposeDisagree(object sender, PreferencesClickPurposeDisagreeEvent e)
    {
        message += "\nEvent: PreferencesClickPurposeDisagree, Purpose=" + e.getPurposeId();
    }

    private void EventListener_PreferencesClickPurposeAgree(object sender, PreferencesClickPurposeAgreeEvent e)
    {
        message += "\nEvent: PreferencesClickPurposeAgree, Purpose=" + e.getPurposeId();
    }

    private void EventListener_PreferencesClickCategoryDisagree(object sender, PreferencesClickCategoryDisagreeEvent e)
    {
        message += "\nEvent: PreferencesClickCategoryDisagree, Category=" + e.getCategoryId();
    }

    private void EventListener_PreferencesClickCategoryAgree(object sender, PreferencesClickCategoryAgreeEvent e)
    {
        message += "\nEvent PreferencesClickCategoryAgreeEvent, Category=" + e.getCategoryId();
    }

    private void EventListener_PreferencesClickViewVendors(object sender, PreferencesClickViewVendorsEvent e)
    {
        message += "\nEvent: PreferencesClickViewVendors";
    }

    private void EventListener_PreferencesClickSaveChoices(object sender, PreferencesClickSaveChoicesEvent e)
    {
        message += "\nEvent: PreferencesClickSaveChoicesEvent";
    }

    private void EventListener_PreferencesClickDisagreeToAll(object sender, PreferencesClickDisagreeToAllEvent e)
    {
        message += "\nEvent: PreferencesClickDisagreeToAll";
    }

    private void EventListener_PreferencesClickAgreeToAll(object sender, PreferencesClickAgreeToAllEvent e)
    {
        message += "\nEvent: PreferencesClickAgreeToAll";
    }

    private void EventListener_PreferencesClickDisagreeToAllVendors(object sender, PreferencesClickDisagreeToAllVendorsEvent e)
    {
        message += "\nEvent: PreferencesClickDisagreeToAllVendors";
    }

    private void EventListener_PreferencesClickAgreeToAllVendors(object sender, PreferencesClickAgreeToAllVendorsEvent e)
    {
        message += "\nEvent: PreferencesClickAgreeToAllVendors";
    }

    private void EventListener_PreferencesClickVendorDisagree(object sender, PreferencesClickVendorDisagreeEvent e)
    {
        message += "\nEvent: PreferencesClickVendorDisagree, Vendor=" + e.getVendorId();
    }

    private void EventListener_PreferencesClickVendorAgree(object sender, PreferencesClickVendorAgreeEvent e)
    {
        message += "\nEvent: PreferencesClickVendorAgreeEvent, Vendor=" + e.getVendorId();
    }

    private void EventListener_PreferencesClickVendorSaveChoices(object sender, PreferencesClickVendorSaveChoicesEvent e)
    {
        message += "\nEvent: PreferencesClickVendorSaveChoices";
    }

    private void EventListener_SyncReady(object sender, SyncReadyEvent e)
    {
        message += "\nEvent: SyncDone, OrganizationUserId=" + e.GetOrganizationUserId() + ", " +
            "Status applied ? " + e.IsStatusApplied() + ", " +
            "Acknowledged ? " + e.SyncAcknowledged();
    }

    private void EventListener_SyncError(object sender, SyncErrorEvent e)
    {
        message += "\nEvent: SyncError, Message=" + e.getErrorMessage();
    }

    private void EventListener_LanguageUpdated(object sender, LanguageUpdatedEvent e)
    {
        message += "\nEvent: LanguageUpdated, LanguageCode=" + e.getLanguageCode();
    }

    private void EventListener_LanguageUpdateFailed(object sender, LanguageUpdateFailedEvent e)
    {
        message += "\nEvent: LanguageUpdateFailed, Reason=" + e.getReason();
    }

    private string GetFirstRequiredPurposeId()
    {
        var requiredPurposes = Didomi.GetInstance().GetRequiredPurposes();

        var purposeId = string.Empty;
        if (requiredPurposes.Count > 0)
        {
            purposeId = requiredPurposes.FirstOrDefault().Id;
        }

        return purposeId;
    }

    private string GetFirstRequiredVendorId()
    {
        var requiredVendors = Didomi.GetInstance().GetRequiredVendors();

        var vendorId = string.Empty;
        if (requiredVendors.Count > 0)
        {
            vendorId = requiredVendors.FirstOrDefault().Id;
        }

        return vendorId;
    }

    private string TextForISet<T>(ISet<T> set)
    {
        if (set == null)
        {
            return "Null Returned";
        }
        else
        {
            return $"set returned with {set.Count}";
        }
    }

    private string ToMessage(IList<string> list)
    {
        var messageBuilder = new StringBuilder();
        var seperator = ", ";

        foreach (var item in list)
        {
            messageBuilder.Append(item);
            messageBuilder.Append(seperator);
        }

        var message = messageBuilder.ToString();

        if (list.Count > 0)
        {
            message = message.Substring(0, message.Length - 2);
        }

        return message;
    }

    private string ToMessage(Purpose purpose)
    {
        var id = purpose.Id ?? string.Empty;
        var name = purpose.Name ?? string.Empty;
        var descriptionText = purpose.DescriptionText ?? string.Empty;

        var nl = Environment.NewLine;

        return $"Purpose {nl} id: {id + nl} name: {name + nl} descriptionText: {descriptionText + nl}";
    }

    private string ToMessage(Vendor vendor)
    {

        var id = vendor.Id ?? NullText;
        var name = vendor.Name ?? NullText;
        var policyUrl = vendor.PolicyUrl ?? NullText;
        var namespaces = vendor.GetNamespaces()?.ToString() ?? NullText;

        var purposeIds = NullText;
        var purposeIdList = vendor.PurposeIds;
        if (purposeIdList != null)
        {
            purposeIds = ToMessage(purposeIdList);
        }

        var legIntPurposeIds = NullText;
        var legIntPurposeIdList = vendor.LegIntPurposeIds;
        if (legIntPurposeIdList != null)
        {
            legIntPurposeIds = ToMessage(legIntPurposeIdList);
        }

        var featureIds = NullText;
        var featureIdList = vendor.FeatureIds;
        if (featureIdList != null)
        {
            featureIds = ToMessage(featureIdList);
        }

        var flexiblePurposeIds = NullText;
        var flexiblePurposeIdList = vendor.FlexiblePurposeIds;
        if (flexiblePurposeIdList != null)
        {
            flexiblePurposeIds = ToMessage(flexiblePurposeIdList);
        }

        var specialFeatureIds = NullText;
        var specialFeatureIdList = vendor.SpecialFeatureIds;
        if (specialFeatureIdList != null)
        {
            specialFeatureIds = ToMessage(specialFeatureIdList);
        }

        var specialPurposeIds = NullText;
        var specialPurposeIdList = vendor.SpecialFeatureIds;
        if (specialPurposeIdList != null)
        {
            specialPurposeIds = ToMessage(specialPurposeIdList);
        }

        var urls = vendor.Urls?.ToString() ?? NullText;

        var nl = Environment.NewLine;

        return $"Vendor {nl} id: {id + nl} name: {name + nl} policyUrl: {policyUrl + nl} namespaces: {namespaces + nl} purposeIds: {purposeIds + nl} legIntPurposeIds: {legIntPurposeIds + nl} featureIds: {featureIds + nl} flexiblePurposeIds: {flexiblePurposeIds + nl} specialFeatureIds: {specialFeatureIds + nl} specialPurposeIds: {specialPurposeIds + nl} urls: {urls + nl}";
    }

    private string ToMessage(IDictionary<string, string> dict)
    {
        var dictContent = new StringBuilder();

        foreach (var key in dict.Keys)
        {
            dictContent.AppendLine($"key {key} value {dict[key]}");
        }

        return $"Dictionary {Environment.NewLine} {dictContent.ToString()}";
    }

    private string TextForObject(object obj)
    {

        if (obj == null)
        {
            return NullText;
        }
        else
        {
            var purposeSet = obj as ISet<Purpose>;

            if (purposeSet != null)
            {
                return TextForISet(purposeSet);
            }

            var vendorSet = obj as ISet<Vendor>;

            if (vendorSet != null)
            {
                return TextForISet(vendorSet);
            }

            var stringSet = obj as ISet<string>;

            if (stringSet != null)
            {
                return TextForISet(stringSet);
            }

            var purpose = obj as Purpose;

            if (purpose != null)
            {
                return ToMessage(purpose);
            }

            var vendor = obj as Vendor;

            if (vendor != null)
            {
                return ToMessage(vendor);
            }

            return $"Value {obj} returned";
        }
    }

    private string MessageForObject(object obj)
    {
        return $"{Environment.NewLine} {TextForObject(obj)}";
    }
}
