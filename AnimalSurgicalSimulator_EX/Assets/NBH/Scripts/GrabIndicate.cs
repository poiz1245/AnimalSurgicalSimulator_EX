using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabIndicate : MonoBehaviour
{
    //드릴 잡았을때
    public UnityEvent OnGrab; 
    //드릴 놓았을때
    public UnityEvent OnRelease;

    public void Grab(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject;
        if (interactor is XRDirectInteractor)
        {
            Debug.Log("잡음");
            OnGrab?.Invoke();
        }
    }

    public void Release(SelectExitEventArgs args)
    {
        var interactor = args.interactorObject;
        if (interactor is XRDirectInteractor)
        {
            Debug.Log("놓음");
            OnRelease?.Invoke();
        }
    }
}
