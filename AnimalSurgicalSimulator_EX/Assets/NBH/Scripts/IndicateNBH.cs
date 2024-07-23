using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicateNBH : MonoBehaviour
{
    [SerializeField] HandModelControll controller;
    [SerializeField] HandTrackingModelControllNBH handModel;
    public void OnIndicate()
    {
        if (!controller.currentTaskComplete)
        {
            gameObject.SetActive(true);
        }
    }

    public void OffIndicate()
    {
        gameObject.SetActive(false);
    }
}
