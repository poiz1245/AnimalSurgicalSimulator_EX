using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static TaskManager;

public class DigTask : BaseTask
{
    [SerializeField] HandModelControll handModel;
    [SerializeField] DrillTaskHandModelControll hand;
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
                if (TaskManager.instance.isNextTask)
                    TaskManager.instance.UpdateTask(TaskName.Complete); // 다음 태스크로 전환
                    TaskArrow.Instance.isCompleteArrow = true; // 마지막 Task에만 추가
                break;
        }
    }
    protected override TaskManager.MainTask GetMainTaskType()
    {
        return TaskManager.MainTask.Dig;
    }

    protected override void UpdateUIText(TaskManager.TaskName taskName)
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
            case TaskName.Process:
                uiText.text = "Put your fist on the drill";
                subUiText.text = "* Pull the index finger to activate the drill";
                break;
            case TaskName.Complete:
                uiText.text = "Bring the drill back to its original position";
                subUiText.text = "* Take it to the stand. Put your hands down";
                break;
        }
    }

    protected override void UpdateTargets(TaskManager.TaskName taskName)
    {
        List<Transform> newTargets = new List<Transform>();

        switch (taskName)
        {
            case TaskName.Start:
                newTargets.Add(targets[0]);
                break;
            case TaskName.Attach:
                newTargets.Add(targets[1]);
                break;
            case TaskName.Process:
                newTargets.Add(targets[2]);
                break;
            case TaskName.Complete:
                newTargets.Add(targets[3]);
                break;
        }

        TaskArrow.Instance.SetTargets(newTargets);
    }
}
