using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    public DigComplete digComplete;
    [SerializeField] DigTask digTask;
    [SerializeField] TextMeshProUGUI uiText;
    public enum TaskName
    {
        Start,
        Attach,
        Dig,
        Complete
    }

    public TaskName task = TaskName.Start; // 현재 작업 상태

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        digTask.OnTaskStateChanged += UpdateUIText;
        UpdateUIText(task);
    }

    void UpdateUIText(TaskName taskName)
    {
        switch (taskName)
        {
            case TaskName.Start:
                uiText.text = "Grab the drill";
                break;
            case TaskName.Attach:
                uiText.text = "Attach the drill to the guidelines";
                break;
            case TaskName.Dig:
                uiText.text = "Put your fist on the drill";
                break;
            case TaskName.Complete:
                uiText.text = "Bring the drill back to its original position";
                break;
        }
    }
}
