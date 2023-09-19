using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace RGVA.Encryption
{
    public class Encryption
    {
        public static string Hash(string data)
        {
            var textToBytes = Encoding.UTF8.GetBytes(data);
            var mySha256 = new SHA256Managed();

            var hashValue = mySha256.ComputeHash(textToBytes);

            return GetHexStringFromHash(hashValue);
        }

        private static string GetHexStringFromHash(byte[] hash)
        {
            var hexString = string.Empty;
            foreach (var b in hash)
                hexString += b.ToString("x2");
            return hexString;
        }

        public static string EncryptDecrypt(string data, int key = 0)
        {
            if (key == 0)
            {
                var playerPrefsKey = "f6qEkdElp5DsocHkY5iB";
                if (PlayerPrefs.HasKey(playerPrefsKey))
                    key = PlayerPrefs.GetInt(playerPrefsKey);
                else
                {
                    key = Random.Range(1,99999);
                    PlayerPrefs.SetInt(playerPrefsKey, key);
                }
            }

            var input = new StringBuilder(data);
            var output = new StringBuilder(data.Length);

            char character;

            for (int i = 0; i < data.Length; i++)
            {
                character = input[i];
                character = (char)(character ^ key);
                output.Append(character);
            }

            return output.ToString();
        }
    }
}


