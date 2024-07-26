//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static DigTask; // TaskName의 정의가 있는 파일
//using static TaskManager; // TaskManager의 정의가 있는 파일

//public class LookCameraTest : MonoBehaviour
//{
//    Camera mainCamera;
//    [SerializeField] List<Transform> targets; // 바라볼 오브젝트 리스트
//    [SerializeField] float fieldOfView; // 바라보는 각도 허용 범위
//    [SerializeField] GameObject[] arrow; // 비활성화할 Arrow 오브젝트

//    public delegate void TaskStateChanged(TaskName task);
//    public event TaskStateChanged OnTaskStateChanged;

//    void Start()
//    {
//        mainCamera = Camera.main;
//        OnTaskStateChanged += RotateArrow;
//    }
//    void Update()
//    {
//        for (int i = 0; i < targets.Count; i++)
//        {
//            bool isOnTargets = targets[i].gameObject.activeInHierarchy;

//            if (targets[i] != null)
//            {
//                Vector3 directionToTarget = targets[i].position - mainCamera.transform.position;
//                float angle = Vector3.Angle(mainCamera.transform.forward, directionToTarget);

//                if (angle <= fieldOfView / 2 && isOnTargets)
//                {
//                   TaskName currentTask = TaskManager.instance.task; // 현재 작업 상태를 가져옴
//                   TargetView(i, currentTask); // index와 현재 작업을 넘겨줌
//                }
//            }
//        }
//    }

//    void TargetView(int targetIndex, TaskName currentTask)
//    {
//        switch (targetIndex)
//        {
//            case 0:
//                arrow[0].SetActive(false);
//                if (currentTask == TaskName.Attach && targets[0].gameObject.activeInHierarchy) 
//                {
//                    RotateArrow(TaskName.Attach);
//                }
//                break;
//            case 1:
//                arrow[0].SetActive(false);
//                if (currentTask == TaskName.Dig && targets[1].gameObject.activeInHierarchy)
//                {
//                    RotateArrow(TaskName.Dig);
//                }
//                break;
//            case 2:
//                if (currentTask == TaskName.Complete && targets[2].gameObject.activeInHierarchy)
//                {
//                    RotateArrow(TaskName.Complete);
//                    arrow[0].SetActive(false); // Arrow 비활성화
//                }
//                break;
//            default:
//                arrow[0].SetActive(false); // Arrow 비활성화
//                arrow[1].SetActive(false); // Arrow 비활성화
//                break;
//        }
//    }

//    private void TaskStateChange(TaskName taskName)
//    {
//        TaskManager.instance.task = taskName;
//        OnTaskStateChanged?.Invoke(TaskManager.instance.task);
//    }

//    void RotateArrow(TaskName task)
//    {
//        switch (task)
//        {
//            case TaskName.Attach:
//                if(task == TaskName.Attach)
//                arrow[1].SetActive(true);
//                break;

//            case TaskName.Dig:
//                if (task == TaskName.Dig)
//                    arrow[1].SetActive(true);
//                break;

//            case TaskName.Complete:
//                if (task == TaskName.Complete)
//                    arrow[1].SetActive(true);
//                break;

//            default:
//                break;
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DigTask; // TaskName의 정의가 있는 파일
using static TaskManager; // TaskManager의 정의가 있는 파일

public class LookCameraTest : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] List<Transform> targets; // 바라볼 오브젝트 리스트
    [SerializeField] float fieldOfView; // 바라보는 각도 허용 범위
    [SerializeField] GameObject[] arrow; // 비활성화할 Arrow 오브젝트

    public delegate void TaskStateChanged(TaskName task);
    public event TaskStateChanged OnTaskStateChanged;

    void Start()
    {
        mainCamera = Camera.main;
        //OnTaskStateChanged += RotateArrow;
    }

    void Update()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            bool isOnTargets = targets[i].gameObject.activeInHierarchy;

            if (targets[i] != null)
            {
                Vector3 directionToTarget = targets[i].position - mainCamera.transform.position;
                float angle = Vector3.SignedAngle(mainCamera.transform.forward, directionToTarget, Vector3.up);

                // 시야각과 타겟의 활성화 여부 확인
                if (Mathf.Abs(angle) <= fieldOfView / 2 && isOnTargets)
                {
                    TaskName currentTask = TaskManager.instance.task; // 현재 작업 상태를 가져옴
                    //TargetView(i, currentTask); // index와 현재 작업을 넘겨줌
                }
                else if (isOnTargets)
                {
                    // 시야각에 들어오지 않는 경우 방향에 따라 화살표 활성화
                    if (angle < 0)
                    {
                        // 왼쪽에 위치
                        arrow[0].SetActive(true);
                        arrow[1].SetActive(false);
                    }
                    else
                    {
                        // 오른쪽에 위치
                        arrow[0].SetActive(false);
                        arrow[1].SetActive(true);
                    }
                }
            }
        }
    }

    //void TargetView(int targetIndex, TaskName currentTask)
    //{
    //    switch (targetIndex)
    //    {
    //        case 0:
    //            arrow[0].SetActive(false);
    //            if (currentTask == TaskName.Attach && targets[0].gameObject.activeInHierarchy)
    //            {
    //                RotateArrow(TaskName.Attach);
    //            }
    //            break;
    //        case 1:
    //            arrow[0].SetActive(false);
    //            if (currentTask == TaskName.Dig && targets[1].gameObject.activeInHierarchy)
    //            {
    //                RotateArrow(TaskName.Dig);
    //            }
    //            break;
    //        case 2:
    //            if (currentTask == TaskName.Complete && targets[2].gameObject.activeInHierarchy)
    //            {
    //                RotateArrow(TaskName.Complete);
    //                arrow[0].SetActive(false); // Arrow 비활성화
    //            }
    //            break;
    //        default:
    //            arrow[0].SetActive(false); // Arrow 비활성화
    //            arrow[1].SetActive(false); // Arrow 비활성화
    //            break;
    //    }
    //}

    //private void TaskStateChange(TaskName taskName)
    //{
    //    TaskManager.instance.task = taskName;
    //    OnTaskStateChanged?.Invoke(TaskManager.instance.task);
    //}

    //void RotateArrow(TaskName task)
    //{
    //    switch (task)
    //    {
    //        case TaskName.Attach:
    //            arrow[1].SetActive(true);
    //            break;

    //        case TaskName.Dig:
    //            arrow[1].SetActive(true);
    //            break;

    //        case TaskName.Complete:
    //            arrow[1].SetActive(true);
    //            break;

    //        default:
    //            break;
    //    }
    //}
}
