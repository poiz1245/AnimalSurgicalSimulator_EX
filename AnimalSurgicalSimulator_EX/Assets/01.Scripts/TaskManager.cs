using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;
    //[SerializeField] List<MonoBehaviour> taskList;
    int currentTaskIndex = 0;
    public bool isNextTask = false;

    public DigComplete digComplete;
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
    }

    public MainTask currentMainTask = MainTask.Mes;
    public TaskName task = TaskName.Start; // 현재 작업 상태

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
        InitializeMainTask(currentMainTask);
    }
    public void NextTask()
    {
        if (task == TaskName.Complete)
        {
            ProceedToNextMainTask();
        }
    }

    private void ProceedToNextMainTask()
    {
        switch (currentMainTask)
        {
            case MainTask.Mes:
                currentMainTask = MainTask.Clamp;
                break;
            case MainTask.Clamp:
                currentMainTask = MainTask.Dig;
                break;
            case MainTask.Dig:
                TaskArrow.Instance.isCompleteArrow = true;
                Debug.Log("모든 메인 작업 완료");
                return;
        }

        InitializeMainTask(currentMainTask); // 새로운 메인 태스크를 초기화합니다
    }

    public void UpdateTask(TaskName newTaskStatus)
    {
        task = newTaskStatus;
        if (task == TaskName.Complete)
        {
            NextTask(); // 다음 태스크로 넘어가기
        }
    }

    public void SetMainTask(MainTask newMainTask)
    {
        currentMainTask = newMainTask;
        InitializeMainTask(currentMainTask); // 메인 태스크 초기화
    }

    private void InitializeMainTask(MainTask mainTask)
    {
        // 메인 태스크에 따라 각 태스크의 초기화 작업을 수행합니다.
        // 예: TaskArrow.Instance.SetTargets(...); // 필요에 따라 타겟 설정
    }
    //public void NextTask()
    //{
    //    currentTaskIndex++;
    //    if (currentTaskIndex < taskList.Count)
    //    {
    //        task = TaskName.Start;
    //        taskList[currentTaskIndex].enabled = true;  // 다음 태스크 활성화
    //    }
    //    else if (currentTaskIndex == taskList.Count)
    //    {
    //        TaskArrow.Instance.isCompleteArrow = true;
    //        Debug.Log("테스크 종료");
    //        return;
    //    }
    //}

    //// 현재 태스크 반환
    //public MonoBehaviour GetCurrentTask()
    //{
    //    return taskList[currentTaskIndex];
    //}
}
