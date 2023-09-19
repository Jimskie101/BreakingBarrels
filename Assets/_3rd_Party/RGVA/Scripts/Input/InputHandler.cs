using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace RGVA
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField, ReadOnly] private ButtonRGVA m_Button;

#if UNITY_EDITOR
        [Button]
        private void SetRef()
        {
            m_Button = GetComponent<ButtonRGVA>();
        }
#endif

        private void OnEnable()
        {
            m_Button.OnInputDown += OnInputDown;
            m_Button.OnInputUp += OnInputUp;
        }

        private void OnDisable()
        {
            m_Button.OnInputDown -= OnInputDown;
            m_Button.OnInputUp -= OnInputUp;
        }

        private void OnInputDown(PointerEventData i_Pointer)
        {
            InputManager.Instance.InputDown();
        }

        private void OnInputUp(PointerEventData i_Pointer)
        {
            InputManager.Instance.InputUp();
        }

    }
}