using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicateNBH : MonoBehaviour
{
    [SerializeField] HandModelControllNBH controller;
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
