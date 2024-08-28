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
    [SerializeField] ScrubTaskHandModelControll scrubHandModelController;
    [SerializeField] IvCartheterHandModelControll cartheterHandModelController;
    [SerializeField] XRSocketInteractor handSocketInteractor;

    public enum ObjectName
    {
        Mes,
        Clamp,
        Dig,
        WashHand,
        IvCartheter
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
                gameObject.SetActive(true);
            }
        }
        else if(objectName == ObjectName.WashHand)
        {
            if (!controller.currentTaskComplete && !scrubHandModelController.currentTaskComplete)
            {
                gameObject.SetActive(true);
            }
        }
        else if (objectName == ObjectName.IvCartheter)
        {
            if (!controller.currentTaskComplete && !cartheterHandModelController.currentTaskComplete)
            {
                gameObject.SetActive(true);
            }
        }
    }

    public void OffIndicate()
    {
        gameObject.SetActive(false);
    }
}
