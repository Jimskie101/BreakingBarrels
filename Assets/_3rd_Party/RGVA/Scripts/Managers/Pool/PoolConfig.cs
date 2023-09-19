using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace RGVA
{
    [Serializable]
    public class PoolConfig
    {
        public bool UseList = false;
        [HideIf(nameof(UseList))] public GameObject Prefab;
        [ShowIf(nameof(UseList))] public List<GameObject> PrefabList;
        public int InitialQuantity;
    }
}