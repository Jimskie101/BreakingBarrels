using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RGVA
{
    public class ButtonRGVA : Button
    {
        public delegate void InputEvent(PointerEventData i_PointerEventData);

        public event InputEvent OnInputUp;
        public event InputEvent OnInputDown;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnInputDown?.Invoke(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnInputUp?.Invoke(eventData);
        }
    }
}

