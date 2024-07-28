using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampTrigger : MonoBehaviour
{
    [SerializeField] GameObject moveClamp;

    public void OpenClamp()
    {
        moveClamp.transform.DOLocalRotate(new Vector3(0, 0, 20), 0.1f);
    }

    public void CloseClamp()
    {
        moveClamp.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);
    }
}
