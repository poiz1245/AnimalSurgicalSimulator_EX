using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampTrigger : MonoBehaviour
{
    [SerializeField] GameObject moveClamp;

    bool clampOpen = false;
    public void OpenClamp()
    {
        moveClamp.transform.DOLocalRotate(new Vector3(0, 0, 20), 0.1f);
        clampOpen = true;
    }

    public void CloseClamp()
    {
        moveClamp.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);

        if (clampOpen)
        {
            HapticsTest.instance.CustomBasic(0.5f, 0.1f);
            clampOpen=false;
        }
    }
}
