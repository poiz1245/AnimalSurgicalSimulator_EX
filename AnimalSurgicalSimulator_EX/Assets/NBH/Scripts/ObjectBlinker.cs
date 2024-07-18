using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectBlinker : MonoBehaviour
{
    [SerializeField] XRBaseInteractor socketInteractor; // 소켓 인터렉터 참조
    [SerializeField] float blinkInterval = 0.5f; // 깜빡이는 간격
    [SerializeField] List<XRGrabInteractable> grabInteractors; // XRGrabInteractable 리스트
    [SerializeField] List<GameObject> objectsToBlink; // 깜빡이게 할 오브젝트 리스트
    [SerializeField] HandModelControll handModelControll; // HandModelControll 인스턴스 참조

    bool isObjectInSocket = false;
    bool isGrabbed = false;
    int grabbedIndex = -1;

    void Start()
    {
        // 모든 오브젝트를 비활성화
        foreach (var obj in objectsToBlink)
        {
            obj.SetActive(false);
        }

        // 각 grabInteractor에 대해 SelectEntered 이벤트를 등록
        for (int i = 0; i < grabInteractors.Count; i++)
        {
            int index = i; // 캡처된 인덱스
            grabInteractors[i].selectEntered.AddListener((args) => OnGrabbed(index));
            grabInteractors[i].selectExited.AddListener((args) => OnReleased(index));
        }

        // HandModelControll의 TaskCompleted 이벤트 구독
        handModelControll.IsTaskCompleted += OnTaskCompleted;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            if (grabbedIndex >= 0 && grabbedIndex < objectsToBlink.Count)
            {
                objectsToBlink[grabbedIndex].SetActive(false);
            }
            return;
        }
    }

    private IEnumerator BlinkObject(int index)
    {
        Debug.Log("블링크");

        while (!isObjectInSocket)
        {
            objectsToBlink[index].SetActive(!objectsToBlink[index].activeSelf);
            yield return new WaitForSeconds(blinkInterval);
            isObjectInSocket = socketInteractor.hasSelection;
        }

        // 오브젝트가 소켓에 들어가면 깜빡이기 멈춤
        objectsToBlink[index].SetActive(false);
    }

    private void OnGrabbed(int index)
    {
        isGrabbed = true;
        grabbedIndex = index;
    }

    private void OnReleased(int index)
    {
        isGrabbed = false;
        grabbedIndex = -1;
    }

    // TaskCompleted 이벤트가 발생할 때 실행할 메서드
    private void OnTaskCompleted(bool taskComplete)
    {
        if (taskComplete)
        {
            Debug.Log("Task is completed.");
            // 여기에서 필요한 작업을 수행합니다.
            if (grabbedIndex >= 0 && grabbedIndex < objectsToBlink.Count)
            {
                StartCoroutine(BlinkObject(grabbedIndex));
            }
        }
    }

    //// 소켓 인덱스에 따라 포지션 값을 설정하는 메서드
    //public void SetSocketPosition(int index)
    //{
    //    if (index >= 0 && index < socketIndexes.Count)
    //    {
    //        socketInteractor.transform.position = socketIndexes[index].transform.position;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("잘못된 인덱스입니다.");
    //    }
    //}
}
