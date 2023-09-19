using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DictionaryPoolTypeListGameObject : UnitySerializedDictionary<ePoolType, GameObjectList>
{
}

[Serializable]
public class DictionaryPoolTypeListTransform : UnitySerializedDictionary<ePoolType, Transform>
{
}

[Serializable]
public class GameObjectList
{
    public List<GameObject> List;

    public GameObjectList()
    {
        List = new List<GameObject>();
    }
    public GameObjectList(List<GameObject> i_List)
    {
        List = i_List;
    }
}


public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    protected List<TKey> keyData = new List<TKey>();

    [SerializeField, HideInInspector]
    protected List<TValue> valueData = new List<TValue>();

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        Clear();
        //Debug.LogError(this.keyData.Count + "   " + valueData.Count);

        for (int i = 0; i < this.keyData.Count; i++)
        {
            this[keyData[i]] = valueData[i];
        }
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        keyData.Clear();
        valueData.Clear();

        foreach (var entry in this)
        {
            keyData.Add(entry.Key);
            valueData.Add(entry.Value);
        }
    }
}