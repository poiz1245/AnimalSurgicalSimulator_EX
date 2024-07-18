using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicate : MonoBehaviour
{
    [SerializeField] HandModelControll controller;
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
