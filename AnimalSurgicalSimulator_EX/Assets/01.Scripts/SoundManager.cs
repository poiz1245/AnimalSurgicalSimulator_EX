using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SoundManager : MonoBehaviour
{
    [SerializeField] HandTrackingDrillTrigger handDrillTrigger;
    [SerializeField] XRGrabInteractable drillGrabInteractable;
    [SerializeField] AudioSource drillSound;
    // Start is called before the first frame update
    void Start()
    {
        handDrillTrigger.buttonSwitchChanged += DrillSoundControll;
    }

    void DrillSoundControll(bool buttonOn)
    {
        if (drillGrabInteractable.isSelected)
        {
            if (buttonOn)
            {
                drillSound.Play();
            }
            else
            {
                drillSound.Stop();
            }
        }
    }
}
