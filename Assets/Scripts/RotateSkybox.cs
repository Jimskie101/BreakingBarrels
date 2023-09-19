using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0f;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_SkyboxRotation", Time.time * rotationSpeed);
    }
}
