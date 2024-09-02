using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Interaction.Toolkit;

public class IvCatheterHandModelControll : MonoBehaviour
{
    /*[SerializeField] GameObject bloodVessel;

    [SerializeField] Transform catheter;
    [SerializeField] Transform catheterStartPos;
    [SerializeField] Transform catheterEndPos;

    [SerializeField] Transform skinStartPos;
    [SerializeField] Transform skinEndPos;*/


    public XRSocketInteractor socketInteractor;
    public XRGrabInteractable grabInteractor;

    public bool currentTaskComplete { get; private set; } = false;
    public bool isGrab { get; private set; } = false;

    public delegate void TaskCompleted(bool taskComplete);
    public event TaskCompleted IsTaskCompleted;


    private void Start()
    {
        IsTaskCompleted += TaskComplete;
    }
    private void Update()
    {
        if (grabInteractor.isSelected)
        {
            print("주사기 그랩 상태 " + isGrab);
            isGrab = true;
        }
        else
        {
            print("주사기 그랩 상태 " + isGrab);
            isGrab = false;
        }
        
    }

    void TaskComplete(bool taskComplete)
    {
        //TaskManager.instance.scrubComplete.TaskComplete();
    }
}
