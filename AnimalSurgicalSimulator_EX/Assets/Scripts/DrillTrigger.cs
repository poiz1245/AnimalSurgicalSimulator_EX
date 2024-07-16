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
    //[SerializeField] XRController leftController;
    // Start is called before the first frame update

    LayerMask currentTriggerMask;
    string currentTriggerLayerName;
    bool isTrigger = false;
    bool buttonOn = false;
    void Start()
    {
        triggerButton.action.started += TriggerButtonOn;
        triggerButton.action.started -= TriggerButtonOff;

        triggerButton.action.canceled += TriggerButtonOff;
        triggerButton.action.canceled -= TriggerButtonOn;
    }

    private void Update()
    {
        if (!isTrigger)
        {
            currentTriggerLayerName = "null";
        }

        if (buttonOn)
        {
            switch (currentTriggerLayerName)
            {
                case "Bone1":
                    OnVibration(1f);
                    print("Bone1");
                    break;
                case "Bone2":
                    OnVibration(0.7f);
                    print("Bone2");
                    break;
                case "null":
                    OnVibration(0.3f);
                    break;
            }
        }

    }
    void TriggerButtonOn(InputAction.CallbackContext context)
    {
        buttonOn = true;
    }
    void TriggerButtonOff(InputAction.CallbackContext context)
    {
        buttonOn = false;
        OnVibration(0);
    }
    private void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
        currentTriggerMask = 1 << other.gameObject.layer;
        currentTriggerLayerName = LayerMask.LayerToName((int)Mathf.Log(currentTriggerMask, 2));
        print(currentTriggerLayerName);
    }
    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
    }
    void OnVibration(float amplitude)
    {
        rightController.SendHapticImpulse(amplitude, 0.1f);

    }
}
