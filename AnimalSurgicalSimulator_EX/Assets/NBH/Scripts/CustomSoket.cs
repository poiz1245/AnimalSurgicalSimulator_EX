using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomSocketHandler : MonoBehaviour
{
    public XRSocketInteractor socketInteractor;
    public Transform[] targetPositions; // 이동할 위치 배열

    private void OnEnable()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnSelectEntered);
        }
    }

    private void OnDisable()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // XRGrabInteractable와 ObjectIndex 컴포넌트를 가진 오브젝트만 처리합니다.
        var interactable = args.interactableObject.transform.GetComponent<XRGrabInteractable>();
        var objectIndex = args.interactableObject.transform.GetComponent<SocketIndex>();

        if (interactable != null && objectIndex != null)
        {
            int index = objectIndex.index;

            // 유효한 인덱스인지 확인합니다.
            if (index >= 0 && index < targetPositions.Length)
            {
                // 오브젝트를 지정한 위치로 이동시킵니다.
                args.interactableObject.transform.position = targetPositions[index].position;
                args.interactableObject.transform.rotation = targetPositions[index].rotation;
            }
        }
    }
}