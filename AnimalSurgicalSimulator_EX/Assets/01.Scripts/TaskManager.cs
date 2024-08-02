using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    public DigComplete digComplete;
    public MesComplete mesComplete;
    public ClampComplete clampComplete;
    public bool isNextTask = false;

    [SerializeField] DigTask digTask;
    [SerializeField] ClampTask clampTask;
    [SerializeField] MesTask mesTask;
    [SerializeField] ObjectGrabGuideControll objectGrabGuideControll; //�� ���̵� �޽� ȣ��
    public enum MainTask
    {
        Mes,
        Clamp,
        Dig
    }

    public enum TaskName
    {
        Start,
        Attach,
        Process,
        Complete
    }

    public MainTask currentMainTask = MainTask.Mes;
    public TaskName task = TaskName.Start; // ���� �۾� ����


    public delegate void MainTaskChanged(MainTask newMainTask);
    public event MainTaskChanged OnMainTaskChanged;

    private void Awake()
    {
        if (instance == null)
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
        SetActiveTask(currentMainTask);
        objectGrabGuideControll.UpdateMeshes(currentMainTask); // �� �޽� ������Ʈ
    }

    public void UpdateTask(TaskName newTaskStatus)
    {
        task = newTaskStatus;
        if (task == TaskName.Complete)
        {
            ProceedToNextMainTask(); // ���� �½�ũ�� ��ȯ
        }
    }

    private void ProceedToNextMainTask()
    {
        MainTask nextTask = currentMainTask;

        switch (currentMainTask)
        {
            case MainTask.Mes:
                nextTask = MainTask.Clamp;
                break;
            case MainTask.Clamp:
                nextTask = MainTask.Dig;
                break;
            case MainTask.Dig:
                Debug.Log("��� ���� �۾� �Ϸ�");
                return;
        }

        SetTaskAndActivate(nextTask);
    }

    private void SetTaskAndActivate(MainTask newMainTask)
    {
        currentMainTask = newMainTask;
        task = TaskName.Start; // �� �½�ũ�� ���� ���·� �ʱ�ȭ
        SetActiveTask(currentMainTask);
        OnMainTaskChanged?.Invoke(currentMainTask); // �̺�Ʈ ȣ��
        Debug.Log("���ο� ���� �۾�: " + currentMainTask);
    }

    private void SetActiveTask(MainTask newMainTask)
    {
        // ���� �۾� ��Ȱ��ȭ
        if (digTask != null) digTask.enabled = false;
        if (clampTask != null) clampTask.enabled = false;
        if (mesTask != null) mesTask.enabled = false;

        // ���ο� �۾� Ȱ��ȭ
        switch (newMainTask)
        {
            case MainTask.Mes:
                if (mesTask != null) mesTask.enabled = true;
                break;
            case MainTask.Clamp:
                if (clampTask != null) clampTask.enabled = true;
                break;
            case MainTask.Dig:
                if (digTask != null) digTask.enabled = true;
                break;
        }

    }
}
