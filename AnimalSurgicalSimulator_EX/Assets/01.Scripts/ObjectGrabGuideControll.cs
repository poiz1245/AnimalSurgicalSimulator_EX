using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectGrabGuideControll : MonoBehaviour
{
    //[SerializeField] List<GameObject> handMesh; // �پ��� �ڵ� �޽��� �����ϴ� ����Ʈ
    //int currentIndex;

    //public enum GripObject
    //{
    //    Mes,
    //    Clamp,
    //    Dig
    //}

    //private void Start()
    //{
    //    // ���� ���� �½�ũ�� ������� �޽� ������Ʈ
    //    UpdateMeshes(TaskManager.instance.currentMainTask);
    //    // ���� �½�ũ�� ����� �� �̺�Ʈ�� ����
    //    TaskManager.instance.OnMainTaskChanged += UpdateMeshes;

    //    for (int i = 0; i < handMesh.Count; i++)
    //    {
    //        int index = i;
    //        // �� �ڵ� �޽��� ���� ��ü���� XRGrabInteractable ������Ʈ�� ������
    //        XRGrabInteractable grabInteractable = handMesh[i].GetComponentInParent<XRGrabInteractable>();
    //        // ��ü�� ���õǾ��� �� �̺�Ʈ�� ����
    //        grabInteractable.selectEntered.AddListener((interactor) => GrabObject());
    //        // ��ü ������ �����Ǿ��� �� �̺�Ʈ�� ����
    //        grabInteractable.selectExited.AddListener((interactor) => ReleaseObject());
    //    }
    //}

    //private void OnDestroy()
    //{
    //    // ���� �½�ũ ���� �̺�Ʈ ���� ����
    //    TaskManager.instance.OnMainTaskChanged -= UpdateMeshes;
    //}

    //// ���� ���� �½�ũ�� ���� �ڵ� �޽� ������Ʈ
    //public void UpdateMeshes(TaskManager.MainTask currentMainTask)
    //{
    //    // ��� �ڵ� �޽� ��Ȱ��ȭ
    //    for (int i = 0; i < handMesh.Count; i++)
    //    {
    //        handMesh[i].SetActive(false);
    //    }

    //    // ���� ���� �½�ũ�� �´� �ڵ� �޽��� Ȱ��ȭ
    //    switch (currentMainTask)
    //    {
    //        case TaskManager.MainTask.Mes:
    //            handMesh[0].SetActive(true);
    //            currentIndex = 0;
    //            break;
    //        case TaskManager.MainTask.Clamp:
    //            handMesh[1].SetActive(true);
    //            currentIndex = 1;
    //            break;
    //        case TaskManager.MainTask.Dig:
    //            handMesh[2].SetActive(true);
    //            currentIndex = 2;
    //            break;
    //    }
    //}

    //// Ư�� �޽��� �׷��� �� ó��
    //public void GrabObject()
    //{
    //    for (int i = 0; i < handMesh.Count; i++) 
    //    { 
    //        handMesh[i].SetActive(false);
    //    }
    //}

    //// Ư�� �޽��� �׷��� ������ �� ó��
    //void ReleaseObject()
    //{
    //    for (int i = 0; i < handMesh.Count; i++)
    //    {
    //        if(i == currentIndex)
    //        {
    //            handMesh[i].SetActive(true);
    //        }


    //    }
    //}

    [SerializeField] List<GameObject> handMesh; // �پ��� �ڵ� �޽��� �����ϴ� ����Ʈ
    int currentIndex;

    public enum GripObject
    {
        Mes,
        Clamp,
        Dig
    }

    private void Start()
    {
        // ���� ���� �½�ũ�� ������� �޽� ������Ʈ
        UpdateMeshes(TaskManager.instance.currentMainTask);
        // ���� �½�ũ�� ����� �� �̺�Ʈ�� ����
        TaskManager.instance.OnMainTaskChanged += UpdateMeshes;

        for (int i = 0; i < handMesh.Count; i++)
        {
            int index = i; // ���� �ε����� ĸó
            XRGrabInteractable grabInteractable = handMesh[index].GetComponentInParent<XRGrabInteractable>();
            grabInteractable.selectEntered.AddListener((interactor) => GrabObject(index));
            grabInteractable.selectExited.AddListener((interactor) => ReleaseObject(index));
        }
    }

    private void OnDestroy()
    {
        // ���� �½�ũ ���� �̺�Ʈ ���� ����
        TaskManager.instance.OnMainTaskChanged -= UpdateMeshes;
    }

    // ���� ���� �½�ũ�� ���� �ڵ� �޽� ������Ʈ
    public void UpdateMeshes(TaskManager.MainTask currentMainTask)
    {
        for (int i = 0; i < handMesh.Count; i++)
        {
            handMesh[i].SetActive(false);
        }

        switch (currentMainTask)
        {
            case TaskManager.MainTask.Mes:
                currentIndex = 0;
                break;
            case TaskManager.MainTask.Clamp:
                currentIndex = 1;
                break;
            case TaskManager.MainTask.Dig:
                currentIndex = 2;
                break;
        }

        handMesh[currentIndex].SetActive(true);
    }

    // Ư�� �޽��� �׷��� �� ó��
    public void GrabObject(int index)
    {
        if (index == currentIndex)
        {
            handMesh[index].SetActive(false);
        }
    }

    // Ư�� �޽��� �׷��� ������ �� ó��
    public void ReleaseObject(int index)
    {
        if (index == currentIndex)
        {
            handMesh[index].SetActive(true);
        }
    }
}