using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static TaskManager;

public class MesTask : BaseTask
{
    [SerializeField] HandModelControll handModel;
    [SerializeField] MesTaskHandModelControll hand;
    [SerializeField] XRGrabInteractable grab;
    [SerializeField] CustomSocket mesSocket;

    public void Update()
    {
        if (!enabled) return;

        switch (TaskManager.instance.task)
        {
            case TaskName.Start:
                if (grab.isSelected)
                {
                    //handGuide.SetActive(true);
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
                TaskManager.instance.isNextTask = mesSocket.hasSelection;
                if (TaskManager.instance.isNextTask == true)
                {

                    Debug.Log("다음 테스크로 전환");
                    TaskStateChange(TaskName.Next);
                }
                break;
            case TaskName.Next:
                TaskManager.instance.UpdateTask(TaskName.Next); // 다음 태스크로 전환
                break;
        }
    }



    protected override TaskManager.MainTask GetMainTaskType()
    {
        return TaskManager.MainTask.Mes;
    }

    protected override void UpdateUIText(TaskManager.TaskName taskName)
    {
        switch (taskName)
        {
            case TaskName.Start:
                uiText.text = "Grab the Mes";
                subUiText.text = "* Follow the Mes guidelines on the right";
                break;
            case TaskName.Attach:
                uiText.text = "Attach the Mes to the guidelines";
                subUiText.text = "* Hold the object and follow the guidelines";
                break;
            case TaskName.Process:
                uiText.text = "Draw the scalpel along the guide";
                subUiText.text = "* Do not let go of the Mes from your hand.";
                break;
            case TaskName.Complete:
                uiText.text = "Bring the Mes back to its original position";
                subUiText.text = "* Take it to the stand. Put your hands down";
                break;
        }
    }
}
