using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] HandTrackingDrillTrigger handDrillTrigger;

    [SerializeField] AudioSource drillSound;
    // Start is called before the first frame update
    void Start()
    {
        handDrillTrigger.buttonSwitchChanged += DrillSoundControll;
    }

    void DrillSoundControll(bool buttonOn)
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
