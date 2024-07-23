using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StorageSocketControll : MonoBehaviour
{
    XRSocketInteractor socketInteractor;

    private void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
    }
    public void OnSocketInteractor()
    {
        socketInteractor.enabled = false;
    }

    public void OffSocketInteractor()
    {
        socketInteractor.enabled = true;
    }
}
