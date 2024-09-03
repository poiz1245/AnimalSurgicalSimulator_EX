using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Interaction.Toolkit;

public class IvCatheterHandModelControll : MonoBehaviour
{
    [SerializeField] GameObject catheter;
    [SerializeField] IvCatheterDegrees catheterSucess;

    public XRSocketInteractor socketInteractor;
    public XRGrabInteractable grabInteractor;

    public bool currentTaskComplete { get; private set; } = false;
    public bool isGrab { get; private set; } = false;

    public delegate void TaskCompleted(bool taskComplete);
    public event TaskCompleted IsTaskCompleted;


    void Start()
    {
        IsTaskCompleted += TaskComplete;
    }
    public void catheterShot()
    {
        if (catheterSucess.isCatheterShot && grabInteractor.isSelected)
        {
            print("Shot");
            catheter.transform.SetParent(null);
        }
    }


    void TaskComplete(bool taskComplete)
    {
        //TaskManager.instance.scrubComplete.TaskComplete();
    }
}
