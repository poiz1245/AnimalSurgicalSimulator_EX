using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Indicate : MonoBehaviour
{
    [SerializeField] HandModelControll controller;
    [SerializeField] DrillTaskHandModelControll drillHandModelController;
    [SerializeField] MesTaskHandModelControll mesHandModelController;
    [SerializeField] ClampTaskHandModelControll clampHandModelController;
    [SerializeField] XRSocketInteractor handSocketInteractor;

    public enum ObjectName
    {
        Mes,
        Clamp,
        Dig
    }

    public ObjectName objectName;
    public void OnIndicate()
    {
        if (objectName == ObjectName.Mes)
        {
            if (!controller.currentTaskComplete && !mesHandModelController.currentTaskComplete)
            {
                gameObject.SetActive(true);
            }
        }
        else if (objectName == ObjectName.Clamp)
        {
            if (!controller.currentTaskComplete && !clampHandModelController.currentTaskComplete)
            {
                gameObject.SetActive(true);
            }
        }
        else if (objectName == ObjectName.Dig)
        {
            if (!controller.currentTaskComplete && !drillHandModelController.currentTaskComplete)
            {
                Debug.Log("드릴 활성화");
                gameObject.SetActive(true);
            }
        }
    }

    public void OffIndicate()
    {
        gameObject.SetActive(false);
        if (objectName == ObjectName.Dig)
        {
            Debug.Log("DIG");
        }
    }
}
