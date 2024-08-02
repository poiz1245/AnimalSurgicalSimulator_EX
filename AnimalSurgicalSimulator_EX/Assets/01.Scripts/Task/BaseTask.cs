using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class BaseTask : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI uiText;
    [SerializeField] protected TextMeshProUGUI subUiText;
    [SerializeField] protected List<Transform> targets;

    public delegate void TaskStateChanged(TaskManager.TaskName task);
    public event TaskStateChanged OnTaskStateChanged;

    protected virtual void Start()
    {
        TaskManager.instance.OnMainTaskChanged += OnMainTaskChanged;
        OnTaskStateChanged += UpdateUIText;
        OnTaskStateChanged += UpdateTargets;
        // �ʱ� Ÿ�� ���� �� UI �ؽ�Ʈ ������Ʈ
        if (TaskManager.instance.currentMainTask == GetMainTaskType())
        {
            TaskManager.instance.isNextTask = false;
            UpdateTargets(TaskManager.instance.task);
            UpdateUIText(TaskManager.instance.task);
        }
    }

    protected virtual void OnDestroy()
    {
        TaskManager.instance.OnMainTaskChanged -= OnMainTaskChanged;
        OnTaskStateChanged -= UpdateUIText;
        OnTaskStateChanged -= UpdateTargets;
    }

    protected void TaskStateChange(TaskManager.TaskName taskName)
    {
        TaskManager.instance.task = taskName;
        OnTaskStateChanged?.Invoke(taskName);
    }


    protected abstract TaskManager.MainTask GetMainTaskType();
    protected abstract void UpdateUIText(TaskManager.TaskName taskName);
    protected abstract void UpdateTargets(TaskManager.TaskName taskName);

    private void OnMainTaskChanged(TaskManager.MainTask newMainTask)
    {
        enabled = newMainTask == GetMainTaskType();
        if (enabled)
        {
            UpdateUIText(TaskManager.instance.task);
            UpdateTargets(TaskManager.instance.task);
        }
    }
}
