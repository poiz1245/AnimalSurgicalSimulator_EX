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

    float drillSpeed;
    bool isAttach = false;

    public bool currentTaskComplete { get; private set; } = false;
    private void Update()
    {
        float distance = Vector3.Distance(indicator.transform.position, gameObject.transform.position);

        if (!currentTaskComplete && !isAttach && grabInteractor.isSelected && distance <= 0.2f)
        {
            indicator.SetActive(false);
            Attach();
        }
        else if (isAttach && grabInteractor.isSelected && distance <= 0.2f)
        {
            handModel.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            Move();

        }
        else if (!currentTaskComplete && isAttach && distance > 0.2f)
        {
            indicator.SetActive(true);
            Detach();
        }
    }
    private void Attach()
    {
        handModel.transform.position = indicatorAttach.position;
        handModel.transform.SetParent(null);
        isAttach = true;
    }

    private void Detach()
    {
        handModel.transform.SetParent(gameObject.transform);
        handModel.transform.position = handModelAttach.position;
        handModel.transform.rotation = handModelAttach.rotation;
        isAttach = false;
    }

    void Move()
    {
        if (drillTrigger.buttonOn) //자동으로 움직이는 기능
        {
            if (drillTrigger.currentTriggerLayerName == "OutsideBone")
            {
                drillSpeed = 0.03f;
            }
            else if (drillTrigger.currentTriggerLayerName == "InsideBone")
            {
                drillSpeed = 0.06f;
            }
            else if (drillTrigger.currentTriggerLayerName == "EndLayer")
            {
                currentTaskComplete = true;
                Detach();
            }
            handModel.transform.Translate(0, 0, drillSpeed * Time.deltaTime);
        }

        /*if (drillTrigger.buttonOn) // 직접 움직이는 기능
        {
            handModel.transform.position = new Vector3(indicatorAttach.position.x, indicatorAttach.position.y, gameObject.transform.position.z);

            if (drillTrigger.currentTriggerLayerName == "EndLayer")
            {
                currentTaskComplete = true;
                Detach();
            }
        }*/
    }
}
