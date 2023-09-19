using UnityEngine;

/// <summary>
/// Abstract class for making reload-proof singletons out of ScriptableObjects
/// Returns the asset created on the editor, or null if there is none
/// </summary>
/// <typeparam name="T">Singleton type</typeparam>
namespace RGVA
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        static T s_Instance = null;
        public static T Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    // First let's try 'Resources.FindObjectsOfTypeAll'
                    T[] objects = UnityEngine.Resources.FindObjectsOfTypeAll<T>();

                    if (objects == null || objects.Length == 0)
                    {
                        // Failed, let's try 'Resources.LoadAll'

                        //Debug.LogError($"[SingletonScriptableObject] Type:{typeof(T).FullName}");
                        objects = UnityEngine.Resources.LoadAll<T>(string.Empty);
                    }

                    if (objects.Length > 0 && objects[0] != null)
                    {
                        s_Instance = objects[0];
                    }

                    if (objects.Length > 1)
                    {
                        Debug.LogError("[SingletonScriptableObject] You have more than 1 object of type - " + typeof(T).FullName + ". Choosing first.");
                    }
                    else if (objects.Length == 0)
                    {
                        Debug.LogError("[SingletonScriptableObject] Something is wrong. Can't find the object - " + typeof(T).FullName + ". Make sure it's on the Resources folder.");
                    }
                }
                return s_Instance;
            }
        }
    }
}