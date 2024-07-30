using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;
    
    public DigComplete digComplete;
    //[SerializeField] List<MonoBehaviour> taskList;
    //int currentTaskIndex = 0;
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
    //public void NextTask()
    //{
    //    if (currentTaskIndex < taskList.Count - 1) // 현재 인덱스가 마지막 태스크보다 작을 때만 증가
    //    {
    //        taskList[currentTaskIndex].enabled = false; // 현재 태스크 비활성화
    //        currentTaskIndex++;
    //        task = TaskName.Start;
    //        taskList[currentTaskIndex].enabled = true;  // 다음 태스크 활성화
    //    }
    //    else
    //    {
    //        Debug.Log("No more tasks available."); // 모든 태스크가 완료된 경우 메시지 출력
    //    }
    //}


    //// 현재 태스크 반환
    //public MonoBehaviour GetCurrentTask()
    //{
    //    return taskList[currentTaskIndex];
    //}
}
