#if MIO_EXTERNAL
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MioTool
{
    public class LongClickButton : Button
    {
        [SerializeField]
        private ButtonLongClickEvent _mOnLongClick = new ButtonLongClickEvent();


        [SerializeField]
        private bool m_everyFrame; // 长按后每帧都触发事件

        private float m_thresholdTime = 0.3f; // 长按时间阀值

        private float _downTime;

        public ButtonLongClickEvent OnLongClick
        {
            get => _mOnLongClick;
            set => _mOnLongClick = value;
        }

        public bool everyFrame
        {
            get => m_everyFrame;
            set => m_everyFrame = value;
        }

        void Press()
        {
            if (!IsActive() || !IsInteractable())
                return;
            UISystemProfilerApi.AddMarker("LongClickButton.onClick", this);
            onClick.Invoke();
        }

        void LongPress(bool isOver)
        {
            if (!IsActive() || !IsInteractable())
                return;
            UISystemProfilerApi.AddMarker("LongClickButton.onLongClick", this);
            OnLongClick.Invoke(isOver);
        }

        void Update()
        {
            if (!m_everyFrame) return;
            if (IsPressed() && Time.unscaledTime - _downTime > m_thresholdTime)
                _mOnLongClick.Invoke(false);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _downTime = Time.unscaledTime;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            var isPressed = IsPressed();
            base.OnPointerUp(eventData);

            if (!isPressed) return;
            if (Time.unscaledTime - _downTime > m_thresholdTime)
                LongPress(true);
            else
                Press();
            _downTime = 0;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
        }

        [Serializable]
        public class ButtonLongClickEvent : UnityEvent<bool>
        {
        }
    }
}
#endif