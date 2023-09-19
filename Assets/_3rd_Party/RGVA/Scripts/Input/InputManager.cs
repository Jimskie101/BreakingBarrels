using UnityEngine;
using Sirenix.OdinInspector;

namespace RGVA
{
    public class InputManager : Singleton<InputManager>
    {
        public delegate void InputUpEvent();
        public delegate void InputEvent(Vector2 i_Points);

        [ShowInInspector, ReadOnly] private ScreenData m_ScreenData;
        [SerializeField] private bool LogScreenData = false;

        public static event InputUpEvent OnInputUp;
        public static event InputEvent OnInputDown;
        public static event InputEvent OnDragDelta;
        public static event InputEvent OnDrag;

        public Vector2 DragSensitivity = Vector2.one;
        public bool IsInputDown { get { return m_IsInputDown; } }
        public Vector2 DeltaDrag { get { return m_DeltaDrag * DragSensitivity / m_ScreenData.FinalDPI * m_ScreenData.Scaling; } }
        public Vector2 Drag { get { return m_Drag * DragSensitivity / m_ScreenData.FinalDPI * m_ScreenData.Scaling; } }

        private bool m_IsInputDown;
        private Vector2 m_DeltaDrag;
        private Vector2 m_Drag;
        private Vector3 m_MousePos => Input.mousePosition * m_ScreenData.Scaling;
        private Vector3 m_InputDownPos;
        private Vector3 m_LastInputPos;

        //protected override void OnAwakeEvent()
        //{
        //    base.OnAwakeEvent();
        //    m_ScreenData = new ScreenData();
        //    m_ScreenData.CalculateData(LogScreenData);
        //}

        private void OnEnable()
        {
            m_ScreenData.CalculateData(LogScreenData);
        }

        private void ResetValues()
        {
            m_DeltaDrag = m_Drag = Vector2.zero;
        }


        private void FixedUpdate()
        {
#if UNITY_EDITOR
            m_ScreenData.CalculateData();
#endif

            if (m_IsInputDown)
            {
                m_Drag = m_MousePos - m_InputDownPos;
                m_DeltaDrag = m_MousePos - m_LastInputPos;

                if (m_Drag != Vector2.zero)
                    OnDrag?.Invoke(m_Drag);

                if (m_DeltaDrag != Vector2.zero)
                    OnDragDelta?.Invoke(m_DeltaDrag);

                m_LastInputPos = m_MousePos;
            }
        }

        public void InputDown()
        {
            m_IsInputDown = true;
            m_InputDownPos = m_LastInputPos = m_MousePos;
            ResetValues();

            OnInputDown?.Invoke(Input.mousePosition);
        }

        public void InputUp()
        {
            m_IsInputDown = false;
            ResetValues();

            OnInputUp?.Invoke();

        }
    }
}

