using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCameraTest : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform target; // 바라볼 오브젝트
    [SerializeField] float lookThreshold; // 바라보는 각도 허용 범위

    void Update()
    {
        
        // 카메라의 위치와 바라보는 방향
        //Vector3 directionToTarget = target.position - mainCamera.transform.position;
        float angle = Vector3.Angle(mainCamera.transform.forward, target.position);

        // 각도가 허용 범위 내에 있을 경우 디버그 메시지 출력
        if (angle < lookThreshold)
        {
            Debug.Log("사용자가 오브젝트를 바라보고 있습니다!");
        }
        else
        {
            // 각도를 디버그 메시지로 출력
            Debug.Log($"오브젝트가 카메라의 각도 범위를 벗어났습니다. 현재 각도: {angle}도, 허용된 각도: {lookThreshold}도");
        }
    }
}
