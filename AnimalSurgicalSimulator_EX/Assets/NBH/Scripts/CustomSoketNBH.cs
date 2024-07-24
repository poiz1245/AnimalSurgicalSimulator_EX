using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomSocketNBH : MonoBehaviour
{
    [SerializeField] Transform attach;
    [SerializeField] LayerMask socketLayer;
    [SerializeField] GameObject hoverMesh;
    [SerializeField] List<XRGrabInteractable> selectObjectGrabInteractable;

    [SerializeField] List<CustomSocketNBH> sokets; // 소켓 리스트

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
        for (int i = 0; i < sokets.Count; i++)
        {
            // 선택된 오브젝트가 없을 경우
            if (!selectObjectGrabInteractable[i].isSelected)
            {
                hasSelection = true;
                hoverMesh.SetActive(false);
                Debug.Log("소켓등록");
                // 해당 소켓의 위치로 이동
                selectObjectGrabInteractable[i].transform.position = sokets[i].attach.position;
                selectObjectGrabInteractable[i].transform.rotation = sokets[i].attach.rotation;
                
                return; // 해당 오브젝트가 이동한 후 반복문 종료
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
