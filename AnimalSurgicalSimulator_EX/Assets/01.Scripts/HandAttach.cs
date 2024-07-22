using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAttach : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject handModel;
    [SerializeField] GameObject grabObject;

    [SerializeField] Transform indicatorAttach;
    [SerializeField] Transform drillAttach;

    [SerializeField] XRSocketInteractor socketInteractor;
    [SerializeField] DrillTrigger drillTrigger;

    [SerializeField] HandVisualizer handVisualizer;

    float drillSpeed = 0.03f;
    bool isAttach = false;

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


        if (!currentTaskComplete && !isAttach && socketInteractor.enabled && distance <= 0.2f)
        {
            indicator.SetActive(false);
            Attach();
        }
        else if (isAttach && socketInteractor.enabled && distance <= 0.2f)
        {
            handModel.transform.rotation = Quaternion.Euler(new Vector3(90, -90, 0));
            Move();
        }
        else if (!currentTaskComplete && isAttach && distance > 0.2f)
        {
            indicator.SetActive(true);
            Detach();
        }
    }
    private void Attach()
    {
        socketInteractor.enabled = false;
        handVisualizer.drawMeshes = false;

        handModel.SetActive(true);
        grabObject.transform.SetParent(handModel.transform);
        handModel.transform.position = indicatorAttach.position;

        grabObject.transform.position = drillAttach.transform.position;
        grabObject.transform.rotation = drillAttach.transform.rotation;

        

        isAttach = true;
    }

    private void Detach()
    {
        socketInteractor.socketActive = true;
        handVisualizer.drawMeshes = true;

        handModel.SetActive(false);
        grabObject.transform.SetParent(null) ;

        isAttach = false;
    }

    void Move()
    {
        if (drillTrigger.buttonOn) //자동으로 움직이는 기능
        {
            if (drillTrigger.currentTriggerLayerName == "OutsideBone")
            {
                drillSpeed = 0.005f;
            }
            else if (drillTrigger.currentTriggerLayerName == "InsideBone")
            {
                drillSpeed = 0.02f;
            }
            else if (drillTrigger.currentTriggerLayerName == "EndLayer")
            {
                currentTaskComplete = true;
                IsTaskCompleted?.Invoke(currentTaskComplete);
                Detach();
            }
            handModel.transform.Translate(0, 0, drillSpeed * Time.deltaTime);
        }

        /*if (drillTrigger.buttonOn) // 직접 움직이는 기능
        {
            handModel.transform.position = new Vector3(indicatorAttach.position.x, gameObject.transform.position.y, indicatorAttach.position.z);

            if (drillTrigger.currentTriggerLayerName == "EndLayer")
            {
                currentTaskComplete = true;
                IsTaskCompleted?.Invoke(currentTaskComplete);
                Detach();
            }
        }*/
    }

    void TaskComplete(bool taskComplete)
    {
        TaskManager.instance.digComplete.TaskComplete();
    }
}
