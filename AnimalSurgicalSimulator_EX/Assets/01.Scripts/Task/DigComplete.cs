using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigComplete : MonoBehaviour
{
    [SerializeField] GameObject taskObject;


    public void TaskComplete()
    {
        taskObject.SetActive(false);
    }
}
