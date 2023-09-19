using System.IO;
using UnityEngine;

namespace RGVA.Resources
{
    public class App
    {
        public static string GetStreamingAssetsPath()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            return Application.dataPath + "/StreamingAssets";
#elif UNITY_ANDROID
            return Application.persistentDataPath + "/Files";
#elif UNITY_IOS
            return Application.dataPath + "/Raw";
#else
            return "";
#endif
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }

}


