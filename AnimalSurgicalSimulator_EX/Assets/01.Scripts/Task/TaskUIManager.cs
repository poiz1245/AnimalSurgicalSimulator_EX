using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUIManager : MonoBehaviour
{
    public static TaskUIManager instance;

    [SerializeField] GameObject[] taskCompleteUI; // 작업 완료 UI 요소
    private int currentTaskIndex = 0; // 현재 표시할 작업 인덱스

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

    public void ShowTaskCompleteUI()
    {
        if (currentTaskIndex < taskCompleteUI.Length)
        {
            Debug.Log("Open UI");
            taskCompleteUI[currentTaskIndex].SetActive(true); // 현재 작업 UI 활성화
            currentTaskIndex++; // 다음 작업으로 이동
        }
    }

    public void CloseTaskCompleteUI()
    {
        taskCompleteUI[currentTaskIndex].SetActive(false); // 현재 작업 UI 비활성화
        if (currentTaskIndex > 0)
        {
            Debug.Log("Close UI");
            currentTaskIndex--; // 이전 작업으로 이동
            taskCompleteUI[currentTaskIndex].SetActive(false); // 현재 작업 UI 비활성화
        }
    }
}
