using UnityEngine;
using Sirenix.OdinInspector;

namespace RGVA.SreenShot
{
    public class ScreenshotController : Singleton<ScreenshotController>
    {
        [Header("Ui Behaviour Settings")]
        public float ShutterSpeed = 0.5f;
        public bool AnimateXAxis = false;

        [Header("PC/Web Settings")]
        public KeyCode KeycodeToScreenshot = KeyCode.P;
        public bool EnableCtrl = true;

        [Header("Reference")]
        [SerializeField, ReadOnly] private CameraScreenshotUiHandler m_CameraScreenshotUiHandler;

        private bool m_IsUiShown = false;
        private bool m_CloseUiAtShot = false;

        private ScreenshotManager m_ScreenShotManager => ScreenshotManager.Instance;

#if UNITY_EDITOR
        [Button]
        private void SetRef()
        {
            m_CameraScreenshotUiHandler = GetComponentInChildren<CameraScreenshotUiHandler>();
        }
#endif
        private void OnEnable()
        {
            m_ScreenShotManager.OnStart += OnStart;
            m_ScreenShotManager.OnFinished += OnFinished;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            m_ScreenShotManager.OnStart -= OnStart;
            m_ScreenShotManager.OnFinished -= OnFinished;
        }
        private void OnFinished(Sprite sprite)
        {
            m_CameraScreenshotUiHandler.gameObject.SetActive(true);
        }

        private void OnStart()
        {
            m_CameraScreenshotUiHandler.gameObject.SetActive(false);
        }
        private void Update()
        {
#if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE_WIN
            HandleKeyboard();
#endif
        }

#if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE_WIN
        private void HandleKeyboard()
        {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && EnableCtrl)
            {
                if (Input.GetKeyDown(KeycodeToScreenshot))
                {
                    // m_ScreenShotManager.
                    Debug.Log("Screenshot with control");
                    Screenshoot();
                }
            }
            else if (Input.GetKeyDown(KeycodeToScreenshot) && !EnableCtrl)
            {
                Debug.Log("Screenshot");
                Screenshoot();
            }
        }

#endif
        private void Screenshoot()
        {
            m_ScreenShotManager.TakeScreenshot();
            m_CameraScreenshotUiHandler.Shot(ShutterSpeed, AnimateXAxis);
        }

        public void ShowUi(bool i_CloseUiAtShot = false)
        {
            m_IsUiShown = true; 
        }
    }
}
