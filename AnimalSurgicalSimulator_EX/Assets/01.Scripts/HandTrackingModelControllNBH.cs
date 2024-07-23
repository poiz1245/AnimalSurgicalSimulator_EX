using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Interaction.Toolkit;

public class HandTrackingModelControllNBH : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject handModel;
    [SerializeField] GameObject grabObject;

    [SerializeField] Transform indicatorAttach;
    [SerializeField] Transform drillAttach;

    [SerializeField] XRSocketInteractor socketInteractor;
    [SerializeField] XRGrabInteractable grabInteractor;
    [SerializeField] HandTrackingDrillTrigger drillTrigger;

    [SerializeField] HandVisualizer handVisualizer;

    float drillSpeed = 0.03f;
    bool isAttach = false;

    public bool currentTaskComplete { get; private set; } = false;
    //public bool currentUITaskComplete { get; private set; } = false;

    public delegate void TaskCompleted(bool taskComplete);
    //public delegate void UITaskCompleted(bool uiTaskComplete);
    public event TaskCompleted IsTaskCompleted;

    private void Start()
    {
        IsTaskCompleted += TaskComplete;
        //IsTaskCompleted += UITaskComplete;
    }

    private void Update()
    {
        float distance = Vector3.Distance(indicator.transform.position, gameObject.transform.position);

        if (!currentTaskComplete && !isAttach && grabInteractor.isSelected && distance <= 0.2f)
        {
            indicator.SetActive(false);
            Attach();
            //IsTaskCompleted?.Invoke(false); // UI를 표시하기 위해 false 전달
        }
        else if (isAttach && grabInteractor.isSelected && distance <= 0.2f)
        {
            handModel.transform.rotation = Quaternion.Euler(new Vector3(90, -90, 0));
            Move();
        }
        else if (!currentTaskComplete && isAttach && distance > 0.2f)
        {
            indicator.SetActive(true);
            Detach();
            //IsTaskCompleted?.Invoke(false); // UI를 표시하기 위해 false 전달
        }
    }

    private void Attach()
    {
        handVisualizer.drawMeshes = false;
        handModel.SetActive(true);

        socketInteractor.transform.SetParent(handModel.transform);
        socketInteractor.transform.position = handModel.transform.position;
        socketInteractor.transform.rotation = handModel.transform.rotation;

        grabObject.transform.SetParent(handModel.transform);
        grabObject.transform.position = drillAttach.transform.position;
        grabObject.transform.rotation = drillAttach.transform.rotation;

        isAttach = true;
    }

    private void Detach()
    {
        handVisualizer.drawMeshes = true;
        handModel.SetActive(false);

        socketInteractor.transform.SetParent(gameObject.transform);
        socketInteractor.transform.position = gameObject.transform.position;
        socketInteractor.transform.rotation = gameObject.transform.rotation;

        grabObject.transform.SetParent(null);
        grabObject.transform.position = socketInteractor.transform.position;

        isAttach = false;
    }

    void Move()
    {
        if (drillTrigger.buttonOn) // 자동으로 움직이는 기능
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
                IsTaskCompleted?.Invoke(currentTaskComplete); // UI를 닫기 위해 true 전달
                Detach();
            }

            handModel.transform.Translate(0, 0, drillSpeed * Time.deltaTime);
        }
    }

    void TaskComplete(bool taskComplete)
    {
        TaskManager.instance.digComplete.TaskComplete();
    }

    //void UITaskComplete(bool uiTaskComplete)
    //{
    //    if (!uiTaskComplete)
    //    {
    //        TaskUIManager.instance.CloseTaskCompleteUI(); // 현재 UI 비활성화
    //    }
    //    else
    //    {
    //        TaskUIManager.instance.ShowTaskCompleteUI(); // 다음 UI 활성화
    //    }
    //}
}
