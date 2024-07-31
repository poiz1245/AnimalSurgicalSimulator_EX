using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Interaction.Toolkit;

public class ClampTaskHandModelControll : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject handModel;
    [SerializeField] GameObject grabObject;

    [SerializeField] Transform mesAttach;

    [SerializeField] XRSocketInteractor socketInteractor;
    [SerializeField] XRGrabInteractable grabInteractor;
    [SerializeField] HandVisualizer handVisualizer;
    [SerializeField] ClampTrigger clampTrigger;

    float startCartPosition;
    public bool isAttach { get; private set; } = false;

    public bool currentTaskComplete { get; private set; } = false;

    public delegate void TaskCompleted(bool taskComplete);
    public event TaskCompleted IsTaskCompleted;


    private void Start()
    {
        //IsTaskCompleted += TaskComplete;
    }
    private void Update()
    {
        float distance = Vector3.Distance(indicator.transform.position, gameObject.transform.position);

        if (!currentTaskComplete && !isAttach && grabInteractor.isSelected && distance <= 0.2f)
        {
            indicator.SetActive(false);
            Attach();
        }
        else if (isAttach && grabInteractor.isSelected && distance <= 0.2f)
        {
            if (!clampTrigger.clampOpen)
            {
                Move();
            }
        }
        else if (isAttach && !grabInteractor.isSelected && distance <= 0.2f)
        {
            indicator.SetActive(true);
            Detach();
        }
        else if (!currentTaskComplete && isAttach && distance > 0.2f)
        {
            indicator.SetActive(true);
            Detach();
        }
    }
    private void Attach()
    {
        handVisualizer.drawMeshes = false;
        handModel.SetActive(true);

        socketInteractor.transform.SetParent(handModel.transform);
        socketInteractor.transform.position = mesAttach.transform.position;
        socketInteractor.transform.rotation = mesAttach.transform.rotation;

        grabObject.transform.SetParent(handModel.transform);

        startCartPosition = transform.position.x;

        isAttach = true;
    }

    private void Detach()
    {
        handVisualizer.drawMeshes = true;

        socketInteractor.transform.SetParent(gameObject.transform);
        socketInteractor.transform.position = gameObject.transform.position;
        socketInteractor.transform.rotation = gameObject.transform.rotation;

        grabObject.transform.SetParent(null);

        handModel.SetActive(false);
        isAttach = false;
    }

    void Move()
    {
        grabInteractor.enabled = false;
        currentTaskComplete = true;
        IsTaskCompleted?.Invoke(currentTaskComplete);

        Detach();
    }

    void TaskComplete(bool taskComplete)
    {
        TaskManager.instance.clampComplete.TaskComplete();
    }
}
