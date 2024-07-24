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
        for (int i = 0; i < selectObjectGrabInteractable.Count; ++i)
        {
            XRGrabInteractable grabInteractable = selectObjectGrabInteractable[i];

            // 해당 오브젝트가 트리거된 경우
            if (grabInteractable != null && other.gameObject == grabInteractable.gameObject)
            {
                print(selectObjectGrabInteractable[i]);
                // 소켓에 부착
                if (i < attach.Count)
                {
                    grabInteractable.transform.position = attach[i].position;
                    grabInteractable.transform.rotation = attach[i].rotation;

                    hasSelection = true;
                    hoverMesh.SetActive(false);
                }
                break; // 하나의 오브젝트만 처리
            }
            else
            {
                hoverMesh.SetActive(true);
                hasSelection = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hoverMesh.SetActive(false);
    }
}
