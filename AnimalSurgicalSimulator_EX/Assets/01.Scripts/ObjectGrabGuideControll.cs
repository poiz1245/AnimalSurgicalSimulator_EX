using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectGrabGuideControll : MonoBehaviour
{
    //[SerializeField] GameObject mesMesh;
    //[SerializeField] GameObject clampMesh;
    //[SerializeField] GameObject drillMesh;
    //[SerializeField] List<GripGuideOnOffTemp> grabObject;

    //public enum GripObject
    //{
    //    Mes,
    //    Clamp,
    //    Dig
    //}

    //public GripObject gripObjectName;

    //void Start()
    //{
    //    UpdateMeshes(TaskManager.instance.currentMainTask);
    //    TaskManager.instance.OnMainTaskChanged += UpdateMeshes; // 이벤트 구독
    //}

    //void OnDestroy()
    //{
    //    TaskManager.instance.OnMainTaskChanged -= UpdateMeshes; // 이벤트 구독 해제
    //}

    //public void UpdateMeshes(TaskManager.MainTask currentMainTask)
    //{ 
    //    // 모든 메쉬 비활성화
    //    mesMesh.SetActive(false);
    //    clampMesh.SetActive(false);
    //    drillMesh.SetActive(false);

    //    // 현재 메인 태스크에 따라 메쉬 활성화
    //    switch (currentMainTask)
    //    {
    //        case TaskManager.MainTask.Mes:
    //            mesMesh.SetActive(true);
    //            break;
    //        case TaskManager.MainTask.Clamp:
    //            if (TaskManager.instance.task == TaskManager.TaskName.Start)
    //            {
    //                clampMesh.SetActive(true);
    //            }
    //            break;
    //        case TaskManager.MainTask.Dig:
    //            if (TaskManager.instance.task == TaskManager.TaskName.Start)
    //            {
    //                drillMesh.SetActive(true);
    //            }
    //            break;
    //    }
    //}


    [SerializeField] GameObject mesMesh;
    [SerializeField] GameObject clampMesh;
    [SerializeField] GameObject drillMesh;
    [SerializeField] List<GripGuideOnOffTemp> grabObject; // grabObject 리스트를 이용하여 각 객체를 관리

    public enum GripObject
    {
        Mes,
        Clamp,
        Dig
    }

    public GripObject gripObjectName;

    private void Start()
    {
        UpdateMeshes(TaskManager.instance.currentMainTask);
        TaskManager.instance.OnMainTaskChanged += UpdateMeshes; // 이벤트 구독

        foreach (var obj in grabObject)
        {
            XRGrabInteractable grabInteractable = obj.GetComponentInParent<XRGrabInteractable>();
            grabInteractable.selectEntered.AddListener((interactor) => obj.GrabObject());
            grabInteractable.selectExited.AddListener((interactor) => obj.ReleaseObject());
        }
    }

    private void OnDestroy()
    {
        TaskManager.instance.OnMainTaskChanged -= UpdateMeshes; // 이벤트 구독 해제
    }

    public void UpdateMeshes(TaskManager.MainTask currentMainTask)
    {
        // 모든 메쉬 비활성화
        mesMesh.SetActive(false);
        clampMesh.SetActive(false);
        drillMesh.SetActive(false);

        // 현재 메인 태스크에 따라 메쉬 활성화
        switch (currentMainTask)
        {
            case TaskManager.MainTask.Mes:
                mesMesh.SetActive(true);
                grabObject[0].enabled = true;
                grabObject[1].enabled = false;
                grabObject[2].enabled = false;
                break;
            case TaskManager.MainTask.Clamp:
                clampMesh.SetActive(true);
                grabObject[0].enabled = false;
                grabObject[1].enabled = true;
                grabObject[2].enabled = false;
                break;
            case TaskManager.MainTask.Dig:
                drillMesh.SetActive(true);
                grabObject[0].enabled = false;
                grabObject[1].enabled = false;
                grabObject[2].enabled = true;
                break;
        }
    }
}
