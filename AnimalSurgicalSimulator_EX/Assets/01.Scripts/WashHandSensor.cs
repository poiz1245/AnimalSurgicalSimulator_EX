using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashHandSensor : MonoBehaviour
{
    [SerializeField] GameObject handTrigger;
    [SerializeField] Transform waterPosition;
    [SerializeField] LineRenderer lineRenderer;

    int triggerCount = 0; // 트리거 횟수 카운트

    void Update()
    {
        if (triggerCount % 2 == 1) // 트리거가 홀수일 때 레이 발사
        {
            ShootRay();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == handTrigger)
        {
            // 트리거 횟수를 증가시킴
            triggerCount++;

            // 홀수일 때 레이 켜고, 짝수일 때 레이 끔
            if (triggerCount % 2 == 1)
            {
                StartRay(); // 레이 켜기
            }
            else
            {
                StopRay(); // 레이 끄기
            }
        }
    }

    private void StartRay()
    {
        lineRenderer.enabled = true; // LineRenderer 활성화
        Debug.Log("레이 켬");
    }

    private void StopRay()
    {
        lineRenderer.enabled = false; // LineRenderer 비활성화
        Debug.Log("레이 끔");
    }

    void ShootRay()
    {
        Debug.Log("레이 발사");
        Ray ray = new Ray(waterPosition.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("레이에 맞은 오브젝트: " + hit.collider.name);
            lineRenderer.SetPosition(0, waterPosition.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            Debug.Log("레이가 아무것도 맞지 않았습니다.");
            Vector3 endPoint = waterPosition.position + Vector3.down.normalized * 1f;
            lineRenderer.SetPosition(0, waterPosition.position);
            lineRenderer.SetPosition(1, endPoint);
        }

        Debug.Log("레이 방향: " + Vector3.down);
    }
}
