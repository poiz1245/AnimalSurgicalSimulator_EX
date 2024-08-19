using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashHandSensor : MonoBehaviour
{
    [SerializeField] Transform waterPosition;

    bool isTriggering = false;
    bool isRayOn = false;

    float triggerTime = 0f;
    float requiredTriggerTime = 1f; // 트리거를 유지해야 하는 시간 (1초)
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            Debug.Log("센서 감지");
            isTriggering = true;
            triggerTime = 0f; // 트리거 시작 시 타이머 초기화
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isTriggering)
        {
            triggerTime += Time.deltaTime; // 트리거 유지 시간 누적

            if (triggerTime >= requiredTriggerTime)
            {
                if (!isRayOn)
                {
                    ShootRay();
                    isRayOn = true;
                }
                else
                {
                    StopRay();
                    isRayOn = false;
                }
                isTriggering = false; // 트리거 상태 초기화
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject)
        {
            isTriggering = false;
            triggerTime = 0f; // 트리거 종료 시 타이머 초기화
        }
    }

    void ShootRay()
    {
        Ray ray = new Ray(waterPosition.position, waterPosition.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("레이에 맞은 오브젝트: " + hit.collider.name);
            // 필요한 경우 추가적인 로직을 여기에 작성
        }

        Debug.DrawRay(waterPosition.position, waterPosition.forward * 10, Color.red, 2f); // 디버그용 레이
    }

    void StopRay()
    {
        // 레이를 끄는 로직을 여기에 작성
        Debug.Log("레이 종료");
    }
}
