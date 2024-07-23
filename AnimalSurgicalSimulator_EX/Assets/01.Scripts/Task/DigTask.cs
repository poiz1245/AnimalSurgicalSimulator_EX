using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DigTask : MonoBehaviour
{
    [SerializeField] HandModelControll handModel;
    [SerializeField] XRGrabInteractable grab;
    [SerializeField] HandTrackingModelControll hand;


    [SerializeField] TextMeshProUGUI uiText;

    public enum TaskComplete
    {
        Start,
        Attach,
        Dig,
        Complete
    }

    private TaskComplete task = TaskComplete.Start; // 현재 작업 상태

    // Update is called once per frame
    void Update()
    {
        switch (task)
        {
            case TaskComplete.Start:
                UpdateUIText();
                if (grab.isSelected)
                {
                    task = TaskComplete.Attach;
                    UpdateUIText();
                }
                break;

            case TaskComplete.Attach:
                if (handModel.isAttach || hand.isAttach)

                {
                    task = TaskComplete.Dig;
                    UpdateUIText();
                }
                break;

            case TaskComplete.Dig:
                if (handModel.currentTaskComplete || hand.currentTaskComplete)
                {
                    task = TaskComplete.Complete;
                    UpdateUIText();
                }
                break;

            case TaskComplete.Complete:
                // 작업 완료 후 추가 동작이 필요하다면 여기에 작성
                break;
        }
        if (!grab.isSelected && task == TaskComplete.Attach)
        {
            task = TaskComplete.Start;
            UpdateUIText();
        }
        else if ((handModel.isAttach == false && hand.isAttach == false) && task == TaskComplete.Dig)
        {
            task = TaskComplete.Attach;
            UpdateUIText();
        }
    }

    void UpdateUIText()
    {
        switch (task)
        {
            case TaskComplete.Start:
                uiText.text = "Grab the drill";
                break;
            case TaskComplete.Attach:
                uiText.text = "Attach the drill to the guidelines";
                break;
            case TaskComplete.Dig:
                uiText.text = "Put your fist on the drill";
                break;
            case TaskComplete.Complete:
                uiText.text = "Bring the drill back to its original position";
                break;
        }
    }
}
