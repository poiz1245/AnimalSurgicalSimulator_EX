using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerZPositionTracker : MonoBehaviour
{
    public GameObject controller;

    private Vector3 previousControllerPosition;
    public float zPositionDelta;

    private void Start()
    {
        // 초기 컨트롤러 위치 저장
        previousControllerPosition = controller.transform.position;
    }

    private void Update()
    {
        // 컨트롤러 현재 위치 가져오기
        Vector3 currentControllerPosition = controller.transform.position;

        // z축 거리 변화 계산
        zPositionDelta = currentControllerPosition.z - previousControllerPosition.z;

        // 이전 프레임 위치 업데이트
        previousControllerPosition = currentControllerPosition;

        // z축 거리 변화 사용하여 필요한 동작 수행
        HandleZPositionChange();
    }

    private void HandleZPositionChange()
    {
        // z축 거리 변화에 따른 동작 구현
        if(zPositionDelta >=-0.0001f && zPositionDelta <= 0.0001f)
        {
            zPositionDelta = 0;
        }
        else if (zPositionDelta > 0)
        {
            // 컨트롤러가 전방으로 이동한 경우
            zPositionDelta = 1;
        }
        else if (zPositionDelta < 0)
        {
            // 컨트롤러가 후방으로 이동한 경우
            zPositionDelta = -1;
        }
    }
}
