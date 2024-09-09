using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodTrigger : MonoBehaviour
{
    [SerializeField] IvCatheterDegrees ivCatheter;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("catheter"))
        {
            ivCatheter.isCatheterContact = true;
            ivCatheter.isDeepCatheter = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("catheter"))
        {
            ivCatheter.isCatheterContact = false;
        }
    }
}
