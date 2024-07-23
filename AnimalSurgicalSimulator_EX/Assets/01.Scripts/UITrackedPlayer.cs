using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UITrackedPlayer : MonoBehaviour
{
    Camera mainCamera;
    float initialDistance;

    private void Start()
    {
        mainCamera = Camera.main;
        initialDistance = Vector3.Distance(transform.position, mainCamera.transform.position);

    }
    void Update()
    {
        transform.position = mainCamera.transform.position + (mainCamera.transform.forward * initialDistance);
        transform.LookAt(mainCamera.transform);
    }
}
