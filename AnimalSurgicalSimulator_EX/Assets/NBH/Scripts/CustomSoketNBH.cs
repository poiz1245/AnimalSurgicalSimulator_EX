using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomSocketNBH : XRSocketInteractor
{
    public List<GameObject> allowedObjects = new List<GameObject>();

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        // 기본 조건이 충족되는지 확인
        if (!base.CanSelect(interactable))
            return false;

        // 오브젝트가 허용된 오브젝트 목록에 있는지 확인
        return allowedObjects.Contains(interactable.transform.gameObject);
    }
}
