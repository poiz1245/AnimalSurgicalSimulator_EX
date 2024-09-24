using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] Renderer[] targetRenderer;
    Renderer renderer;
    Toggle toggle;
    private void Start()
    {
        renderer = obj.GetComponent<Renderer>();
        toggle = GetComponent<Toggle>();
    }
    public void ActiveControll()
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void MeshActiveControll()
    {
        renderer.enabled = !renderer.enabled;
    }

    public void XInverseControll()
    {
        for (int i = 0; i < targetRenderer.Length; i++)
        {
            targetRenderer[i].material.SetFloat("_InverseX", toggle.isOn ? 1.0f : 0.0f);
        }
    }
    public void YInverseControll()
    {
        for (int i = 0; i < targetRenderer.Length; i++)
        {
            targetRenderer[i].material.SetFloat("_InverseY", toggle.isOn ? 1.0f : 0.0f);
        }
    }
    public void ZInverseControll()
    {
        for (int i = 0; i < targetRenderer.Length; i++)
        {
            targetRenderer[i].material.SetFloat("_InverseZ", toggle.isOn ? 1.0f : 0.0f);
        }
    }
}
