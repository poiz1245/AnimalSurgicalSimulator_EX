using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;

public class HandModelControll : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject handModel;
    [SerializeField] GameObject grabObject;

    [SerializeField] Transform indicatorAttach;
    [SerializeField] Transform handModelAttach;
    [SerializeField] Transform drillAttach;

    [SerializeField] XRGrabInteractable grabInteractor;
    [SerializeField] DrillTrigger drillTrigger;

    float drillSpeed = 0.03f;
    public bool isAttach { get; private set; } = false;

    public bool currentTaskComplete { get; private set; } = false;

    public delegate void TaskCompleted(bool taskComplete);
    public event TaskCompleted IsTaskCompleted;


    private void Start()
    {
        IsTaskCompleted += TaskComplete;

    }
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
            handModel.transform.rotation = Quaternion.Euler(new Vector3(90, -90, 0));
            Move();
        }
        else if(isAttach && !grabInteractor.isSelected && distance <= 0.2f)
        {
            indicator.SetActive(true);
            Detach();
        }
        else if (!currentTaskComplete && isAttach && distance > 0.2f)
        {
            indicator.SetActive(true);
            Detach();
        }

    }
    private void Attach()
    {
        grabInteractor.trackPosition = false;
        grabInteractor.trackRotation = false;

        grabObject.transform.SetParent(handModel.transform);
        grabObject.transform.position = drillAttach.transform.position;
        grabObject.transform.rotation = drillAttach.transform.rotation;

        handModel.transform.SetParent(null);
        handModel.transform.position = indicatorAttach.position;

        isAttach = true;
    }

    private void Detach()
    {
        handModel.transform.SetParent(gameObject.transform);
        handModel.transform.position = handModelAttach.position;
        handModel.transform.rotation = handModelAttach.rotation;

        grabObject.transform.SetParent(null);
        grabInteractor.trackPosition = true;
        grabInteractor.trackRotation = true;

        isAttach = false;
    }

    void Move()
    {
        if (drillTrigger.buttonOn) //자동으로 움직이는 기능
        {
            if (drillTrigger.currentTriggerLayerName == "OutsideBone")
            {
                drillSpeed = 0.005f;
            }
            else if (drillTrigger.currentTriggerLayerName == "InsideBone")
            {
                drillSpeed = 0.02f;
            }
            else if (drillTrigger.currentTriggerLayerName == "EndLayer")
            {
                currentTaskComplete = true;
                IsTaskCompleted?.Invoke(currentTaskComplete);
                Detach();
            }

            handModel.transform.Translate(0, 0, drillSpeed * Time.deltaTime);
        }

        /*if (drillTrigger.buttonOn) // 직접 움직이는 기능
        {
            handModel.transform.position = new Vector3(indicatorAttach.position.x, gameObject.transform.position.y, indicatorAttach.position.z);

            if (drillTrigger.currentTriggerLayerName == "EndLayer")
            {
                currentTaskComplete = true;
                IsTaskCompleted?.Invoke(currentTaskComplete);
                Detach();
            }
        }*/
    }

    void TaskComplete(bool taskComplete)
    {
        TaskManager.instance.digComplete.TaskComplete();
    }
}
