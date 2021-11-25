using IO.Didomi.SDK;
using IO.Didomi.SDK.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    Purpose_1,
    Purpose_2,
    Vendor_1,
    Vendor_2,
    Consent_1,
    Consent_2,
    Consent_3,
    Legitimate,
    Notice,
    Preferences,
    Language,
    Initialization,
    Events,
    GetUserStatus
};

public class DemoGUI : MonoBehaviour
{
    private const string NotCallableForObjectiveC= "The function is not callable for IOS platform. Check IOS-SDK doc. Since Unity creates Objective-C, It is not callable.";
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
        if (functionCategory == FunctionCategory.Purpose_1)
        {
            Purpose_1();
        }
        else if (functionCategory == FunctionCategory.Purpose_2)
        {
            Purpose_2();
        }
        else if (functionCategory == FunctionCategory.Vendor_1)
        {
            Vendor_1();
        }
        else if (functionCategory == FunctionCategory.Vendor_2)
        {
            Vendor_2();
        }
        else if (functionCategory == FunctionCategory.Consent_1)
        {
            Consent_1();
        }
        else if (functionCategory == FunctionCategory.Consent_2)
        {
            Consent_2();
        }
        else if (functionCategory == FunctionCategory.Consent_3)
        {
            Consent_3();
        }
        else if (functionCategory == FunctionCategory.Legitimate)
        {
            Legitimate();
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
        else if (functionCategory == FunctionCategory.GetUserStatus)
        {
            GetUserStatus();
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

        if (GUI.Button(GetRightRect1(), "Purpose-1"))
        {
            functionCategory = FunctionCategory.Purpose_1;
        }

        if (GUI.Button(GetLeftRect2(), "Purpose-2"))
        {
            functionCategory= FunctionCategory.Purpose_2;
        }

        if (GUI.Button(GetMiddleRect2(), "Vendor-1"))
        {
            functionCategory = FunctionCategory.Vendor_1;
        }

        if (GUI.Button(GetRightRect2(), "Vendor-2"))
        {
            functionCategory = FunctionCategory.Vendor_2;
        }

        if (GUI.Button(GetLeftRect3(), "Consent-1"))
        {
            functionCategory = FunctionCategory.Consent_1;
        }

        if (GUI.Button(GetMiddleRect3(), "Consent-2"))
        {
            functionCategory = FunctionCategory.Consent_2;
        }

        if (GUI.Button(GetRightRect3(), "Consent-3"))
        {
            functionCategory = FunctionCategory.Consent_3;
        }

        if (GUI.Button(GetLeftRect4(), "Language"))
        {
            functionCategory = FunctionCategory.Language;
        }

        if (GUI.Button(GetMiddleRect4(), "Initialization"))
        {
            functionCategory = FunctionCategory.Initialization;
        }

        if (GUI.Button(GetRightRect4(), "Events"))
        {
            functionCategory = FunctionCategory.Events;
        }

        if (GUI.Button(GetLeftRect5(), "Legitimate"))
        {
            functionCategory = FunctionCategory.Legitimate;
        }

        if (GUI.Button(GetMiddleRect5(), "GetUserStatus"))
        {
            functionCategory = FunctionCategory.GetUserStatus;
        }

        if (GUI.Button(GetRightRect5(), ""))
        {
            functionCategory = FunctionCategory.Events;
        }
    }

    private void Purpose_1()
    {
        if (GUI.Button(GetFuncRect1(), "GetEnabledPurposes"))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                message = string.Empty;
                var retval = Didomi.GetInstance().GetEnabledPurposes();
                message += "GetEnabledPurposes" + MessageForObject(retval);
            }
            else
            {
                message = NotCallableForObjectiveC;
            }
        }

        if (GUI.Button(GetFuncRect2(), "GetEnabledPurposeIds"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().GetEnabledPurposeIds();
            message += "GetEnabledPurposeIds" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect3(), "GetDisabledPurposes"))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                message = string.Empty;
                var retval = Didomi.GetInstance().GetDisabledPurposes();
                message += "GetDisabledPurposes" + MessageForObject(retval);
            }
            else
            {
                message = NotCallableForObjectiveC;
            }
        }

        if (GUI.Button(GetFuncRect4(), "GetDisabledPurposeIds"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().GetDisabledPurposeIds();
            message += "GetDisabledPurposeIds" + MessageForObject(retval);
        }
    }

    private void Purpose_2()
    {
        if (GUI.Button(GetFuncRect1(), "GetRequiredPurposes"))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                message = string.Empty;
                var retval = Didomi.GetInstance().GetRequiredPurposes();
                message = "GetRequiredPurposes" + MessageForObject(retval);
            }
            else
            {
                message = NotCallableForObjectiveC;
            }
        }

        if (GUI.Button(GetFuncRect2(), "GetRequiredPurposeIds"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().GetRequiredPurposeIds();
            message += "GetRequiredPurposeIds" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect3(), "GetPurpose"))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                message = string.Empty;
                var purposeId = GetFirstRequiredPurposeId();
                var retval = Didomi.GetInstance().GetPurpose(purposeId);
                message += "GetPurpose" + MessageForObject(retval);
            }
            else
            {
                message = NotCallableForObjectiveC;
            }
        }
    }

    private void Vendor_1()
    {
        if (GUI.Button(GetFuncRect1(), "GetEnabledVendors"))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                message = string.Empty;
                var retval = Didomi.GetInstance().GetEnabledVendors();
                message += "GetEnabledVendors" + MessageForObject(retval);
            }
            else
            {
                message = NotCallableForObjectiveC;
            }
        }

        if (GUI.Button(GetFuncRect2(), "GetEnabledVendorIds"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().GetEnabledVendorIds();
            message += "GetEnabledVendorIds" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect3(), "GetDisabledVendors"))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                message = string.Empty;
                var retval = Didomi.GetInstance().GetDisabledVendors();
                message += "GetDisabledVendors" + MessageForObject(retval);
            }
            else
            {
                message = NotCallableForObjectiveC;
            }
        }

        if (GUI.Button(GetFuncRect4(), "GetDisabledVendorIds"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().GetDisabledVendorIds();
            message += "GetDisabledVendorIds" + MessageForObject(retval);
        }
    }

    private void Vendor_2()
    {
        if (GUI.Button(GetFuncRect1(), "GetRequiredVendors"))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                message = string.Empty;
                var retval = Didomi.GetInstance().GetRequiredVendors();
                message = "GetRequiredVendors" + MessageForObject(retval);
            }
            else
            {
                message = NotCallableForObjectiveC;
            }
        }

        if (GUI.Button(GetFuncRect2(), "GetRequiredVendorIds"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().GetRequiredVendorIds();
            message += "GetRequiredVendorIds" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect3(), "GetVendor"))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                message = string.Empty;
                var vendorId = GetFirstRequiredVendorId();
                var retval = Didomi.GetInstance().GetVendor(vendorId);
                message += "GetVendor" + MessageForObject(retval);
            }
            else
            {
                message = NotCallableForObjectiveC;
            }
        }
    }

    private void Consent_1()
    {
        if (GUI.Button(GetFuncRect1(), "GetUserConsentStatusForPurpose"))
        {
            message = string.Empty;
            var purposeId = GetFirstRequiredPurposeId();
            var retval = Didomi.GetInstance().GetUserConsentStatusForPurpose(purposeId);
            message += "GetUserConsentStatusForPurpose" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect2(), "GetUserConsentStatusForVendor"))
        {
            message = string.Empty;
            var vendorId = GetFirstRequiredVendorId();
            var retval = Didomi.GetInstance().GetUserConsentStatusForVendor(vendorId);
            message += "GetUserConsentStatusForVendor" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect3(), $"GetUserConsentStatusForVendor{Environment.NewLine}AndRequiredPurposes"))
        {
            message = string.Empty;
            var vendorId = GetFirstRequiredVendorId();
            var retval = Didomi.GetInstance().GetUserConsentStatusForVendorAndRequiredPurposes(vendorId);
            message += "GetUserConsentStatusForVendorAndRequiredPurposes" + MessageForObject(retval);
        }
    }

    private void Legitimate()
    {
        if (GUI.Button(GetFuncRect1(), $"GetUserLegitimate{Environment.NewLine}InterestStatusForPurpose"))
        {
            message = string.Empty;
            var purposeId = GetFirstRequiredPurposeId();
            var retval = Didomi.GetInstance().GetUserLegitimateInterestStatusForPurpose(purposeId);
            message += "GetUserLegitimateInterestStatusForPurpose" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect2(), $"GetUserLegitimate{Environment.NewLine}InterestStatusForVendor"))
        {
            message = string.Empty;
            var vendorId = GetFirstRequiredVendorId();
            var retval = Didomi.GetInstance().GetUserLegitimateInterestStatusForVendor(vendorId);
            message += "GetUserLegitimateInterestStatusForVendor" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect3(), $"GetUserLegitimateInterestStatus{Environment.NewLine}ForVendorAndRequiredPurposes"))
        {
            message = string.Empty;
            var vendorId = GetFirstRequiredVendorId();
            var retval = Didomi.GetInstance().GetUserLegitimateInterestStatusForVendorAndRequiredPurposes(vendorId);
            message += "GetUserLegitimateInterestStatusForVendorAndRequiredPurposes" + MessageForObject(retval);
        }
    }

    private void Consent_2()
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

        if (GUI.Button(GetFuncRect3(), "SetUserStatus 1"))
        {
            message = string.Empty;

            ISet<string> enabledConsentPurposeIds = Didomi.GetInstance().GetRequiredPurposeIds();
            ISet<string> disabledConsentPurposeIds = new HashSet<string>();
            ISet<string> enabledLIPurposeIds = new HashSet<string>();
            ISet<string> disabledLIPurposeIds = new HashSet<string>();
            ISet<string> enabledConsentVendorIds = new HashSet<string>();
            ISet<string> disabledConsentVendorIds = new HashSet<string>();
            ISet<string> enabledLIVendorIds = new HashSet<string>();
            ISet<string> disabledLIVendorIds = new HashSet<string>();

            var retval = Didomi.GetInstance().SetUserStatus(
                enabledConsentPurposeIds,
                disabledConsentPurposeIds,
                enabledLIPurposeIds,
                disabledLIPurposeIds,
                enabledConsentVendorIds,
                disabledConsentVendorIds,
                enabledLIVendorIds,
                disabledLIVendorIds);

            message += "SetUserStatus 1" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect4(), "SetUserStatus 2"))
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

            message += "SetUserStatus 2" + MessageForObject(retval);
        }
    }

    private void Consent_3()
    {
        if (GUI.Button(GetFuncRect1(), "IsConsentRequired"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().IsConsentRequired();
            message += "IsConsentRequired" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect2(), "IsUserConsentStatusPartial"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().IsUserConsentStatusPartial();
            message += "IsUserConsentStatusPartial" + MessageForObject(retval);
        }

        if (GUI.Button(GetFuncRect3(), "ShouldConsentBeCollected"))
        {
            message = string.Empty;
            var retval = Didomi.GetInstance().ShouldConsentBeCollected();
            message += "ShouldConsentBeCollected" + MessageForObject(retval);
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
            Didomi.GetInstance().Reset();
            Didomi.GetInstance().ShowPreferences();
            message += "showPreferences";
        }

        if (GUI.Button(GetFuncRect2(), "HidePreferences"))
        {
            message = string.Empty;
            Didomi.GetInstance().HidePreferences();
            message += "HidePreferences";
        }

        if (GUI.Button(GetFuncRect3(), "IsPreferencesVisible"))
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

        if (GUI.Button(GetFuncRect2(), "Initialize1"))
        {
            Initialize1Didomi();
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

        Didomi.GetInstance().Initialize(
            apiKey: "c3cd5b46-bf36-4700-bbdc-4ee9176045aa",
            localConfigurationPath: null,
            remoteConfigurationURL: null,
            providerId: null,
            disableDidomiRemoteConfig: true,
            languageCode: null);
    }

    private void Initialize1Didomi()
    {
        message = string.Empty;

        Didomi.GetInstance().OnReady(
              () => { message = "Ready"; }
              );

        Didomi.GetInstance().Initialize(
            apiKey: "c3cd5b46-bf36-4700-bbdc-4ee9176045aa",
            localConfigurationPath: null,
            remoteConfigurationURL: null,
            providerId: null,
            disableDidomiRemoteConfig: true,
            languageCode: null,
            noticeId: null);
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

    void GetUserStatus()
    {
        if (GUI.Button(GetFuncRect1(), "Purposes"))
        {
            message = string.Empty;
            var userStatus = Didomi.GetInstance().GetUserStatus();
            message += "Purposes: global -" +
                userStatus.GetPurposes().GetGlobal().GetEnabled().Count + " enabled, " +
                userStatus.GetPurposes().GetGlobal().GetDisabled().Count + " disabled ; " +
                userStatus.GetPurposes().GetEssential().Count + " essential";
        }

        if (GUI.Button(GetFuncRect2(), "Vendors"))
        {
            message = string.Empty;
            var userStatus = Didomi.GetInstance().GetUserStatus();
            message += "Vendors: global -" +
                userStatus.GetVendors().GetGlobal().GetEnabled().Count + " enabled, " +
                userStatus.GetVendors().GetGlobal().GetDisabled().Count + " disabled ; " +
                " consent - " +
                userStatus.GetVendors().GetConsent().GetEnabled().Count + "enabled, " +
                userStatus.GetVendors().GetConsent().GetDisabled().Count + "disabled";
        }

        if (GUI.Button(GetFuncRect3(), "UserId"))
        {
            message = string.Empty;
            var userStatus = Didomi.GetInstance().GetUserStatus();
            message += "User id = " + userStatus.GetUserId();
        }

        if (GUI.Button(GetFuncRect4(), "ConsentString"))
        {
            message = string.Empty;
            var userStatus = Didomi.GetInstance().GetUserStatus();
            message += "ConsentString = " + userStatus.GetConsentString();
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
        eventListener.ConsentChanged += EventListener_ConsentChanged;
        eventListener.HideNotice += EventListener_HideNotice;
        eventListener.Ready += EventListener_Ready;
        eventListener.Error += EventListener_Error;
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

    private void EventListener_Ready(object sender, ReadyEvent e)
    {
        message += "EventListener_ReadyEvent Fired.";
    }

    private void EventListener_Error(object sender, ErrorEvent e)
    {
        message += "EventListener_Error Fired. Error:" + e.getErrorMessage();
    }

    private void EventListener_ShowNotice(object sender, ShowNoticeEvent e)
    {
        message += "EventListener_ShowNoticeEvent Fired.";
    }

    private void EventListener_PreferencesClickViewVendors(object sender, PreferencesClickViewVendorsEvent e)
    {
        message += "EventListener_PreferencesClickViewVendorsEvent Fired.";
    }

    private void EventListener_PreferencesClickVendorSaveChoices(object sender, PreferencesClickVendorSaveChoicesEvent e)
    {
        message += "EventListener_PreferencesClickVendorSaveChoicesEvent Fired.";
    }

    private void EventListener_PreferencesClickVendorDisagree(object sender, PreferencesClickVendorDisagreeEvent e)
    {
        message += "EventListener_PreferencesClickVendorDisagreeEvent Fired.";
    }

    private void EventListener_PreferencesClickVendorAgree(object sender, PreferencesClickVendorAgreeEvent e)
    {
        message += "EventListener_PreferencesClickVendorAgreeEvent Fired.";
    }

    private void EventListener_PreferencesClickSaveChoices(object sender, PreferencesClickSaveChoicesEvent e)
    {
        message += "EventListener_PreferencesClickSaveChoicesEvent Fired.";
    }

    private void EventListener_PreferencesClickPurposeDisagree(object sender, PreferencesClickPurposeDisagreeEvent e)
    {
        message += "EventListener_PreferencesClickPurposeDisagreeEvent Fired.";
    }

    private void EventListener_PreferencesClickPurposeAgree(object sender, PreferencesClickPurposeAgreeEvent e)
    {
        message += "EventListener_PreferencesClickPurposeAgreeEvent Fired.";
    }

    private void EventListener_PreferencesClickDisagreeToAll(object sender, PreferencesClickDisagreeToAllEvent e)
    {
        message += "EventListener_PreferencesClickDisagreeToAllEvent Fired.";
    }

    private void EventListener_PreferencesClickAgreeToAll(object sender, PreferencesClickAgreeToAllEvent e)
    {
        message += "EventListener_PreferencesClickAgreeToAllEvent Fired.";
    }

    private void EventListener_NoticeClickMoreInfo(object sender, NoticeClickMoreInfoEvent e)
    {
        message += "EventListener_NoticeClickMoreInfoEvent Fired.";
    }

    private void EventListener_NoticeClickAgree(object sender, NoticeClickAgreeEvent e)
    {
        message += "EventListener_NoticeClickAgreeEvent Fired.";
    }

    private void EventListener_HideNotice(object sender, HideNoticeEvent e)
    {
        message += "EventListener_HideNoticeEvent Fired.";
    }

    private void EventListener_ConsentChanged(object sender, ConsentChangedEvent e)
    {
        message += "EventListener_ConsentChangedEvent Fired.";
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
        var id = purpose.GetId() ?? string.Empty;
        var iabId = purpose.GetIabId() ?? string.Empty;
        var name = purpose.GetName() ?? string.Empty;
        var description = purpose.GetDescription() ?? string.Empty;
        var isCustom = purpose.IsCustom();

        var nl = Environment.NewLine;

        return $"Purpose {nl} id: {id + nl} iabId: {iabId + nl} name: {name + nl} description: {description + nl} isCustom: {isCustom + nl}";
    }

    private string ToMessage(Vendor vendor)
    {

        var id = vendor.GetId() ?? NullText;
        var name = vendor.GetName() ?? NullText;
        var privacyPolicyUrl = vendor.GetPrivacyPolicyUrl() ?? NullText;
        var @namespace = vendor.GetNamespace() ?? NullText;

        var purposeIds = NullText;
        var purposeIdList = vendor.GetPurposeIds();
        if (purposeIdList != null)
        {
            purposeIds = ToMessage(purposeIdList);
        }

        var legIntPurposeIds = NullText;
        var legIntPurposeIdList = vendor.GetLegIntPurposeIds();
        if (legIntPurposeIdList != null)
        {
            legIntPurposeIds = ToMessage(legIntPurposeIdList);
        }


        var iabId = vendor.GetIabId() ?? NullText;

        var nl = Environment.NewLine;

        return $"Purpose {nl} id: {id + nl} name: {name + nl} privacyPolicyUrl: {privacyPolicyUrl + nl} @namespace: {@namespace + nl} purposeIds: {purposeIds + nl} legIntPurposeIds: {legIntPurposeIds + nl} iabId: {iabId + nl}";
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
