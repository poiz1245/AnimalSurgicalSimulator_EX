using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetParentNBH : MonoBehaviour
{
    [SerializeField] Transform rightHand;
    [SerializeField] Transform drillAttach;
    [SerializeField] XRSocketInteractor socketInteractor;

    void Start()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnSelectEntered);
        }
    }

    void OnDestroy()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    public void AddParent()
    {
        if (rightHand.gameObject.activeInHierarchy)
        {
            gameObject.transform.SetParent(rightHand);
            gameObject.transform.position = drillAttach.position;
            gameObject.transform.rotation = drillAttach.rotation;
        }
        else
        {
            Debug.LogWarning("Right Hand Model is not active!");
        }
    }

    public void DeleteParent()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.transform.SetParent(null);
        }
        else
        {
            Debug.LogWarning("GameObject is not active!");
        }
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform == this.transform)
        {
            // 오브젝트가 소켓에 닿았을 때 자식 관계 해제
            DeleteParent();
        }
    }
}
