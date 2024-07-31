using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClampTrigger : MonoBehaviour
{
    [SerializeField] GameObject moveClamp;

    XRGrabInteractable grabInteractable;
    public bool clampOpen { get; private set; } = false;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }
    public void OpenClamp()
    {
        if (grabInteractable.isSelected)
        {
            print("aa");

            moveClamp.transform.DOLocalRotate(new Vector3(0, 0, 20), 0.1f);
            clampOpen = true;
        }
    }

    public void CloseClamp()
    {
        if(grabInteractable.isSelected)
        {
            if (clampOpen)
            {
                moveClamp.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);
                HapticsTest.instance.CustomBasic(0.5f, 0.1f);
                clampOpen = false;
            }
        }
        
    }
}
