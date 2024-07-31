using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GripGuideOnOffTemp : MonoBehaviour
{
    [SerializeField] GameObject guideMesh;
    XRGrabInteractable grabInteractable;

    private void Start()
    {
        grabInteractable = GetComponentInParent<XRGrabInteractable>();
    }

    //private void Update()
    //{
    //    if (grabInteractable.isSelected)
    //    {
    //        guideMesh.SetActive(false);
    //    }
    //    else
    //    {
    //        guideMesh.SetActive(true);
    //    }
    //}

    public void GrabObject()
    {
        guideMesh.SetActive(false);
    }

    public void ReleaseObject()
    {
        guideMesh.SetActive(true);
    }
}
