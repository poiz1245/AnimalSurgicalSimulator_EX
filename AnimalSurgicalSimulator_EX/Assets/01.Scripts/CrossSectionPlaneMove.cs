using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CrossSectionPlaneMove : MonoBehaviour
{
    [SerializeField] GameObject playerHand;
    [SerializeField] GameObject dog;
    [SerializeField] XRGrabInteractable dogInteractable;

    XRGrabInteractable grabInteractable;

    bool firstSelected = false;

    //Vector3 offset;
    //Vector3 targetPosition;
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void DeGrab()
    {
        firstSelected = false;
        StopCoroutine(Move());
    }

    public void OnGrab()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        Vector3 lastHandPosition = Vector3.zero;

        while (grabInteractable.isSelected)
        {
            Vector3 handPosition = playerHand.transform.position;

            if (!firstSelected)
            {
                lastHandPosition = handPosition;
                firstSelected = true;
            }

            Vector3 currentHandPosition = handPosition;
            Vector3 movement = currentHandPosition - lastHandPosition;
            Vector3 forwardDirection = transform.forward;

            float dotProduct = Vector3.Dot(forwardDirection, movement.normalized);

            if (dotProduct > 0)
            {
                transform.localPosition += forwardDirection * movement.magnitude;
            }
            else if (dotProduct < 0)
            {
                transform.localPosition -= forwardDirection * movement.magnitude;
            }
            lastHandPosition = currentHandPosition;
            yield return null;
        }
    }
    public void AddParent()
    {
        StartCoroutine(FollowMove());
    }


    public void FollowStop()
    {
        StopCoroutine(FollowMove());
    }

    IEnumerator FollowMove()
    {
        Vector3 targetPosition = dog.transform.position;
        Vector3 offset = transform.position - targetPosition;

        while (dogInteractable.isSelected)
        {
            targetPosition = dog.transform.position;
            transform.position = targetPosition + offset;

            yield return null;
        }
    }
}
