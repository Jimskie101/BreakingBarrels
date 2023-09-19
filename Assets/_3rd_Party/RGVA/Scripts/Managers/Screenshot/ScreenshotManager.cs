using System.Collections;
using UnityEngine;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_WIN
using RGVA.Resources;
#endif

namespace RGVA.SreenShot
{
    public class ScreenshotManager : Singleton<ScreenshotManager>
    {
        public delegate void ScreenshotSaveEvent(Sprite sprite);
        public delegate void ScreenshotEvent();
        public event ScreenshotEvent OnStart;
        public event ScreenshotSaveEvent OnFinished;

        private static int m_ScreenshotIndex = 0;


        private IEnumerator Screenshot(int i_Width, int i_Height, string i_ScreenshotName)
        {
            OnStart?.Invoke();
            yield return new WaitForEndOfFrame();

            Texture2D texture = new Texture2D(i_Width, i_Height, TextureFormat.RGB24, false);

            texture.ReadPixels(new Rect(0, 0, i_Width, i_Height), 0, 0);
            texture.Apply();
            //byte
            OnFinished?.Invoke(Sprite.Create(texture, new Rect(0, 0, i_Width, i_Height), Vector2.zero));

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_WIN
            string fileName = i_ScreenshotName + "[" + m_ScreenshotIndex.ToString("00") + "]";
            SaveAndLoadManager.SaveImagePNG(fileName, texture);
            m_ScreenshotIndex++;
#endif
        }

        public void TakeScreenshot(string i_ScreenshotName = "Screenshot")
        {
            StartCoroutine(Screenshot(Screen.width, Screen.height, i_ScreenshotName));
        }
        public void TakeScreenshot(int width, int height, string i_ScreenshotName = "Screenshot")
        {
            StartCoroutine(Screenshot(width, height, i_ScreenshotName));
        }
    }
}