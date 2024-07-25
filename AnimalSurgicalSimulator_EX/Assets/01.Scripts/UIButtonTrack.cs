using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonTrack : MonoBehaviour
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
        Vector3 deltaPosition = mainCamera.transform.position + (mainCamera.transform.forward * initialDistance);
        transform.position = new Vector3(deltaPosition.x, transform.position.y, deltaPosition.z);
        transform.LookAt(mainCamera.transform);
    }
}
