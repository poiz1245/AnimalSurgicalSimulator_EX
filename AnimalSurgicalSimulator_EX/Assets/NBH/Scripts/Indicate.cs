using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicate : MonoBehaviour
{
    [SerializeField] HandModelControll controller;
    [SerializeField] HandTrackingModelControll hadnTrackingController;
    public void OnIndicate()
    {
        if (!controller.currentTaskComplete && !hadnTrackingController.currentTaskComplete)
        {
            gameObject.SetActive(true);
        }
    }

    public void OffIndicate()
    {
        gameObject.SetActive(false);
    }
}
