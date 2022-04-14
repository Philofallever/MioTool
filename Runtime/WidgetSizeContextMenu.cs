#if UNITY_EDITOR


using System;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

namespace MioTool
{
    sealed class WidgetSizeContextMenu : MonoBehaviour
    {
        #region Button

        enum ButtonSize
        {
            None,
            Size_128x64,
            Size_256x128,
        }

        [SerializeField, LabelText("按钮尺寸")]
        [ShowIf(nameof(IsButton))]
        [OnValueChanged(nameof(ResizeButton))]
        private ButtonSize _buttonSize;

        [SerializeField, LabelText("按钮文本字号")]
        [ShowIf(nameof(IsLabelButton))]
        [OnValueChanged(nameof(ResizeButtonLabel))]
        private FontSize _btnLabelFontSize;

        bool IsButton()
        {
            return GetComponent<Button>();
        }

        bool IsLabelButton()
        {
            return GetComponent<Button>() && GetComponentInChildren<Text>();
        }

        void ResizeButton()
        {
            if (_buttonSize == ButtonSize.None) return;
            var btn = GetComponent<Button>();
            var eName = _buttonSize.ToString();
            var (first, second) = MatchInt2(eName);
            var rect = btn.transform as RectTransform;
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, first);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, second);
        }

        void ResizeButtonLabel()
        {
            if (_btnLabelFontSize == FontSize.None) return;

            var label = GetComponentInChildren<Text>();
            var eName = _btnLabelFontSize.ToString();
            label.fontSize = MatchInt(eName);
        }

        #endregion

        #region Text

        enum FontSize
        {
            None,
            Size_20,
            Size_22,
            Size_24,
            Size_26,
            Size_28,
            Size_30,
            Size_32,
            Size_34,
            Size_36,
        }

        //[SerializeField, LabelText("文本字号")]
        //[ShowIf()]
        //private

        #endregion


        static int MatchInt(string input)
        {
            var m = Regex.Match(input, @"(\d+)");
            Debug.Assert(m.Groups.Count == 1);
            return int.Parse(m.Groups[1].Value);
        }

        static (int first, int second) MatchInt2(string input)
        {
            var m = Regex.Match(input, @"(\d+)\D*(\d+)");
            Debug.Assert(m.Groups.Count == 2);
            return (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value));
        }

        static (int first, int second, int third) MatchInt3(string input)
        {
            var m = Regex.Match(input, @"(\d+)\D*(\d+)\D*(\d+)");
            Debug.Assert(m.Groups.Count == 3);
            return (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value));
        }
    }
}

#endif