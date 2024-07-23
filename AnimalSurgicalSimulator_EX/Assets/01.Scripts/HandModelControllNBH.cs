using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static HandModelControll;

public class HandModelControllNBH : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject handModel;
    [SerializeField] GameObject grabObject;

    [SerializeField] Transform indicatorAttach;
    [SerializeField] Transform handModelAttach;
    [SerializeField] Transform drillAttach;

    [SerializeField] XRGrabInteractable grabInteractor;
    [SerializeField] DrillTrigger drillTrigger;

    public delegate void TaskCompletedEventHandler(bool taskComplete);
    public event TaskCompletedEventHandler TaskCompleted;
    public event TaskCompleted IsTaskCompleted;

    float drillSpeed = 0.03f;
    bool isAttach = false;

    public bool currentTaskComplete { get; private set; } = false;

    private void Start()
    {
        TaskCompleted += UpdateTaskUI; // UI 업데이트 메서드 구독
        IsTaskCompleted += TaskComplete;
    }

    private void Update()
    {
        float distance = Vector3.Distance(indicator.transform.position, gameObject.transform.position);

            if (!currentTaskComplete && !isAttach && grabInteractor.isSelected && distance <= 0.2f)
            {
                TaskCompleted?.Invoke(false); // 두 번째 UI 활성화
                indicator.SetActive(false);
                Attach();
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
                TaskCompleted?.Invoke(true); // 세 번째 UI 활성화
            }
        
    }

    private void Attach()
    {
        
        grabInteractor.trackPosition = false;
        grabInteractor.trackRotation = false;

        grabObject.transform.SetParent(handModel.transform);
        gameObject.transform.position = drillAttach.position;
        gameObject.transform.rotation = drillAttach.rotation;

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

        if (isAttach)
        {
            TaskCompleted?.Invoke(true); // 세 번째 UI 활성화
        }
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
                IsTaskCompleted?.Invoke(currentTaskComplete);
                Detach();
            }
            handModel.transform.Translate(0, 0, drillSpeed * Time.deltaTime);
        }
    }

    void TaskComplete(bool taskComplete)
    {
        TaskManager.instance.digComplete.TaskComplete(); // 작업 완료 처리
    }

    void UpdateTaskUI(bool taskComplete)
    {
        if (taskComplete)
        {
            Debug.Log("UI 꺼짐");
            TaskManager.instance.taskUIManager.CloseTaskCompleteUI(); // 이전 UI 비활성화
        }
        else if (!taskComplete)
        {
            Debug.Log("UI 켜짐");
            TaskManager.instance.taskUIManager.ShowTaskCompleteUI(); // 두 번째 UI 활성화
        }
    }
}
