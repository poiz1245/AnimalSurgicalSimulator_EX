using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Interaction.Toolkit;
using static ObjectGrabGuideControll;

public class IvCatheterHandModelControll : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject grabObject;
    [SerializeField] GameObject catheter;
    [SerializeField] GameObject handModel;
    [SerializeField] GameObject handModelMesh;
    [SerializeField] Transform catheterAttach;
    [SerializeField] Transform handAttach;
    [SerializeField] Transform catheterShotAttach;
    [SerializeField] HandVisualizer handVisualizer;
    [SerializeField] IvCatheterDegrees catheterDegrees;

    public XRSocketInteractor socketInteractor;
    public XRGrabInteractable grabInteractor;
    public bool currentTaskComplete { get; private set; } = false;
    public bool isAttach { get; private set; } = false;

    public delegate void TaskCompleted(bool taskComplete);
    public event TaskCompleted IsTaskCompleted;

    public Vector3 handEulerAngles { get; private set; }
    float rotationSpeed = 1f;
    Vector3 initialPosition;
    Quaternion initialSocketRotation;

    void Start()
    {
        IsTaskCompleted += TaskComplete;
    }

    void Update()
    {
        float distance = Vector3.Distance(indicator.transform.position, gameObject.transform.position);
        if (!currentTaskComplete && !isAttach && grabInteractor.isSelected && distance <= 0.1f)
        {
            indicator.SetActive(false);
            Attach();
        }
        else if (isAttach && grabInteractor.isSelected && distance <= 0.3f)
        {
            RotateCatheter();
            if (catheterDegrees.isBloodContact && grabInteractor.isSelected)
            {
                MoveCatheter();
            }
        }
        else if (!currentTaskComplete && isAttach && distance > 0.1f)
        {
            indicator.SetActive(true);
            Detach();
        }
    }



    public void catheterShot()
    {
        if (catheterDegrees.isCatheterShot && grabInteractor.isSelected)
        {
            catheter.transform.SetParent(null);
            Detach();
            catheterDegrees.isCatheterShot = false;
            //currentTaskComplete = true;
            //IsTaskCompleted?.Invoke(currentTaskComplete);
        }
    }
    private void Attach()
    {
        Quaternion catheterRotate = gameObject.transform.rotation;
       
        handModelMesh.SetActive(true);
        handVisualizer.SetRightHandMeshVisibility(false);
        initialPosition = gameObject.transform.position;

        socketInteractor.transform.SetParent(handModel.transform);
        socketInteractor.transform.position = catheterAttach.transform.position;
        socketInteractor.transform.rotation = handModel.transform.rotation;

        grabObject.transform.SetParent(handModel.transform);
        grabObject.transform.position = catheterAttach.transform.position;
        handAttach.transform.rotation = Quaternion.Euler(catheterRotate.eulerAngles.x, handAttach.transform.rotation.eulerAngles.y, handAttach.transform.rotation.eulerAngles.z);
        // 현재 회전값을 고정하기 위한 변수 추가
        if (catheterDegrees.isBloodContact)
        {
            // 현재 회전값을 저장
            Quaternion currentRotation = handAttach.transform.rotation;
            handAttach.transform.rotation = currentRotation; // 현재 회전값으로 고정
        }

        /*
        handAttach.transform.rotation = catheterRotate;
        handAttach.transform.SetParent(catheterShotAttach.transform);
        handAttach.transform.position = catheterShotAttach.transform.position;
        */
        isAttach = true;
    }


    void RotateCatheter()
    {
        if (!catheterDegrees.isBloodContact)
        {
            handEulerAngles = gameObject.transform.eulerAngles;
            Quaternion targetRotation = Quaternion.Euler(handEulerAngles.x, handAttach.transform.rotation.eulerAngles.y, handAttach.transform.rotation.eulerAngles.z);
            handAttach.transform.rotation = Quaternion.Slerp(handAttach.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        /*
        Vector3 handEulerAngles = gameObject.transform.eulerAngles;
        Quaternion targetRotation = Quaternion.Euler(handEulerAngles);
        handAttach.transform.rotation = Quaternion.Slerp(handAttach.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        */
    }

    void MoveCatheter()
    {
        Vector3 currentHandPosition = gameObject.transform.position;

        Vector3 positionDifference = currentHandPosition - initialPosition;

        float zMovement = positionDifference.z;

        Vector3 newPosition = handAttach.transform.position;
        newPosition.z += zMovement;
        handAttach.transform.position = newPosition;
        initialPosition = currentHandPosition;
    }

    private void Detach()
    {
        handModelMesh.SetActive(false);
        handVisualizer.SetRightHandMeshVisibility(true);
        socketInteractor.transform.SetParent(gameObject.transform);
        socketInteractor.transform.position = gameObject.transform.position;
        socketInteractor.transform.rotation = gameObject.transform.rotation;

        grabObject.transform.SetParent(null);
        grabObject.transform.position = socketInteractor.transform.position;

        isAttach = false;

    }
    void TaskComplete(bool taskComplete)
    {
        //TaskManager.instance.scrubComplete.TaskComplete();
    }
}
