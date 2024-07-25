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
    [SerializeField] TextMeshProUGUI subUiText;

    public delegate void TaskStateChanged(TaskName task);
    public event TaskStateChanged OnTaskStateChanged;

    private void Start()
    {
        OnTaskStateChanged += UpdateUIText;
        UpdateUIText(TaskManager.instance.task);
    }

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

    void UpdateUIText(TaskName taskName)
    {
        switch (taskName)
        {
            case TaskName.Start:
                uiText.text = "Grab the drill";
                subUiText.text = "* Follow the drill guidelines on the right";
                break;
            case TaskName.Attach:
                uiText.text = "Attach the drill to the guidelines";
                subUiText.text = "* Hold the object and follow the guidelines";
                break;
            case TaskName.Dig:
                uiText.text = "Put your fist on the drill";
                subUiText.text = "* Pull the index finger to activate the drill";
                break;
            case TaskName.Complete:
                uiText.text = "Bring the drill back to its original position";
                subUiText.text = "* Take it to the stand. Put your hands down";
                break;
        }
    }
}
