using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RGVA
{
    [System.Serializable]
    public class DictionaryPoolTypeListPoolConfig : UnitySerializedDictionary<ePoolType, PoolConfig>
    {
    }

    [ExecutionOrder(eExecutionOrder.PoolManager)]
    public class PoolManagerBase : Singleton<PoolManager>
    {
        [SerializeField]//, UnityEngine.Serialization.FormerlySerializedAs("PoolItemsDefinition2")]
        public DictionaryPoolTypeListPoolConfig PoolItemsDefinition = new DictionaryPoolTypeListPoolConfig();
        //[SerializeField]
        //public DictionaryPoolTypeListPoolConfig PoolItemsDefinition1 = new DictionaryPoolTypeListPoolConfig();
        //public Dictionary<ePoolType, PoolConfig> PoolItemsDefinition3 = new Dictionary<ePoolType, PoolConfig>();
        //[Button]
        //public void Copy()
        //{
        //    foreach (var x in PoolItemsDefinition2)
        //    {
        //        PoolItemsDefinition1.Add(x.Key, x.Value);
        //    }
        //}

        [ReadOnly, Header("Pool Objects")] public DictionaryPoolTypeListGameObject ActiveObjects = new DictionaryPoolTypeListGameObject();
        [ReadOnly] public DictionaryPoolTypeListGameObject AvailableObjects = new DictionaryPoolTypeListGameObject();

        //Transforms generated for organization purposes
        [ReadOnly, Header("Pool Holders")] public Transform PoolHolder;
        [ReadOnly] public DictionaryPoolTypeListTransform PoolHolderType = new DictionaryPoolTypeListTransform();

        protected override void OnAwakeEvent()
        {
            base.OnAwakeEvent();

            if (PoolItemsDefinition.Count > 0 && (PoolHolder == null || PoolItemsDefinition.Count != PoolHolder.childCount))
            {
                Prebake();
            }
            else
            {
                CreateFromExistingTransforms();
            }
        }

        public override void Start()
        {
            base.Start();
        }

        public virtual void Reset()
        {
            foreach (var item in ActiveObjects)
            {
                //Debug.LogError(item.Key);
                for (int i = item.Value.List.Count - 1; i >= 0; i--)
                {
                    //Debug.LogError(i);
                    Queue(item.Key, item.Value.List[i]);
                }
            }
        }

        #region Prebake
        [Button]
        private void Prebake()
        {
            if (Application.IsPlaying(this))
                Debug.LogError("Instantiating on Init. Please prebake to save some resources.");
            RemoveTransforms();
            CreateTransforms();
        }

        private void RemoveTransforms()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            ClearLists();
        }

        private void CreateTransforms()
        {
            PoolHolder = new GameObject("Pool Holder").transform;
            PoolHolder.transform.parent = transform;

            foreach (KeyValuePair<ePoolType, PoolConfig> item in PoolItemsDefinition)
            {
                var m_DummyTransform = new GameObject(item.Key.ToString()).transform;
                m_DummyTransform.SetParent(PoolHolder);
                PoolHolderType.Add(item.Key, m_DummyTransform);

                List<GameObject> m_DummyList = new List<GameObject>();

                for (int i = 0; i < item.Value.InitialQuantity; i++)
                {
                    if (!item.Value.UseList)
                    {
                        if (item.Value.Prefab == null)
                        {
                            Debug.LogError("Prefab is Null. Check Pool Manager dictionary " + item.Key + " prefab.");
                            continue;
                        }
                        else
                        {
#if UNITY_EDITOR
                            var m_DummyGameObject = PrefabUtility.InstantiatePrefab(item.Value.Prefab) as GameObject;
#else
                            var m_DummyGameObject = Instantiate(item.Value.Prefab);
#endif
                            m_DummyGameObject.SetActive(false);
                            m_DummyGameObject.name = item.Key + " Pool " + i;
                            m_DummyGameObject.transform.SetParent(PoolHolderType[item.Key]);
                            m_DummyList.Add(m_DummyGameObject);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < item.Value.PrefabList.Count; j++)
                        {
                            if (item.Value.PrefabList[j] == null)
                            {
                                Debug.LogError("Prefab is Null. Check Pool Manager dictionary " + item.Key + " prefab.");
                                continue;
                            }
                            else
                            {
#if UNITY_EDITOR
                                var m_DummyGameObject = PrefabUtility.InstantiatePrefab(item.Value.PrefabList[j]) as GameObject;
#else
                                var m_DummyGameObject = Instantiate(item.Value.Prefab);
#endif
                                m_DummyGameObject.SetActive(false);
                                m_DummyGameObject.name = item.Key + " Pool " + i;
                                m_DummyGameObject.transform.SetParent(PoolHolderType[item.Key]);
                                m_DummyList.Add(m_DummyGameObject);
                            }
                        }

                    }

                }

                AvailableObjects.Add(item.Key, new GameObjectList(m_DummyList));
                ActiveObjects.Add(item.Key, new GameObjectList());
            }
#if UNITY_EDITOR
            //Sirenix.OdinInspector.Editor.OdinPrefabUtility.UpdatePrefabInstancePropertyModifications(Prefab, true);
            //PrefabUtility.MergeAllPrefabInstances(PrefabUtility.GetCorrespondingObjectFromSource(Prefab));
            //PrefabUtility.ReplacePrefab(this.transform.parent.gameObject, PrefabUtility.GetCorrespondingObjectFromSource(this.transform.parent), ReplacePrefabOptions.ConnectToPrefab);
#endif
        }
        #endregion

        #region CreateFrom Existing Transforms
        private void CreateFromExistingTransforms()
        {
            //Debug.LogError("CreateFromExistingTransforms");
            ClearLists();
            RebuildLists();
        }

        private void RebuildLists()
        {
            foreach (KeyValuePair<ePoolType, PoolConfig> item in PoolItemsDefinition)
            {
                var m_DummyTransform = transform.FindDeepChild<Transform>(item.Key.ToString());
                PoolHolderType.Add(item.Key, m_DummyTransform);

                List<GameObject> m_DummyList = new List<GameObject>();

                for (int i = 0; i < m_DummyTransform.childCount; i++)
                {
                    if (m_DummyTransform.GetChild(i) != PoolHolder)
                        m_DummyList.Add(m_DummyTransform.GetChild(i).gameObject);
                }

                AvailableObjects.Add(item.Key, new GameObjectList(m_DummyList));
                ActiveObjects.Add(item.Key, new GameObjectList());
            }
        }

        [Button]
        private void ClearLists()
        {
            ActiveObjects.Clear();
            AvailableObjects.Clear();
            PoolHolderType.Clear();

            //#if UNITY_EDITOR
            //            Sirenix.OdinInspector.Editor.OdinPrefabUtility.UpdatePrefabInstancePropertyModifications(PrefabUtility.FindPrefabRoot(this.gameObject), true);
            //            //Sirenix.OdinInspector.Editor.OdinPrefabUtility.UpdatePrefabInstancePropertyModifications(PrefabUtility.GetCorrespondingObjectFromSource(this), true);
            //#endif
            //#if UNITY_EDITOR
            //            Sirenix.OdinInspector.Editor.OdinPrefabUtility.UpdatePrefabInstancePropertyModifications(this, true);
            //            //Sirenix.OdinInspector.Editor.OdinPrefabUtility.UpdatePrefabInstancePropertyModifications(PrefabUtility.GetCorrespondingObjectFromSource(this), true);
            //            //Sirenix.OdinInspector.Editor.OdinPrefabUtility.UpdatePrefabInstancePropertyModifications(PrefabUtility.GetCorrespondingObjectFromSource(this), true);
            //            //Sirenix.OdinInspector.Editor.OdinPrefabUtility.UpdatePrefabInstancePropertyModifications(PrefabUtility.GetCorrespondingObjectFromSource(this), true);
            //#endif
        }
        #endregion


        public GameObject Dequeue(ePoolType i_type)
        {
            var m_DummyGameObject = DequeueObject(i_type);

            m_DummyGameObject.SetActive(true);

            return m_DummyGameObject;
        }

        public GameObject Dequeue(ePoolType i_type, Vector3 i_Position)
        {
            var m_DummyGameObject = DequeueObject(i_type);
            m_DummyGameObject.transform.position = i_Position;
            m_DummyGameObject.SetActive(true);

            return m_DummyGameObject;
        }

        private GameObject DequeueObject(ePoolType i_type)
        {
            GameObject m_DummyGameObject = null;

            if (AvailableObjects[i_type].List.Count > 0)
            {
                if (PoolItemsDefinition[i_type].UseList)
                {
                    m_DummyGameObject = AvailableObjects[i_type].List[Random.Range(0, AvailableObjects[i_type].List.Count)];
                    AvailableObjects[i_type].List.Remove(m_DummyGameObject);
                }
                else
                {
                    m_DummyGameObject = AvailableObjects[i_type].List[AvailableObjects[i_type].List.Count - 1];
                    AvailableObjects[i_type].List.Remove(m_DummyGameObject);
                }
            }
            else
            {
                if (PoolItemsDefinition[i_type].UseList)
                {
                    m_DummyGameObject = Instantiate(PoolItemsDefinition[i_type].PrefabList[Random.Range(0, PoolItemsDefinition[i_type].PrefabList.Count)]);

                    if (m_DummyGameObject == null)
                    {
                        Debug.LogError("Prefab List value is Null. Check Pool Manager dictionary -" + i_type + " entry.");

                        return null;
                    }
                }
                else
                {
                    m_DummyGameObject = Instantiate(PoolItemsDefinition[i_type].Prefab);


                    if (m_DummyGameObject == null)
                    {
                        Debug.LogError("Prefab is Null. Check Pool Manager dictionary -" + i_type + " entry.");

                        return null;
                    }

                }

                m_DummyGameObject.name = i_type + " Pool ";
                m_DummyGameObject.transform.SetParent(PoolHolderType[i_type]);
            }

            ActiveObjects[i_type].List.Add(m_DummyGameObject);

            return m_DummyGameObject;
        }

        //We can use the extension method gameobject.Queue(i_type) instead of directly calling this function 
        public void Queue(ePoolType i_Type, GameObject i_Object)
        {
            if (ActiveObjects[i_Type].List.Contains(i_Object))
                ActiveObjects[i_Type].List.Remove(i_Object);
            if (AvailableObjects[i_Type].List.Contains(i_Object))
                return;
            AvailableObjects[i_Type].List.Add(i_Object);

            i_Object.transform.SetParent(PoolHolderType[i_Type]);
            i_Object.transform.position = Vector3.zero;
            i_Object.transform.rotation = Quaternion.identity;

            i_Object.name = i_Type + " Pool ";
            i_Object.SetActive(false);
        }
    }
}