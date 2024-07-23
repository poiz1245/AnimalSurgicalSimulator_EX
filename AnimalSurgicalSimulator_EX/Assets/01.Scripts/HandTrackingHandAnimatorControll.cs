using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandTrackingHandAnimatorControll : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void OnDrillTrigger()
    {
        anim.SetBool("Trigger", true);
    }
    public void OffDrillTrigger()
    {
        anim.SetBool("Trigger", false);
    }
}
