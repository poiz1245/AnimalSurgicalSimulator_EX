using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Indicate : MonoBehaviour
{
    [SerializeField] HandModelControll controller;
    [SerializeField] HandTrackingModelControll handTrackingController;
    [SerializeField] XRSocketInteractor handSocketInteractor;
    public void OnIndicate()
    {
        if (!controller.currentTaskComplete && !handTrackingController.currentTaskComplete)
        {
            //if (handSocketInteractor.enabled || handTrackingController.currentTaskComplete)
            //{
            gameObject.SetActive(true);
            //}
        }
    }

    public void OffIndicate()
    {
        gameObject.SetActive(false);
    }
}
