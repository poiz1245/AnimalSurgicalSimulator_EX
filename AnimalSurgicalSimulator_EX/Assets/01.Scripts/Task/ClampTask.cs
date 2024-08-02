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
    //[SerializeField] TextMeshProUGUI uiText;
    //[SerializeField] TextMeshProUGUI subUiText;
    //[SerializeField] List<Transform> targets; // Ÿ�� ������Ʈ ����Ʈ

    //public delegate void TaskStateChanged(TaskName task);
    //public event TaskStateChanged OnTaskStateChanged;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    OnTaskStateChanged += UpdateUIText;
    //    OnTaskStateChanged += UpdateTargets; // Ÿ�� ������Ʈ�� ���� �̺�Ʈ �߰�

    //    // �ʱ� Ÿ�� ����
    //    UpdateTargets(TaskManager.instance.task);
    //    // �ʱ� UI �ؽ�Ʈ ������Ʈ
    //    UpdateUIText(TaskManager.instance.task);
    //}


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
                if(TaskManager.instance.isNextTask)
                TaskManager.instance.UpdateTask(TaskName.Complete); // ���� �½�ũ�� ��ȯ
                break;
        }
    }

    protected override TaskManager.MainTask GetMainTaskType()
    {
        return TaskManager.MainTask.Clamp;
    }

    //private void TaskStateChange(TaskName taskName)
    //{
    //    TaskManager.instance.task = taskName;
    //    OnTaskStateChanged?.Invoke(TaskManager.instance.task);
    //}


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

    protected override void UpdateTargets(TaskName taskName)
    {
        List<Transform> newTargets = new List<Transform>();

        // TaskName�� ���� Ÿ�� ������Ʈ�� ����
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
                //newTargets.Add(targets[3]);
                break;

        }

        TaskArrow.Instance.SetTargets(newTargets); // Ÿ�� ������Ʈ
    }


    //void UpdateUIText(TaskName taskName)
    //{
    //    switch (taskName)
    //    {
    //        case TaskName.Start:
    //            uiText.text = "Grab the Clamp";
    //            subUiText.text = "* Follow the Mes guidelines on the right";
    //            break;
    //        case TaskName.Attach:
    //            uiText.text = "Attach the Clamp to the guidelines";
    //            subUiText.text = "* Hold the object and follow the guidelines";
    //            break;
    //        case TaskName.Process:
    //            uiText.text = "Spread the clamp with your thumb and ring finger";
    //            subUiText.text = "* Move your thumb and ring finger slowly ";
    //            break;
    //        case TaskName.Complete:
    //            uiText.text = "Bring the Clamp back to its original position";
    //            subUiText.text = "* Take it to the stand. Put your hands down";
    //            break;
    //    }
    //}

    //void UpdateTargets(TaskName taskName)
    //{
    //    List<Transform> newTargets = new List<Transform>();

    //    // TaskName�� ���� Ÿ�� ������Ʈ�� ����
    //    switch (taskName)
    //    {
    //        case TaskName.Start:
    //            newTargets.Add(targets[0]);
    //            break;
    //        case TaskName.Attach:
    //            newTargets.Add(targets[1]);
    //            break;
    //        case TaskName.Process:
    //            newTargets.Add(targets[2]);
    //            break;
    //        case TaskName.Complete:
    //            newTargets.Add(targets[3]);
    //            break;

    //    }

    //    TaskArrow.Instance.SetTargets(newTargets); // Ÿ�� ������Ʈ
    //}
}
