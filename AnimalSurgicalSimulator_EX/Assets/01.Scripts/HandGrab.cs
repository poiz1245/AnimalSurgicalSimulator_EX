using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandGrab : MonoBehaviour
{
    [SerializeField] XRSocketInteractor socketInteractor;
    public void OnGripAction()
    {
        socketInteractor.enabled = true;
    }

    public void OffGripAction()
    {
        socketInteractor.enabled = false;
    }
}
