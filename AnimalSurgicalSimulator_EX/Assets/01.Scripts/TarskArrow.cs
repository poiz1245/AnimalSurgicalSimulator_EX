//using System.Collections.Generic;
//using UnityEngine;
//using static DigTask;
//using static TaskManager;

//public class TaskArrow : MonoBehaviour
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
//    }

//    void Update()
//    {
//        for (int i = 0; i < targets.Count; i++)
//        {
//            if (targets[i] != null && targets[i].gameObject.activeInHierarchy)
//            {
//                Vector3 cameraPosition = mainCamera.transform.position;
//                Vector3 objectPosition = targets[i].position;

//                // 방향 벡터 계산
//                Vector3 directionToTarget = objectPosition - cameraPosition;
//                float angle = Vector3.SignedAngle(mainCamera.transform.forward, directionToTarget, Vector3.up);

//                // 시야각 확인
//                if (Mathf.Abs(angle) <= fieldOfView / 2)
//                {
//                    // 시야각 안에 있을 때의 처리 (예: 작업 상태 가져오기)
//                    TaskName currentTask = TaskManager.instance.task;
//                    // TargetView(i, currentTask);
//                    arrow[0].SetActive(false);
//                    arrow[1].SetActive(false);
//                }
//                else
//                {
//                    // 시야각에 들어오지 않는 경우 방향에 따라 화살표 활성화
//                    if (angle < 0)
//                    {
//                        // 왼쪽에 위치
//                        arrow[0].SetActive(true);
//                        arrow[1].SetActive(false);
//                    }
//                    else
//                    {
//                        // 오른쪽에 위치
//                        arrow[0].SetActive(false);
//                        arrow[1].SetActive(true);
//                    }
//                }
//            }
//            else
//            {
//                // 타겟이 비활성화 되어 있으면 화살표 비활성화
//                arrow[0].SetActive(false);
//                arrow[1].SetActive(false);
//            }
//        }
//    }
//}
using System.Collections.Generic;
using UnityEngine;

public class TaskArrow : MonoBehaviour
{
    private static TaskArrow instance; // 싱글톤 인스턴스
    private Camera mainCamera;
    [SerializeField] float fieldOfView; // 바라보는 각도 허용 범위
    [SerializeField] GameObject[] arrow; // 비활성화할 Arrow 오브젝트
    private List<Transform> targets; // 타겟 리스트
    public static TaskArrow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TaskArrow>();
                if (instance == null)
                {
                    Debug.LogError("TaskArrow instance not found in the scene.");
                }
            }
            return instance;
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (targets == null || targets.Count == 0) return;

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null && targets[i].gameObject.activeInHierarchy)
            {
                Vector3 cameraPosition = mainCamera.transform.position;
                Vector3 objectPosition = targets[i].position;

                // 방향 벡터 계산
                Vector3 directionToTarget = objectPosition - cameraPosition;
                float angle = Vector3.SignedAngle(mainCamera.transform.forward, directionToTarget, Vector3.up);

                // 시야각 확인
                if (Mathf.Abs(angle) <= fieldOfView / 2)
                {
                    // 시야각 안에 있을 때의 처리
                    arrow[0].SetActive(false);
                    arrow[1].SetActive(false);
                }
                else
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
            else
            {
                // 타겟이 비활성화 되어 있으면 화살표 비활성화
                arrow[0].SetActive(false);
                arrow[1].SetActive(false);
            }
        }
    }

    public void SetTargets(List<Transform> newTargets)
    {
        targets = newTargets;
    }
}
