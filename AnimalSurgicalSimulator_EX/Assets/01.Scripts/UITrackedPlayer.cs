using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UITrackedPlayer : MonoBehaviour
{
    [SerializeField] float offsetY;

    Camera mainCamera;
    float initialDistance;
    private void Start()
    {
        mainCamera = Camera.main;
        transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y + offsetY, transform.position.z);
        initialDistance = Vector3.Distance(transform.position, mainCamera.transform.position);

    }
    void Update()
    {
        Vector3 deltaPosition = mainCamera.transform.position + (mainCamera.transform.forward * initialDistance);
        transform.position = new Vector3(deltaPosition.x, mainCamera.transform.position.y + offsetY, deltaPosition.z);
        transform.LookAt(mainCamera.transform);
    }
}
