using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonTrack : MonoBehaviour
{
    [SerializeField] Transform player; // 플레이어의 Transform
    float minZ = 0.72f; // 최소 Z 값
    float maxZ = 1.5f;  // 최대 Z 값

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("플레이어를 설정해주세요.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // 플레이어의 위치를 기준으로 버튼의 새로운 위치를 설정
            Vector3 newPosition = player.position ;

            // Z 위치를 minZ와 maxZ 사이로 제한
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

            // 새로운 위치 설정: X와 Y는 플레이어의 위치를 그대로 유지하고, Z는 clampedZ로 설정
            transform.position = new Vector3(transform.position.x, transform.position.y, newPosition.z);
        }
    }
}


