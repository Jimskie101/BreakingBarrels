using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage sImage;
    [SerializeField] private float x, y;

    // Update is called once per frame
    void Update()
    {
        sImage.uvRect = new Rect(sImage.uvRect.position + new Vector2(x, y) * Time.deltaTime, sImage.uvRect.size);


    }
}
