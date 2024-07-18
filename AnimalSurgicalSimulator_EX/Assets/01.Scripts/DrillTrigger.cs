using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DrillTrigger : MonoBehaviour
{

    [SerializeField] InputActionReference triggerButton;
    [SerializeField] XRBaseController rightController;
    [SerializeField] GameObject spinObject;
    [SerializeField] XRGrabInteractable interactable;

    LayerMask currentTriggerMask;
    bool isTrigger = false;
    public string currentTriggerLayerName { get; private set; }
    public bool buttonOn { get; private set; } = false;
    void Start()
    {
        triggerButton.action.started += TriggerButtonOn;
        triggerButton.action.started -= TriggerButtonOff;

        triggerButton.action.canceled += TriggerButtonOff;
        triggerButton.action.canceled -= TriggerButtonOn;
    }

    private void Update()
    {
        Vibration();
    }

    private void Vibration()
    {
        if (!isTrigger)
        {
            currentTriggerLayerName = "null";
        }

        if (buttonOn && interactable.isSelected)
        {
            switch (currentTriggerLayerName)
            {
                case "OutsideBone":
                    OnVibration(1f);
                    break;
                case "InsideBone":
                    OnVibration(0.5f);
                    break;
                case "null":
                    OnVibration(0.2f);
                    break;
            }
        }
    }

    void TriggerButtonOn(InputAction.CallbackContext context)
    {
        buttonOn = true;
        spinObject.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.01f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
    void TriggerButtonOff(InputAction.CallbackContext context)
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
