using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SurgicalBedControll : MonoBehaviour
{
    [SerializeField] GameObject surgicalBed;
    [SerializeField] InputActionReference primaryButton;
    [SerializeField] InputActionReference secondaryButton;
    bool upButton;
    bool downButton;
    private void Start()
    {
        primaryButton.action.started += BedUp;
        secondaryButton.action.started += BedDown;

        primaryButton.action.canceled -= BedUp;
        secondaryButton.action.canceled -= BedDown;

        primaryButton.action.canceled += Stop;
        secondaryButton.action.canceled += Stop;
        primaryButton.action.started -= Stop;
        secondaryButton.action.started -= Stop;
    }

    void BedUp(InputAction.CallbackContext context)
    {
        //upButton = true;
        surgicalBed.transform.DOLocalMove(new Vector3(0, 0.5f, 0), 3f).SetEase(Ease.Linear);
    }
    void BedDown(InputAction.CallbackContext context)
    {
        surgicalBed.transform.DOLocalMove(new Vector3(0, -0.1f, 0), 3).SetEase(Ease.Linear);
    }
    void Stop(InputAction.CallbackContext contexxt)
    {
        surgicalBed.transform.DOKill();
    }
}
