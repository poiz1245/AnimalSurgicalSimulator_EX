using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Hands.Gestures;
using UnityEngine.XR.Interaction.Toolkit;

public class HandGrab : MonoBehaviour
{
    [SerializeField] XRSocketInteractor socketInteractor;
    [SerializeField] TextMeshProUGUI gripText;

    int currentHandShape;

    public void OnGripAction()
    {
        socketInteractor.enabled = true;
    }

    public void LayerSetting()
    {
        InteractionLayerMask interacitonLayer = currentHandShape;
        socketInteractor.interactionLayers = 1 << interacitonLayer;
        if (gripText != null)
        {
            gripText.text = InteractionLayerMask.LayerToName(currentHandShape);
        }
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
