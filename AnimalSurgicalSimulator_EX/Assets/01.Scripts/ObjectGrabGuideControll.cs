using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabGuideControll : MonoBehaviour
{
    [SerializeField] GameObject guideMesh;

    public enum GripObject
    {
        Mes,
        Clamp,
        Dig
    }

    public GripObject gripObjectName;

    void Update()
    {
        // 현재 메인 태스크와 비교하여 가이드를 활성화 또는 비활성화
        if (TaskManager.instance.task == TaskManager.TaskName.Start && TaskManager.instance.currentMainTask == (TaskManager.MainTask)gripObjectName)
        {
            guideMesh.SetActive(true);
        }
        else
        {
            guideMesh.SetActive(false);
        }
    }
}
