using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TaskManager;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Interaction.Toolkit;
using static ObjectGrabGuideControll;
using UnityEngine.UIElements;

public class ScrubTaskHandModelControll : MonoBehaviour
{

    [SerializeField] GameObject indicator;
    [SerializeField] GameObject handModel;
    [SerializeField] GameObject[] leftHandModels; // 배열로 변경
    [SerializeField] GameObject grabObject;

    [SerializeField] Transform scrubAttach;
    [SerializeField] Transform[] fingerWashAttach; // 기존에 Scrub 인디케이터를 이 위치로 옮길 예정


    [SerializeField] XRSocketInteractor socketInteractor;
    [SerializeField] XRGrabInteractable grabInteractor;

    [SerializeField] HandVisualizer handVisualizer;

    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] CinemachinePathBase[] washPath; // 새로운 경로를 지정할 변수
    [SerializeField] float moveSpeed;

    float startCartPositionX;
    float startCartPositionY;
    float startCartPositionZ;

    int washScrub = 0;
    int washFinger = 0;

    bool isNextWash = false; //다음 손씻는거로 넘어가기 위한 bool형 변수
    bool isRightFinger = true;
    public bool isAttach { get; private set; } = false;
    public bool currentTaskComplete { get; private set; } = false;

    public delegate void TaskCompleted(bool taskComplete);
    public event TaskCompleted IsTaskCompleted;


    private void Start()
    {
        IsTaskCompleted += TaskComplete;
        foreach (var model in leftHandModels)
        {
            model.SetActive(false);
        }
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
/*            if (!isNextWash)
            {
            }
            else
            {
                WashMove();
            }
*/
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
    void SetPositionAndRotation(GameObject Before, Transform After)
    {
        Before.transform.position = After.position;
        Before.transform.rotation = After.rotation;
    }

    private void Attach()
    {
        handVisualizer.drawMeshes = false;
        handModel.SetActive(true);
        leftHandModels[isNextWash ? 1 : 0].SetActive(true);

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
        foreach(var model in leftHandModels)
        {
            model.SetActive(false); // 모든 손 모델 비활성화
        }
        isAttach = false;

    }

    void ScrubMove()
    {

        float movePositionX = startCartPositionX - gameObject.transform.position.x ;
        float movePositionY = startCartPositionY - gameObject.transform.position.y ;
        float movePositionZ = startCartPositionZ - gameObject.transform.position.z;
        float cartPosition = dollyCart.m_Position;

        dollyCart.m_Position = (movePositionX + movePositionY + movePositionZ) * moveSpeed;

        if (dollyCart.m_Position >= 1 && cartPosition < 1)
        {
            washScrub++;
            Debug.Log("washScrub" + washScrub);
            if (washScrub == 5 && !isNextWash)
            {
                isNextWash = true;
                Detach();
                SetPositionAndRotation(indicator, fingerWashAttach[0]);
                indicator.SetActive(true);
                dollyCart.ChangePath(washPath[1]);
            }
            else if (isNextWash)
            {
                washFinger++;
                Debug.Log("washFinger" + washFinger);
                if (washFinger == 2 && isRightFinger)
                {
                    Detach();
                    dollyCart.ChangePath(washPath[2]); // 경로 변경
                    SetPositionAndRotation(indicator, fingerWashAttach[1]);
                    indicator.SetActive(true);
                    washFinger = 0;
                    isRightFinger = false;
                }
                else if (washFinger == 2 && !isRightFinger)
                {
                    Detach();
                    currentTaskComplete = true;
                    IsTaskCompleted?.Invoke(currentTaskComplete);
                }
            }
                
            
        }
    }
    
    void TaskComplete(bool taskComplete)
    {
        //TaskManager.instance.scrubComplete.TaskComplete();
    }
}
