using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        private readonly Rect textureRect = new Rect(0, 0, 1440, 2560);
        private readonly Rect noticeLearnMoreRect = new Rect(545, 1665, 369, 93);
        private readonly Rect noticeDeclineRect = new Rect(157, 1833, 413, 133);
        private readonly Rect noticeAgreeAndCloseRect = new Rect(625, 1821, 657, 145);
        private readonly Rect noticeCrossCloseRect = new Rect(1241, 41, 161, 161);
        //partners
        private readonly Rect partnersSaveRect = new Rect(1043, 2373, 329, 143);
        private readonly Rect partnersCrossClosePartnersRect = new Rect(1241, 41, 161, 161);
        //purposes
        private readonly Rect purposesViewOurPartnersRect = new Rect(73, 1761, 1293, 177);
        private readonly Rect purposesDisagreeToAllRect = new Rect(501, 2381, 457, 105);
        private readonly Rect purposesAgreeToAllRect = new Rect(1013, 2381, 373, 105);
        private readonly Rect purposesCrossCloseRect = new Rect(1241, 41, 161, 161);

        private Rect _textureNewAspectedRect;
        private float _xMousePositionMapFactor = 0;
        private float _yMousePositionMapFactor = 0;

        Texture2D _noticeTexture;
        Texture2D _partnersTexture;
        Texture2D _purposesTexture;

        public void Start()
        {
            _noticeTexture = Resources.Load<Texture2D>("mock-ui/Notice");
            _partnersTexture = Resources.Load<Texture2D>("mock-ui/Partners");
            _purposesTexture = Resources.Load<Texture2D>("mock-ui/Purposes");
            _textureNewAspectedRect = GetFullScreenKeepAspectRatioRect();
        }

        private Rect GetFullScreenRect()
        {
            return new Rect(0, 0, Screen.width, Screen.height);
        }

        private Vector3 MapMousePositionOnOriginalTexture()
        {
            Vector3 retval = new Vector3();
            retval.x = (-_textureNewAspectedRect.x + Input.mousePosition.x) * _xMousePositionMapFactor;
            retval.y = (-_textureNewAspectedRect.y + Screen.height-Input.mousePosition.y) * _yMousePositionMapFactor;
            return retval;
        }

        private Rect GetFullScreenKeepAspectRatioRect()
        {
            var ratio= textureRect.width / textureRect.height;
            var ratioScreen = (Screen.width) / (float)Screen.height;

            float newWidth = 0;
            float newHeight = 0;
            float newX = 0;
            float newY = 0;

            if (ratio > ratioScreen)
            {
                newWidth = Screen.width;
                newHeight = newWidth / ratio;
                newX = 0;
                newY = (Screen.height - newHeight) / 2;

                _xMousePositionMapFactor = textureRect.width / newWidth;
                _yMousePositionMapFactor = textureRect.height / newHeight;
            }
            else
            {
                newWidth = ratio * Screen.height;
                newHeight = Screen.height;
                newX = (Screen.width - newWidth) / 2;
                newY = 0;

                _xMousePositionMapFactor = textureRect.width/ newWidth;
                _yMousePositionMapFactor = textureRect.height / newHeight;
            }

            return new Rect(newX, newY, newWidth, newHeight);
        }

        private void OnGUI()
        {
            if (_noticeUIVisible || _showPreferencesUIVisible || _partnersUIVisible)
            {
                GUI.depth = -1;
                //Empty Button added to block mouse clicks to other widgets other than mock ui.
                GUI.Button(GetFullScreenRect(), "");
                //fill white background before showing mock ui.
                GUI.DrawTexture(GetFullScreenRect(), Texture2D.whiteTexture, ScaleMode.StretchToFill, true);
            }

            GUI.depth = 0;

            if (_partnersUIVisible)
            {
                GUI.DrawTexture(_textureNewAspectedRect, _partnersTexture);
            }
            else if (_showPreferencesUIVisible)
            {
                GUI.DrawTexture(_textureNewAspectedRect, _purposesTexture);
            }
            else if (_noticeUIVisible)
            {
                GUI.DrawTexture(_textureNewAspectedRect, _noticeTexture);
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
                    }

                    if (PurposesDisAgreeToAllClicked() && !_partnersUIVisible)
                    {
                        _showPreferencesUIVisible = false;

                        CloseUI();
                    }

                    if (PurposesAgreeToAllClicked() && !_partnersUIVisible)
                    {
                        _showPreferencesUIVisible = false;

                        CloseUI();
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

        private bool RectClicked(Rect target, Rect main)
        {
            Vector3 mousePosition = MapMousePositionOnOriginalTexture();
            //to target mouse clicks right buttons we put a delay of 0.5 seconds for subsequent clicks
            if (DateTime.Now.Subtract(_lastCall).TotalSeconds < 0.5)
            {
                return false;
            }
            var retval = target.Contains(mousePosition);

            if (retval)
            {
                _lastCall = DateTime.Now;
            }

            return retval;
        }

        private bool NoticeCrossCloseClicked()
        {
            return RectClicked(noticeCrossCloseRect, textureRect);
        }

        private bool NoticeLearnMoreClicked()
        {
            return RectClicked(noticeLearnMoreRect, textureRect);
        }

        private bool NoticeDeclineClicked()
        {
            return RectClicked(noticeDeclineRect, textureRect);
        }

        private bool NoticeAgreeAndCloseClicked()
        {
            return RectClicked(noticeAgreeAndCloseRect, textureRect);
        }

        private bool PurposesViewOurPartnersClicked()
        {
            return RectClicked(purposesViewOurPartnersRect, textureRect);
        }

        private bool PurposesCrossCloseClicked()
        {
            return RectClicked(purposesCrossCloseRect, textureRect);
        }

        private bool PurposesAgreeToAllClicked()
        {
            return RectClicked(purposesAgreeToAllRect, textureRect);
        }

        private bool PurposesDisAgreeToAllClicked()
        {
            return RectClicked(purposesDisagreeToAllRect, textureRect);
        }

        private bool PartnersCrossCloseClicked()
        {
            return RectClicked(partnersCrossClosePartnersRect, textureRect);
        }

        private bool PartnersSaveClicked()
        {
            return RectClicked(partnersSaveRect, textureRect);
        }
    }
}
