using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static TaskManager;

public abstract class BaseTask : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI uiText;
    [SerializeField] protected TextMeshProUGUI subUiText;

    public delegate void TaskStateChanged(TaskManager.TaskName task);
    public event TaskStateChanged OnTaskStateChanged;

    protected virtual void Start()
    {
        TaskManager.instance.OnMainTaskChanged += OnMainTaskChanged;
        OnTaskStateChanged += UpdateUIText;

        if (TaskManager.instance.currentMainTask == GetMainTaskType())
        {
            UpdateUIText(TaskManager.instance.task);
        }
    }

    protected virtual void OnDestroy()
    {
        TaskManager.instance.OnMainTaskChanged -= OnMainTaskChanged;
        OnTaskStateChanged -= UpdateUIText;
    }

    protected void TaskStateChange(TaskManager.TaskName taskName)
    {
        TaskManager.instance.task = taskName;
        OnTaskStateChanged?.Invoke(taskName);
    }


    protected abstract TaskManager.MainTask GetMainTaskType();
    protected abstract void UpdateUIText(TaskManager.TaskName taskName);

    private void OnMainTaskChanged(TaskManager.MainTask newMainTask)
    {
        enabled = newMainTask == GetMainTaskType();
        if (enabled)
        {
            UpdateUIText(TaskManager.instance.task);
        }
    }
}