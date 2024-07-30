using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Interaction.Toolkit;

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
    //float drillSpeed = 0.03f;
    public bool isAttach { get; private set; } = false;

    public bool currentTaskComplete { get; private set; } = false;

    /*public delegate void TaskCompleted(bool taskComplete);
    public event TaskCompleted IsTaskCompleted;*/


    /*private void Start()
    {
        IsTaskCompleted += TaskComplete;
    }*/
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
    private void Attach()
    {
        handVisualizer.drawMeshes = false;
        handModel.SetActive(true);

        socketInteractor.transform.SetParent(handModel.transform);
        socketInteractor.transform.position = mesAttach.transform.position;
        socketInteractor.transform.rotation = mesAttach.transform.rotation;

        grabObject.transform.SetParent(handModel.transform);

        isAttach = true;
    }

    private void Detach()
    {
        handVisualizer.drawMeshes = true;
        handModel.SetActive(false);

        socketInteractor.transform.SetParent(gameObject.transform);
        socketInteractor.transform.position = gameObject.transform.position;
        socketInteractor.transform.rotation = gameObject.transform.rotation;

        grabObject.transform.SetParent(null);
        grabObject.transform.position = socketInteractor.transform.position;
        isAttach = false;
    }

    void Move()
    {
        //handModel.transform.position = new Vector3(gameObject.transform.position.x, handModel.transform.position.y, handModel.transform.position.z);
        dollyCart.m_Position = gameObject.transform.position.x;
    }

    void TaskComplete(bool taskComplete)
    {
        TaskManager.instance.digComplete.TaskComplete();
    }

    private void OnTriggerEnter(Collider other)
    {
        HapticsTest.instance.CustomBasic(0.02f, 0.1f);
    }
}
