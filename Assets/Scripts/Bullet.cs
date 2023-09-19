using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGVA;

public class Bullet : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (transform.position.z > 6f)
        {
            PoolManager.Instance.Queue(ePoolType.Bullet, this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Box"))
        {
            collision.gameObject.GetComponent<Box>().DestroyBox();
            PoolManager.Instance.Queue(ePoolType.Bullet, this.gameObject);
        }
        
    }
}
