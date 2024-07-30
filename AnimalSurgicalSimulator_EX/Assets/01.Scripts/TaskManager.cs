using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;
    //[SerializeField] List<MonoBehaviour> taskList;
    

    public DigComplete digComplete;
    public enum MainTask
    {
        Mes,
        Dig
        //Clamp,
    }
    public MesComplete mesComplete;

    [SerializeField] DigTask digTask;
    public enum TaskName
    {
        Start,
        Attach,
        Process,
        Complete
    }

    public MainTask currentMainTask = MainTask.Mes;
    public TaskName task = TaskName.Start; // 현재 작업 상태
    public bool isNextTask = false;
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
                //currentMainTask = MainTask.Clamp;
                currentMainTask = MainTask.Dig;
                break;
            //case MainTask.Clamp:
            //    currentMainTask = MainTask.Dig;
            //    break;
            case MainTask.Dig:
                TaskArrow.Instance.isCompleteArrow = true;
                Debug.Log("모든 메인 작업 완료");
                return;
        }

        task = TaskName.Start; // 새 태스크를 시작 상태로 초기화
        Debug.Log("새로운 메인 작업: " + currentMainTask);
    }

    public void UpdateTask(TaskName newTaskStatus)
    {
        task = newTaskStatus;
        if (task == TaskName.Complete)
        {
            NextTask(); // 다음 태스크로 넘어가기
        }
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
