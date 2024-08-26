using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TaskManager;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Interaction.Toolkit;
using static ObjectGrabGuideControll;

public class ScrubTaskHandModelControll : MonoBehaviour
{

    [SerializeField] GameObject indicator;
    [SerializeField] GameObject handModel;
    [SerializeField] GameObject leftHandModel;
    [SerializeField] GameObject grabObject;
    //[SerializeField] Transform grabObjectAttach;

    [SerializeField] Transform scrubAttach;
    //[SerializeField] Transform fingerWashAttach; // 기존에 Scrub 인디케이터를 이 위치로 옮길 예정

    //[SerializeField] Transform indicatorAttach;

    [SerializeField] XRSocketInteractor socketInteractor;
    [SerializeField] XRGrabInteractable grabInteractor;

    [SerializeField] HandVisualizer handVisualizer;

    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] float moveSpeed;

    float startCartPositionX;
    float startCartPositionY;
    float startCartPositionZ;

    int scrubhand = 0;

    bool isNextWash = false; //다음 손씻는거로 넘어가기 위한 bool형 변수
    public bool isAttach { get; private set; } = false;
    public bool currentTaskComplete { get; private set; } = false;

    //public delegate void TaskCompleted(bool taskComplete);
    //public event TaskCompleted IsTaskCompleted;


    private void Start()
    {
        //IsTaskCompleted += TaskComplete;
    }
    private void Update()
    {
        float distance = Vector3.Distance(indicator.transform.position, gameObject.transform.position);

/*
        if (TaskManager.instance.currentMainTask == MainTask.scrub)
        {

        }
*/
        if (!currentTaskComplete && !isAttach && grabInteractor.isSelected && distance <= 0.1f)
        {
            indicator.SetActive(false);
            Attach();
        }
        else if (isAttach && grabInteractor.isSelected && distance <= 0.4f)
        {
            ScrubMove();
        }
       /* else if (isAttach && !grabInteractor.isSelected && distance <= 0.1f)
        {
            indicator.SetActive(true);
            Detach();
        }*/
        else if (!currentTaskComplete && isAttach && distance > 0.4f)
        {
            indicator.SetActive(true);
            Detach();
        }
    }
    private void Attach()
    {
        handVisualizer.drawMeshes = false;
        handModel.SetActive(true);
        leftHandModel.SetActive(true);

        socketInteractor.transform.SetParent(handModel.transform);
        socketInteractor.transform.position = scrubAttach.transform.position;
        socketInteractor.transform.rotation = scrubAttach.transform.rotation;

        grabObject.transform.SetParent(handModel.transform);

        dollyCart.m_Position = 0;

        startCartPositionX = transform.position.x;
        startCartPositionY = transform.position.y;
        startCartPositionZ = transform.position.z;

        isAttach = true;

    }

    private void Detach()
    {
        grabObject.SetActive(true);
        handVisualizer.drawMeshes = true;

        socketInteractor.transform.SetParent(gameObject.transform);
        socketInteractor.transform.position = gameObject.transform.position;
        socketInteractor.transform.rotation = gameObject.transform.rotation;

        grabObject.transform.SetParent(null);
        grabObject.transform.position = socketInteractor.transform.position;

        handModel.SetActive(false);
        leftHandModel.SetActive(false);
        isAttach = false;

    }

    void ScrubMove()
    {

        float movePositionX = startCartPositionX - gameObject.transform.position.x ;
        float movePositionY = startCartPositionY - gameObject.transform.position.y ;
        float movePositionZ = startCartPositionZ - gameObject.transform.position.z;
        float caetPosition = dollyCart.m_Position;

        dollyCart.m_Position = (movePositionX + movePositionY + movePositionZ) * moveSpeed;

        if (dollyCart.m_Position >= 1 && caetPosition < 1)
        {
            scrubhand++;
            if (scrubhand == 30)
            {
                Debug.Log("Scurb 완료");

                //currentTaskComplete = true;
                //IsTaskCompleted?.Invoke(currentTaskComplete);
                Detach();
            }
        }
    }
    void WashMove()
    {

        float movePositionX = startCartPositionX - gameObject.transform.position.x;
        float movePositionY = startCartPositionY - gameObject.transform.position.y;
        float movePositionZ = startCartPositionZ - gameObject.transform.position.z;
        float caetPosition = dollyCart.m_Position;

        dollyCart.m_Position = (movePositionX + movePositionY + movePositionZ) * moveSpeed;

        if (dollyCart.m_Position >= 1 && caetPosition < 1)
        {
            scrubhand++;
            if (scrubhand == 30)
            {
                Debug.Log("Scurb 완료");

                //currentTaskComplete = true;
                //IsTaskCompleted?.Invoke(currentTaskComplete);
                Detach();
            }
        }
    }
    void TaskComplete(bool taskComplete)
    {
        //TaskManager.instance.scrubComplete.TaskComplete();
    }
}
