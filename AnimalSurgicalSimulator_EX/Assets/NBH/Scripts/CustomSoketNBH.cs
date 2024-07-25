using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomSocketNBH : MonoBehaviour
{
    [SerializeField] List<Transform> attach; // 소켓의 위치
    [SerializeField] LayerMask socketLayer; // 소켓 레이어
    [SerializeField] GameObject hoverMesh; // 호버 메쉬
    [SerializeField] List<XRGrabInteractable> selectObjectGrabInteractable; // 그랩 가능한 오브젝트 리스트

    public bool hasSelection = false;

    private void OnTriggerEnter(Collider other)
    {
        if ((socketLayer & (1 << other.gameObject.layer)) != 0)
        {
            hoverMesh.SetActive(true);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < selectObjectGrabInteractable.Count; i++)
        {
            // 해당 오브젝트가 트리거된 경우
            if (selectObjectGrabInteractable[i] != null && other.gameObject == selectObjectGrabInteractable[i].gameObject)
            {
                // 인덱스가 같을 경우에만 이동
                if (i < attach.Count) // attach 리스트가 충분한지 확인
                {
                    // 해당 인덱스의 attach로 이동
                    selectObjectGrabInteractable[i].transform.position = attach[i].position;
                    selectObjectGrabInteractable[i].transform.rotation = attach[i].rotation;

                    hasSelection = true;
                    hoverMesh.SetActive(false);
                }
                break; // 하나의 오브젝트만 처리
            }
        }

        // 만약 해당 오브젝트가 소켓에 부착되지 않았다면 hoverMesh를 활성화
        if (!hasSelection)
        {
            hoverMesh.SetActive(true);
            hasSelection = false;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        hoverMesh.SetActive(false);
    }
}
