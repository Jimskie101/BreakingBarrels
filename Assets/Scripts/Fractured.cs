using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGVA;

public class Fractured : MonoBehaviour
{
   

    public List<Vector3> pos = new List<Vector3>();
    public List<Quaternion> rot = new List<Quaternion>();



    private void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        { 
            transform.GetChild(i).localPosition = pos[i];
            transform.GetChild(i).localRotation = rot[i];
            transform.GetChild(i).transform.localScale = new Vector3(1f, 1f, 1f);
            transform.GetChild(i).transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.GetChild(i).transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }


    private void OnEnable()
    {
        StartCoroutine(RemoveDebris());
    }

    IEnumerator RemoveDebris()
    {   
        yield return new WaitForSeconds(3f);
        Reset();
        PoolManager.Instance.Queue(ePoolType.Fractured, this.gameObject);
    
    }
}
