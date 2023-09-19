using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGVA;
using Sirenix.OdinInspector;
using DG.Tweening;

public class TileClusterManager : Singleton<TileClusterManager>
{
    [System.Serializable]
    public class Cluster 
    {
        public int clusterID;
        public GameObject clusterObject;
    }


    public int overAllPairCount = 0;

    public int clusterNumber = 0;

    public List<Cluster> clusters = new List<Cluster>();



    private void OnEnable()
    {
        clusterNumber = 0;
    }


    [Button]
    void AddClusters()
    {
        clusters.Clear();
        int i = 0;
        for (int j = transform.childCount - 1; j >= 0; j--)
        {
            if (transform.GetChild(j).GetComponent<TileManager>() != null)
            {
                clusters.Add(new Cluster { clusterID = i, clusterObject = transform.GetChild(j).gameObject });
                i++;
            }
        }

    }


    [Button]
    void CountAllClusterPairCount()
    {
        overAllPairCount = 0;
        foreach (Cluster cluster in clusters)
        {
            overAllPairCount += cluster.clusterObject.GetComponent<TileManager>().clusterPairCount;
        }
    }


    [Button]
    public bool NoClustersRemaining()
    {
        if (clusterNumber >= clusters.Count -1)
        {
            return true;
        }
        else
        {
            clusters[clusterNumber].clusterObject.SetActive(true);
            Sequence seq = DOTween.Sequence();
            seq.Append(clusters[clusterNumber].clusterObject.transform.DOLocalMoveY(11.36f, 0.5f));
            seq.Append(clusters[clusterNumber].clusterObject.transform.DOLocalMoveY(12.36f, 0.3f));
            clusterNumber++;

            return false;
        }
    }



}
