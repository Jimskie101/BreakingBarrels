using UnityEngine;
using System.IO;

namespace RGVA.Resources
{
    public class SaveAndLoadManager : MonoBehaviour
    {
        #region Save Image
        public static void SaveImage(string i_FileName, byte[] i_Bytes)
        {
            App.CreateDirectory(App.GetStreamingAssetsPath());
            File.WriteAllBytes(App.GetStreamingAssetsPath() + "/" + i_FileName, i_Bytes);
        }
        public static void SaveImagePNG(string i_FileName, Texture2D i_Texture2D)
        {
            App.CreateDirectory(App.GetStreamingAssetsPath());
            File.WriteAllBytes(App.GetStreamingAssetsPath() + "/" + i_FileName + (i_FileName.Contains(".png")? "" : ".png"), i_Texture2D.EncodeToPNG());
        }

        public static void SaveImageJPG(string i_FileName, Texture2D i_Texture2D)
        {
            App.CreateDirectory(App.GetStreamingAssetsPath());
            File.WriteAllBytes(App.GetStreamingAssetsPath() + "/" + i_FileName + (i_FileName.Contains(".jpg") ? "" : ".jpg"), i_Texture2D.EncodeToJPG());
        }
        #endregion
    }
}
