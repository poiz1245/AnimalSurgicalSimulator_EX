using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AttachIndicator : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject indicatorAttach;
    [SerializeField] Transform handModelAttach;
    [SerializeField] XRGrabInteractable grabInteractor;
    bool isAttach = false;

    private void Update()
    {
        float distance = Vector3.Distance(indicator.transform.position, gameObject.transform.position);

        if (!isAttach && distance <= 0.2f && grabInteractor.isSelected)
        {
            indicator.SetActive(false);

            gameObject.transform.position = indicatorAttach.transform.position;
            isAttach = true;
        }
        else if (isAttach && distance > 0.2f)
        {
            indicator.SetActive(true);
            gameObject.transform.position = handModelAttach.position;
            isAttach = false;
        }
    }
}
