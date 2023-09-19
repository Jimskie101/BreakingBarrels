using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorToWorld : MonoBehaviour
{
    public GameObject cursor; //drag n drop UI object in inspector
    public Canvas canvas; //drag n drop canvas in inspector
    float pixelX;
    float pixelY;
    float posX;
    float posY;

    void Start()
    {
        pixelX = canvas.worldCamera.pixelWidth;
        pixelY = canvas.worldCamera.pixelHeight;
    }

    void Update()
    {
        cursor.transform.position = Input.mousePosition;
        CheckRay();
    }

    void CheckRay()
    {

        posX = cursor.transform.localPosition.x + pixelX / 2f;
        posY = cursor.transform.localPosition.y + pixelY / 2f;
        Vector3 cursorPos = new Vector3(posX, posY, 0);
        //OR- Ray ray = Camera.main.ScreenPointToRay(new Vector3(posX, posY, 0));
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

    }
}
