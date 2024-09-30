using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static TaskManager;

public class ClampTask : BaseTask
{
    [SerializeField] HandModelControll handModel;
    [SerializeField] ClampTaskHandModelControll hand;
    [SerializeField] XRGrabInteractable grab;


    public void Update()
    {
        if (!enabled) return;

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
                    TaskStateChange(TaskName.Process);
                }
                else if (!grab.isSelected)
                {
                    TaskStateChange(TaskName.Start);
                }
                break;

            case TaskName.Process:
                if (handModel.currentTaskComplete || hand.currentTaskComplete)
                {
                    TaskStateChange(TaskName.Complete);
                }
                else if ((handModel.isAttach == false && hand.isAttach == false))
                {
                    TaskStateChange(TaskName.Attach);
                }
                break;
            case TaskName.Complete:
                TaskManager.instance.UpdateTask(TaskName.Next); // 다음 태스크로 전환
                break;
        }
    }

    protected override TaskManager.MainTask GetMainTaskType()
    {
        return TaskManager.MainTask.Clamp;
    }

    protected override void UpdateUIText(TaskName taskName)
    {
        switch (taskName)
        {
            case TaskName.Start:
                uiText.text = "Grab the Clamp";
                subUiText.text = "* Follow the Clamp guidelines on the right";
                break;
            case TaskName.Attach:
                uiText.text = "Attach the Clamp to the guidelines";
                subUiText.text = "* Hold the object and follow the guidelines";
                break;
            case TaskName.Process:
                uiText.text = "Spread the clamp with your thumb and ring finger";
                subUiText.text = "* Move your thumb and ring finger slowly ";
                break;
            case TaskName.Complete:
                //uiText.text = "Bring the Clamp back to its original position";
                //subUiText.text = "* Take it to the stand. Put your hands down";
                break;
        }
    }
}

