using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;

public class HandModelControll : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject handModel;

    [SerializeField] Transform indicatorAttach;
    [SerializeField] Transform handModelAttach;
    [SerializeField] Transform moveEndPoint;

    [SerializeField] XRGrabInteractable grabInteractor;
    [SerializeField] DrillTrigger drillTrigger;

    bool isAttach = false;

    private void Update()
    {
        float distance = Vector3.Distance(indicator.transform.position, gameObject.transform.position);
        AttachIndicator(distance);
    }

    private void AttachIndicator(float distance)
    {
        if (!isAttach && grabInteractor.isSelected && distance <= 0.2f)
        {
            indicator.SetActive(false);

            handModel.transform.position = indicatorAttach.position;
            handModel.transform.SetParent(null);
            isAttach = true;
        }
        else if (isAttach && grabInteractor.isSelected && distance <= 0.2f)
        {
            handModel.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            Move();

        }
        else if ((isAttach && distance > 0.2f))
        {
            indicator.SetActive(true);
            handModel.transform.SetParent(gameObject.transform);
            handModel.transform.position = handModelAttach.position;
            handModel.transform.rotation = handModelAttach.rotation;
            isAttach = false;
        }
    }

    void Move()
    {
        if (drillTrigger.buttonOn)
        {
            handModel.transform.DOMove(moveEndPoint.position, 2).SetEase(Ease.InOutCirc);
        }
        //handModel.transform.position = new Vector3(indicatorAttach.position.x, indicatorAttach.position.y, gameObject.transform.position.z);
    }
}
