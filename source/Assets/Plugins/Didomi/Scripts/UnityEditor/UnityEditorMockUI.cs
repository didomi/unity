using System;
using UnityEngine;

namespace IO.Didomi.SDK.UnityEditor
{
    /// <summary>
    /// GameObject script for showing Mock UI windows for SetupUI ShowNotice and ShowPreferences
    /// </summary>
    public class UnityEditorMockUI : MonoBehaviour
    {
        bool _noticeUIVisible = false;
        bool _showPreferencesUIVisible = false;
        bool _partnersUIVisible = false;
        DateTime _lastCall = DateTime.Now;

        //notice
        private readonly Rect noticeRect = new Rect(0, 0, 1440, 2560);
        private readonly Rect noticeLearnMoreRect = new Rect(545, 1665, 369, 93);
        private readonly Rect noticeDeclineRect = new Rect(157, 1833, 413, 133);
        private readonly Rect noticeAgreeAndCloseRect = new Rect(625, 1821, 657, 145);
        private readonly Rect noticeCrossCloseRect = new Rect(1241, 41, 161, 161);
        //partners
        private readonly Rect partnersRect = new Rect(0, 0, 1440, 2560);
        private readonly Rect partnersSaveRect = new Rect(1043, 2373, 329, 143);
        private readonly Rect partnersCrossClosePartnersRect = new Rect(1241, 41, 161, 161);
        //purposes
        private readonly Rect purposesRect = new Rect(0, 0, 1440, 2560);
        private readonly Rect purposesViewOurPartnersRect = new Rect(73, 1761, 1293, 177);
        private readonly Rect purposesDisagreeToAllRect = new Rect(501, 2381, 457, 105);
        private readonly Rect purposesAgreeToAllRect = new Rect(1013, 2381, 373, 105);
        private readonly Rect purposesCrossCloseRect = new Rect(1241, 41, 161, 161);

        Texture2D _noticeTexture;
        Texture2D _partnersTexture;
        Texture2D _purposesTexture;

        public void Start()
        {
            _noticeTexture = Resources.Load<Texture2D>("mock-ui/Notice");
            _partnersTexture = Resources.Load<Texture2D>("mock-ui/Partners");
            _purposesTexture = Resources.Load<Texture2D>("mock-ui/Purposes");
        }

        private Rect GetFullScreenRect()
        {
            return new Rect(0, 0, Screen.width, Screen.height);
        }

        private void OnGUI()
        {
            GUI.depth = 0;

            if (_partnersUIVisible)
            {
                GUI.DrawTexture(GetFullScreenRect(), _partnersTexture, ScaleMode.StretchToFill, true);
            }
            else if (_showPreferencesUIVisible)
            {
                GUI.DrawTexture(GetFullScreenRect(), _purposesTexture, ScaleMode.StretchToFill, true);
            }
            else if (_noticeUIVisible)
            {
                GUI.DrawTexture(GetFullScreenRect(), _noticeTexture, ScaleMode.StretchToFill, true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (_partnersUIVisible)
                {

                    if (PartnersCrossCloseClicked())
                    {
                        _partnersUIVisible = false;
                    }

                    if (PartnersSaveClicked())
                    {
                        _partnersUIVisible = false;
                    }
                }
                else if (_showPreferencesUIVisible)
                {

                    if (PurposesCrossCloseClicked() && !_partnersUIVisible)
                    {
                        _showPreferencesUIVisible = false;

                        if (!_noticeUIVisible)
                        {
                            CloseUI();
                        }
                    }

                    if (PurposesDisAgreeToAllClicked() && !_partnersUIVisible)
                    {
                        _showPreferencesUIVisible = false;

                        if (!_noticeUIVisible)
                        {
                            CloseUI();
                        }
                    }

                    if (PurposesAgreeToAllClicked() && !_partnersUIVisible)
                    {
                        _showPreferencesUIVisible = false;

                        if (!_noticeUIVisible)
                        {
                            CloseUI();
                        }
                    }

                    if (PurposesViewOurPartnersClicked() && !_partnersUIVisible)
                    {
                        ShowPartnersUI();
                    }
                }
                else
                {
                    if (_noticeUIVisible)
                    {

                        if (NoticeCrossCloseClicked())
                        {
                            CloseUI();
                        }

                        if (NoticeAgreeAndCloseClicked())
                        {
                            CloseUI();
                        }

                        if (NoticeDeclineClicked())
                        {
                            CloseUI();
                        }

                        if (NoticeLearnMoreClicked())
                        {
                            ShowPurposesUI();
                        }
                    }
                }
            }
        }

        private void CloseUI()
        {
            Destroy(gameObject);
        }

        public void ShowNoticeUI()
        {
            _noticeUIVisible = true;
        }

        private void ShowPartnersUI()
        {
            _partnersUIVisible = true;
        }

        public void ShowPurposesUI()
        {
            _showPreferencesUIVisible = true;
        }

        private bool RectClicked(Rect target, Vector3 mousePosition, Rect main)
        {
            //to target mouse clicks right buttons we put a delay of 0.5 seconds for subsequent clicks
            if (DateTime.Now.Subtract(_lastCall).TotalSeconds<0.5)
            {
                return false;
            }

            Vector2 rectMousePoint = new Vector2((mousePosition.x / Screen.width) * main.width,
             ((Screen.height - mousePosition.y) / Screen.height) * main.height);

            var retval = target.Contains(rectMousePoint);

            if (retval)
            {
                _lastCall = DateTime.Now;
            }

            return retval;
        }

        private bool NoticeCrossCloseClicked()
        {
            return RectClicked(noticeCrossCloseRect, Input.mousePosition, noticeRect);
        }

        private bool NoticeLearnMoreClicked()
        {
            return RectClicked(noticeLearnMoreRect, Input.mousePosition, noticeRect);
        }

        private bool NoticeDeclineClicked()
        {
            return RectClicked(noticeDeclineRect, Input.mousePosition, noticeRect);
        }

        private bool NoticeAgreeAndCloseClicked()
        {
            return RectClicked(noticeAgreeAndCloseRect, Input.mousePosition, noticeRect);
        }

        private bool PurposesViewOurPartnersClicked()
        {
            return RectClicked(purposesViewOurPartnersRect, Input.mousePosition, purposesRect);
        }

        private bool PurposesCrossCloseClicked()
        {
            return RectClicked(purposesCrossCloseRect, Input.mousePosition, purposesRect);
        }

        private bool PurposesAgreeToAllClicked()
        {
            return RectClicked(purposesAgreeToAllRect, Input.mousePosition, purposesRect);
        }

        private bool PurposesDisAgreeToAllClicked()
        {
            return RectClicked(purposesDisagreeToAllRect, Input.mousePosition, purposesRect);
        }

        private bool PartnersCrossCloseClicked()
        {
            return RectClicked(partnersCrossClosePartnersRect, Input.mousePosition, partnersRect);
        }

        private bool PartnersSaveClicked()
        {
            return RectClicked(partnersSaveRect, Input.mousePosition, partnersRect);
        }
    }
}
