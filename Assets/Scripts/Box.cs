using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using RGVA;
using DG.Tweening;


public class Box : MonoBehaviour
{
    public float breakForce;


    [Button]
    private void InitializeVariables()
    {


        
    }

    public void DestroyBox()
    {
        AudioManager.Instance.Play("BoxBroken");

        this.gameObject.GetComponentInParent<Tile>().opened = true;
        this.gameObject.GetComponentInParent<Tile>().CallValidator();
        


        GameObject fractured = PoolManager.Instance.Dequeue(ePoolType.Fractured, transform.position);
        foreach (Rigidbody rb in fractured.GetComponentsInChildren<Rigidbody>()) 
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddForce(force, ForceMode.Impulse);
            rb.gameObject.transform.DOScale(0, 1f);
        }

        
        PoolManager.Instance.Queue(ePoolType.Intact, this.gameObject);
    
    }

    private void OnEnable()
    {
        Sequence anim = DOTween.Sequence();


        anim.Append(transform.DOScale(new Vector3(150f * 0.7f, 116.379318f * 0.7f, 951.563904f * 0.7f), 0f));
        anim.Append(transform.DOScale(new Vector3(150f * 1.2f, 116.379318f * 1.2f, 951.563904f * 1.2f), 0.5f));
        anim.Append(transform.DOScale(new Vector3(150f, 116.379318f, 951.563904f), 0.5f));
        
    }



}
