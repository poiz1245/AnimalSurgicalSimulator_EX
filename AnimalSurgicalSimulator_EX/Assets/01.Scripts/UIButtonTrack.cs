using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonTrack : MonoBehaviour
{
    Camera mainCamera;
    float minZ = 0.72f; // 최소 Z 값
    float maxZ = 1.5f;  // 최대 Z 값

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 카메라의 위치와 방향에 따라 새로운 Z 위치 계산
        Vector3 deltaPosition = mainCamera.transform.position + (mainCamera.transform.forward * 1.0f); // 1.0f는 원하는 거리

        // Z 위치를 minZ와 maxZ 사이로 제한
        float clampedZ = Mathf.Clamp(deltaPosition.z, minZ, maxZ);

        // 새로운 위치 설정: X와 Y는 그대로 유지하고, Z는 clampedZ로 설정
        transform.position = new Vector3(transform.position.x, transform.position.y, clampedZ);
    }
}
