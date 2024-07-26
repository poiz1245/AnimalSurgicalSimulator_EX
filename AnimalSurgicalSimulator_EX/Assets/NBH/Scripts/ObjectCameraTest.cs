using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCameraTest : MonoBehaviour
{
    Camera mainCamera;

    public Transform targetObject; // 검사할 오브젝트

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 objectPosition = targetObject.position;

        Vector3 directionToTarget = objectPosition - cameraPosition;
        Vector3 cameraRight = Camera.main.transform.right;

        float dotProduct = Vector3.Dot(cameraRight, directionToTarget.normalized);

        if (dotProduct > 0)
        {
            Debug.Log("오브젝트는 카메라 오른쪽에 위치합니다.");
        }
        else
        {
            Debug.Log("오브젝트는 카메라 왼쪽에 위치합니다.");
        }
    }
}
