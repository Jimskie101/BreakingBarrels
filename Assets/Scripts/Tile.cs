using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGVA;

public class Tile : MonoBehaviour
{
    public int ID;
    public string name;
    public SpriteRenderer sprite;


    public bool opened = false;

    public void CallValidator()
    {
        transform.GetComponentInParent<TileManager>().TileValidator(ID);    
        
    }
    private void OnEnable()
    {
        SpawnBox();
    }


    public void SpawnBox()
    {
        opened = false;
        GameObject box = PoolManager.Instance.Dequeue(ePoolType.Intact, transform.position);
        box.transform.parent = transform;

    }

    private void OnDisable()
    {
        
    }



    //public IEnumerator 
}
