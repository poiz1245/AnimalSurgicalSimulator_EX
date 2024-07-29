using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabGuideControll : MonoBehaviour
{

    [SerializeField] GameObject guideMesh;
    void Update()
    {
        if(TaskManager.instance.task == TaskManager.TaskName.Start)
        {
            guideMesh.SetActive(true);
        }
        else
        {
            guideMesh.SetActive(false);
        }
    }
}
