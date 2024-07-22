using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandTrackingDrillTrigger : MonoBehaviour
{
    [SerializeField] XRBaseController rightController;
    [SerializeField] GameObject spinObject;
    [SerializeField] XRSocketInteractor interactable;

    LayerMask currentTriggerMask;
    bool isTrigger = false;
    public string currentTriggerLayerName { get; private set; }
    public bool buttonOn { get; private set; } = false;

    private void Update()
    {
        Vibration();
    }

    public void Vibration()
    {
        if (!isTrigger)
        {
            currentTriggerLayerName = "null";
        }

        if (buttonOn && interactable.enabled)
        {
            spinObject.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.01f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);

            if (currentTriggerLayerName == "OutsideBone")
            {
                OnVibration(1f);
            }
            else if (currentTriggerLayerName == "InsideBone")
            {
                OnVibration(0.5f);
            }
            else
            {
                OnVibration(0.2f);
            }
        }

    }

    public void TriggerButtonOn()
    {
        buttonOn = true;
    }
    public void TriggerButtonOff()
    {
        buttonOn = false;
        spinObject.transform.DOKill();
        OnVibration(0);
    }
    void OnVibration(float amplitude)
    {
        rightController.SendHapticImpulse(amplitude, 0.1f);

    }
    private void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
        currentTriggerMask = 1 << other.gameObject.layer;
        currentTriggerLayerName = LayerMask.LayerToName((int)Mathf.Log(currentTriggerMask, 2));
    }
    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
    }
}
