using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Hands.Gestures;
using UnityEngine.XR.Interaction.Toolkit;

public class HandGrab : MonoBehaviour
{
    [SerializeField] XRSocketInteractor socketInteractor;

    int currentHandShape;

    public void OnGripAction()
    {
        socketInteractor.enabled = true;
    }

    public void LayerSetting()
    {
        socketInteractor.interactionLayers = currentHandShape;
    }
    public void OffGripAction()
    {
        socketInteractor.enabled = false;
    }

    public void SettingHandShape(int layer)
    {
        currentHandShape = layer;
    }
}
