using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TaskManager;
using UnityEngine.XR.Interaction.Toolkit;

public class ReplacementTask : BaseTask
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
                if (TaskManager.instance.isNextTask)
                    TaskManager.instance.UpdateTask(TaskName.Complete); // 다음 태스크로 전환
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
                uiText.text = "Grab the Bone Impactor";
                subUiText.text = "* Follow the Bone Impactor guidelines on the right";
                break;
            case TaskName.Attach:
                uiText.text = "Attach the Bone Impactor to the guidelines";
                subUiText.text = "* Hold the object and follow the guidelines";
                break;
            case TaskName.Process:
                uiText.text = "Hit this impactor and put a prosthesis in it";
                subUiText.text = "* If the prosthesis is in, remove the impactor";
                break;
            case TaskName.Complete:
                uiText.text = "Bring the Clamp back to its original position";
                subUiText.text = "* Take it to the stand. Put your hands down";
                break;
        }
    }

    protected override void UpdateTargets(TaskName taskName)
    {
        List<Transform> newTargets = new List<Transform>();

        // TaskName에 따라 타겟 오브젝트를 변경
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

        TaskArrow.Instance.SetTargets(newTargets); // 타겟 업데이트
    }
}
