using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;
    [SerializeField] List<MonoBehaviour> taskList;
    int currentTaskIndex = 0;
    public bool isNextTask = false;

    public DigComplete digComplete;

    //[SerializeField] DigTask digTask;
    public enum TaskName
    {
        Start,
        Attach,
        Process,
        Complete
    }

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
    public void NextTask()
    {
        currentTaskIndex++;
        if (currentTaskIndex < taskList.Count)
        {
            task = TaskName.Start;
            taskList[currentTaskIndex].enabled = true;  // 다음 태스크 활성화
        }
        else if (currentTaskIndex == taskList.Count)
        {
            TaskArrow.Instance.isCompleteArrow = true;
            Debug.Log("테스크 종료");
            return;
        }
    }

    // 현재 태스크 반환
    public MonoBehaviour GetCurrentTask()
    {
        return taskList[currentTaskIndex];
    }
}
