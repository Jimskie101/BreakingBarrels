using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RGVA;

public class MousePosition3D : Singleton<MousePosition3D>
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask ignoreLayer;


    // Update is called once per frame

    public void MoveTarget()
    {
        Ray ray = mainCamera.ScreenPointToRay(Shooting.Instance.crosshair.position);
        if (Physics.Raycast(ray, out RaycastHit raycastHit,float.MaxValue, ignoreLayer))
        {
            //transform.position = raycastHit.point;
            transform.position = new Vector3(raycastHit.point.x, Mathf.Clamp(raycastHit.point.y, 0, 14f) ,6f);

        }
    }
}
