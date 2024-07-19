using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandGrab : MonoBehaviour
{
    XRSocketInteractor socketInteractor;
    XRGrabInteractable interactable;
    // Start is called before the first frame update
    void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        interactable = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
