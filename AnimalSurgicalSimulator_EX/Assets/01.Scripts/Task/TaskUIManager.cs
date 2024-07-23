//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TaskUIManager : MonoBehaviour
//{
//    public static TaskUIManager instance;
//    [SerializeField] HandModelControllNBH controllNBH;
//    [SerializeField] GameObject[] taskCompleteUI; // 작업 완료 UI 요소
//    private int currentTaskIndex = 0; // 현재 표시할 작업 인덱스
//    private bool[] taskUIActivated; // 각 UI의 활성화 상태
//    bool startUI = true;
//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//            taskUIActivated = new bool[taskCompleteUI.Length]; // UI 활성화 상태 배열 초기화
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }
//    private void Start()
//    {
//        controllNBH.TaskCompleted += isUiTask;
//    }
//    //public void ShowNextTaskUI()
//    //{
//    //    // 현재 인덱스가 UI 배열의 길이를 초과하지 않고, 이미 활성화되지 않은 경우에만 UI를 활성화
//    //    if (currentTaskIndex < taskCompleteUI.Length && !taskUIActivated[currentTaskIndex])
//    //    {
//    //        Debug.Log("Open UI");
//    //        taskCompleteUI[currentTaskIndex].SetActive(true); // 현재 작업 UI 활성화
//    //        taskUIActivated[currentTaskIndex] = true; // 현재 UI 활성화 상태 기록
//    //        currentTaskIndex++; // 다음 작업으로 이동
//    //    }
//    //    else if (currentTaskIndex >= taskCompleteUI.Length)
//    //    {
//    //        Debug.Log("모든 UI가 이미 활성화되었습니다.");
//    //    }
//    //}

//    //public void CloseCurrentTaskUI()
//    //{
//    //    if (startUI)
//    //    {
//    //        taskCompleteUI[currentTaskIndex].SetActive(false); // 현재 작업 UI 비활성화
//    //        currentTaskIndex++; // 다음 작업으로 이동
//    //        startUI = false;
//    //    }
//    //    if (currentTaskIndex > 0 && !startUI)
//    //    {
//    //        currentTaskIndex--; // 이전 작업으로 이동
//    //        taskCompleteUI[currentTaskIndex].SetActive(false); // 현재 작업 UI 비활성화
//    //        Debug.Log("Close UI");
//    //    }
//    //}

//    public void isUiTask(bool isCompleted)
//    {
//        taskCompleteUI[currentTaskIndex].SetActive(false); // 현재 작업 UI 비활성화
//        currentTaskIndex++;
//        taskCompleteUI[currentTaskIndex].SetActive(true); // 현재 작업 UI 비활성화
//        currentTaskIndex++; // 다음 작업으로 이동
//    }
//}
