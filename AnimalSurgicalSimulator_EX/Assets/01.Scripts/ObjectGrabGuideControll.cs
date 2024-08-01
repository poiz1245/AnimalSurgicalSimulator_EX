using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectGrabGuideControll : MonoBehaviour
{
    //[SerializeField] List<GameObject> handMesh; // 다양한 핸드 메쉬를 저장하는 리스트
    //int currentIndex;

    //public enum GripObject
    //{
    //    Mes,
    //    Clamp,
    //    Dig
    //}

    //private void Start()
    //{
    //    // 현재 메인 태스크를 기반으로 메쉬 업데이트
    //    UpdateMeshes(TaskManager.instance.currentMainTask);
    //    // 메인 태스크가 변경될 때 이벤트를 구독
    //    TaskManager.instance.OnMainTaskChanged += UpdateMeshes;

    //    for (int i = 0; i < handMesh.Count; i++)
    //    {
    //        int index = i;
    //        // 각 핸드 메쉬의 상위 객체에서 XRGrabInteractable 컴포넌트를 가져옴
    //        XRGrabInteractable grabInteractable = handMesh[i].GetComponentInParent<XRGrabInteractable>();
    //        // 객체가 선택되었을 때 이벤트를 구독
    //        grabInteractable.selectEntered.AddListener((interactor) => GrabObject());
    //        // 객체 선택이 해제되었을 때 이벤트를 구독
    //        grabInteractable.selectExited.AddListener((interactor) => ReleaseObject());
    //    }
    //}

    //private void OnDestroy()
    //{
    //    // 메인 태스크 변경 이벤트 구독 해제
    //    TaskManager.instance.OnMainTaskChanged -= UpdateMeshes;
    //}

    //// 현재 메인 태스크에 따라 핸드 메쉬 업데이트
    //public void UpdateMeshes(TaskManager.MainTask currentMainTask)
    //{
    //    // 모든 핸드 메쉬 비활성화
    //    for (int i = 0; i < handMesh.Count; i++)
    //    {
    //        handMesh[i].SetActive(false);
    //    }

    //    // 현재 메인 태스크에 맞는 핸드 메쉬만 활성화
    //    switch (currentMainTask)
    //    {
    //        case TaskManager.MainTask.Mes:
    //            handMesh[0].SetActive(true);
    //            currentIndex = 0;
    //            break;
    //        case TaskManager.MainTask.Clamp:
    //            handMesh[1].SetActive(true);
    //            currentIndex = 1;
    //            break;
    //        case TaskManager.MainTask.Dig:
    //            handMesh[2].SetActive(true);
    //            currentIndex = 2;
    //            break;
    //    }
    //}

    //// 특정 메쉬가 그랩될 때 처리
    //public void GrabObject()
    //{
    //    for (int i = 0; i < handMesh.Count; i++) 
    //    { 
    //        handMesh[i].SetActive(false);
    //    }
    //}

    //// 특정 메쉬의 그랩이 해제될 때 처리
    //void ReleaseObject()
    //{
    //    for (int i = 0; i < handMesh.Count; i++)
    //    {
    //        if(i == currentIndex)
    //        {
    //            handMesh[i].SetActive(true);
    //        }


    //    }
    //}

    [SerializeField] List<GameObject> handMesh; // 다양한 핸드 메쉬를 저장하는 리스트
    int currentIndex;

    public enum GripObject
    {
        Mes,
        Clamp,
        Dig
    }

    private void Start()
    {
        // 현재 메인 태스크를 기반으로 메쉬 업데이트
        UpdateMeshes(TaskManager.instance.currentMainTask);
        // 메인 태스크가 변경될 때 이벤트를 구독
        TaskManager.instance.OnMainTaskChanged += UpdateMeshes;

        for (int i = 0; i < handMesh.Count; i++)
        {
            int index = i; // 현재 인덱스를 캡처
            XRGrabInteractable grabInteractable = handMesh[index].GetComponentInParent<XRGrabInteractable>();
            grabInteractable.selectEntered.AddListener((interactor) => GrabObject(index));
            grabInteractable.selectExited.AddListener((interactor) => ReleaseObject(index));
        }
    }

    private void OnDestroy()
    {
        // 메인 태스크 변경 이벤트 구독 해제
        TaskManager.instance.OnMainTaskChanged -= UpdateMeshes;
    }

    // 현재 메인 태스크에 따라 핸드 메쉬 업데이트
    public void UpdateMeshes(TaskManager.MainTask currentMainTask)
    {
        for (int i = 0; i < handMesh.Count; i++)
        {
            handMesh[i].SetActive(false);
        }

        switch (currentMainTask)
        {
            case TaskManager.MainTask.Mes:
                currentIndex = 0;
                break;
            case TaskManager.MainTask.Clamp:
                currentIndex = 1;
                break;
            case TaskManager.MainTask.Dig:
                currentIndex = 2;
                break;
        }

        handMesh[currentIndex].SetActive(true);
    }

    // 특정 메쉬가 그랩될 때 처리
    public void GrabObject(int index)
    {
        if (index == currentIndex)
        {
            handMesh[index].SetActive(false);
        }
    }

    // 특정 메쉬의 그랩이 해제될 때 처리
    public void ReleaseObject(int index)
    {
        if (index == currentIndex)
        {
            handMesh[index].SetActive(true);
        }
    }
}