using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandTrackingBedControll : MonoBehaviour
{
    [SerializeField] GameObject surgicalBed;

    public void BedUp()
    {
        surgicalBed.transform.DOLocalMove(new Vector3(0, 0.1f, 0), 3).SetEase(Ease.Linear);
    }
    public void BedDown()
    {
        surgicalBed.transform.DOLocalMove(new Vector3(0, -0.1f, 0), 3).SetEase(Ease.Linear);
    }
}
