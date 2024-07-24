using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static TaskManager;

public class DigTask : MonoBehaviour
{
    [SerializeField] HandModelControll handModel;
    [SerializeField] HandTrackingModelControll hand;
    [SerializeField] XRGrabInteractable grab;

    [SerializeField] TextMeshProUGUI uiText;

    public delegate void TaskStateChanged(TaskName task);
    public event TaskStateChanged OnTaskStateChanged;

    public void Update()
    {
        switch (TaskManager.instance.task)
        {
            case TaskName.Start:
                if (grab.isSelected)
                {
                    TaskStateChange(TaskName.Attach);
                }
                break;

            case TaskName.Attach:
                if (handModel.isAttach || hand.isAttach)
                {
                    TaskStateChange(TaskName.Dig);
                }
                else if (!grab.isSelected)
                {
                    TaskStateChange(TaskName.Start);
                }
                break;

            case TaskName.Dig:
                if (handModel.currentTaskComplete || hand.currentTaskComplete)
                {
                    TaskStateChange(TaskName.Complete);
                }
                else if ((handModel.isAttach == false && hand.isAttach == false))
                {
                    TaskStateChange(TaskName.Attach);
                }
                break;
        }
    }

    private void TaskStateChange(TaskName taskName)
    {
        TaskManager.instance.task = taskName;
        OnTaskStateChanged?.Invoke(TaskManager.instance.task);
    }
}
