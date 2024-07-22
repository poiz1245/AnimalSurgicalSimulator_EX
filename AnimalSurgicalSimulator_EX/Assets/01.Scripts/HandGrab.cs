using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandGrab : MonoBehaviour
{
    XRSocketInteractor socketInteractor;
    XRGrabInteractable grabinteractable;

    void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        grabinteractable = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        //grabinteractable.enabled = true;
    }

    public void OnGripAction()
    {
        print("aa");
    }
}
