using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    public DigComplete digComplete;
    public MesComplete mesComplete;
    public ClampComplete clampComplete;
    public bool isNextTask = false;

    //private TaskStateMachine stateMachine;

    [SerializeField] DigTask digTask;
    [SerializeField] ClampTask clampTask;
    [SerializeField] MesTask mesTask;
    [SerializeField] ObjectGrabGuideControll objectGrabGuideControll; //손 가이드 메쉬 호출
    public enum MainTask
    {
        Mes,
        Clamp,
        Dig
    }

    public enum TaskName
    {
        Start,
        Attach,
        Process,
        Complete
        , Next
    }

    public MainTask currentMainTask = MainTask.Mes;
    public TaskName task = TaskName.Start; // 현재 작업 상태


    public delegate void MainTaskChanged(MainTask newMainTask);
    public event MainTaskChanged OnMainTaskChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetActiveTask(currentMainTask);
        objectGrabGuideControll.UpdateMeshes(currentMainTask); // 손 메쉬 업데이트
    }

    public void UpdateTask(TaskName newTaskStatus)
    {
        task = newTaskStatus;
        if (task == TaskName.Next)
        {
            ProceedToNextMainTask(); // 다음 태스크로 전환
            TaskManager.instance.isNextTask = false;
        }
    }

    private void ProceedToNextMainTask()
    {
        MainTask nextTask = currentMainTask;

        switch (currentMainTask)
        {
            case MainTask.Mes:
                nextTask = MainTask.Clamp;
                break;
            case MainTask.Clamp:
                nextTask = MainTask.Dig;
                break;
            case MainTask.Dig:
                Debug.Log("모든 메인 작업 완료");
                return;
        }

        SetTaskAndActivate(nextTask);
    }

    private void SetTaskAndActivate(MainTask newMainTask)
    {
        currentMainTask = newMainTask;
        task = TaskName.Start; // 새 태스크를 시작 상태로 초기화
        SetActiveTask(currentMainTask);
        OnMainTaskChanged?.Invoke(currentMainTask); // 이벤트 호출
        Debug.Log("새로운 메인 작업: " + currentMainTask);
    }

    private void SetActiveTask(MainTask newMainTask)
    {
        // 현재 작업 비활성화
        if (digTask != null) digTask.enabled = false;
        if (clampTask != null) clampTask.enabled = false;
        if (mesTask != null) mesTask.enabled = false;

        // 새로운 작업 활성화
        switch (newMainTask)
        {
            case MainTask.Mes:
                if (mesTask != null) mesTask.enabled = true;
                break;
            case MainTask.Clamp:
                if (clampTask != null) clampTask.enabled = true;
                break;
            case MainTask.Dig:
                if (digTask != null) digTask.enabled = true;
                break;
        }

    }
}

