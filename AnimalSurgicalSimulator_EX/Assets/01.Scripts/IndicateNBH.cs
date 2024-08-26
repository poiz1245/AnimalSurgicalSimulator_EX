using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicateNBH : MonoBehaviour
{
    [SerializeField] HandModelControll controller;
    [SerializeField] ScrubTaskHandModelControll handModel;
    public void OnIndicate()
    {
        if (!controller.currentTaskComplete && !handModel.currentTaskComplete)
        {
            gameObject.SetActive(true);
        }
    }

    public void OffIndicate()
    {
        gameObject.SetActive(false);
    }
}
