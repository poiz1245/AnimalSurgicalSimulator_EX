using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUIManager : MonoBehaviour
{
    [SerializeField] GameObject[] taskCompleteUI; // 작업 완료 UI 요소
    [SerializeField] HandModelControll handModelControll;
    private int currentTaskIndex = 0; // 현재 표시할 작업 인덱스

    private void Start()
    {
        handModelControll.IsTaskCompleted += uiTaskCompleted;
    }

    public void uiTaskCompleted(bool isTaskCompleted)
    {
        Debug.Log(isTaskCompleted);
        if (isTaskCompleted)
        {
            Debug.Log("Close UI");
            CloseTaskCompleteUI();
        }
        else
        {
            Debug.Log("Show UI");
            ShowTaskCompleteUI();
        }
    }

    // 작업 완료 UI 표시
    public void ShowTaskCompleteUI()
    {
        if (currentTaskIndex < taskCompleteUI.Length)
        {
            taskCompleteUI[currentTaskIndex].SetActive(true); // 현재 작업 UI 활성화
            currentTaskIndex++; // 다음 작업으로 이동
        }
    }

    public void CloseTaskCompleteUI()
    {
        if (currentTaskIndex > 0)
        {
            currentTaskIndex--; // 이전 작업으로 이동
            taskCompleteUI[currentTaskIndex].SetActive(false); // 현재 작업 UI 비활성화
        }
    }
}
