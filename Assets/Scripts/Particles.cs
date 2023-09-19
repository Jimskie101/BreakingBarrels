using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGVA;

public class Particles : MonoBehaviour
{

    public ePoolType poolType;
    public void OnParticleSystemStopped()
    {
        PoolManager.Instance.Queue(poolType, this.gameObject);   
    }
}
