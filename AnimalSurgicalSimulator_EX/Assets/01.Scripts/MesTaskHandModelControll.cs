using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Interaction.Toolkit;
using static TaskManager;

public class MesTaskHandModelControll : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject handModel;
    [SerializeField] GameObject grabObject;
    //[SerializeField] Transform grabObjectAttach;

    [SerializeField] Transform mesAttach;

    //[SerializeField] Transform indicatorAttach;

    [SerializeField] XRSocketInteractor socketInteractor;
    [SerializeField] XRGrabInteractable grabInteractor;

    [SerializeField] HandVisualizer handVisualizer;

    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] float moveSpeed;

    float startCartPositionX;
    float startCartPositionY;

    public bool isAttach { get; private set; } = false;

    public bool currentTaskComplete { get; private set; } = false;

    public delegate void TaskCompleted(bool taskComplete);
    public event TaskCompleted IsTaskCompleted;


    private void Start()
    {
        IsTaskCompleted += TaskComplete;
    }
    private void Update()
    {
        float distance = Vector3.Distance(indicator.transform.position, gameObject.transform.position);

        if(TaskManager.instance.currentMainTask == MainTask.Mes)
        {
            if (!currentTaskComplete && !isAttach && grabInteractor.isSelected && distance <= 0.2f)
            {
                indicator.SetActive(false);
                Attach();
            }
            else if (isAttach && grabInteractor.isSelected && distance <= 0.2f)
            {
                Move();
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
    }
    private void Attach()
    {
        handVisualizer.drawMeshes = false;
        handModel.SetActive(true);

        socketInteractor.transform.SetParent(handModel.transform);
        socketInteractor.transform.position = mesAttach.transform.position;
        socketInteractor.transform.rotation = mesAttach.transform.rotation;

        grabObject.transform.SetParent(handModel.transform);

        dollyCart.m_Position = 0;

        startCartPositionX = transform.position.x;
        startCartPositionY = transform.position.y;

        isAttach = true;
    }

    private void Detach()
    {
        handVisualizer.drawMeshes = true;

        socketInteractor.transform.SetParent(gameObject.transform);
        socketInteractor.transform.position = gameObject.transform.position;
        socketInteractor.transform.rotation = gameObject.transform.rotation;

        grabObject.transform.SetParent(null);
        grabObject.transform.position = socketInteractor.transform.position;

        handModel.SetActive(false);
        isAttach = false;
    }

    void Move()
    {
        float movePositionX = gameObject.transform.position.x - startCartPositionX;
        float movePositionY = startCartPositionY - gameObject.transform.position.y;

        dollyCart.m_Position = (movePositionX + movePositionY) * moveSpeed;

        if (dollyCart.m_Position >= 1)
        {
            currentTaskComplete = true;
            IsTaskCompleted?.Invoke(currentTaskComplete);
            Detach();
        }
    }

    void TaskComplete(bool taskComplete)
    {
        TaskManager.instance.mesComplete.TaskComplete();
    }
}
