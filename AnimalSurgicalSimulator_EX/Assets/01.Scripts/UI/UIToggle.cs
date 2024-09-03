using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{
    [SerializeField] GameObject obj;
    Renderer renderer;
    private void Start()
    {
        renderer = obj.GetComponent<Renderer>();
    }
    public void ActiveControll()
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void MeshActiveControll()
    {
        renderer.enabled = !renderer.enabled;
    }
}
