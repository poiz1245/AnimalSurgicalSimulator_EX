using System.Collections.Generic;
using UnityEngine;

public class TaskArrow : MonoBehaviour
{
    [SerializeField]List<Transform> targetPositions; // 목표 위치 리스트
    int currentIndex;
    void Update()
    {
        if (currentIndex < targetPositions.Count)
        {
            // 현재 목표 위치로 이동
            Transform targetPosition = targetPositions[currentIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, Time.deltaTime); // 속도 설정

            // z 회전 변경 (스위치문을 사용하여 각 인덱스에 따라 회전 설정)
            switch (currentIndex)
            {
                case 0:
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -90f);
                    break;
                case 1:
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -180f);
                    break;
                // 필요에 따라 추가적인 case를 추가
                default:
                    break;
            }

            // 목표 위치에 도달하면 다음 목표로 변경
            if (Vector3.Distance(transform.position, targetPosition.position) < 0.1f)
            {
                currentIndex++;
            }
        }
    }
}
