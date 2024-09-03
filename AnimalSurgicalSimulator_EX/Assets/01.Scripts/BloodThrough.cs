using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodThrough : MonoBehaviour
{
    [SerializeField] IvCatheterDegrees ivCatheter;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("catheter"))
        {
            ThroughBlood();
        }
    }

    void ThroughBlood()
    {
        ivCatheter.cartheterAngleText[1].text = "It's too deep.";
        ivCatheter.cartheterAngleText[1].color = Color.red;
        ivCatheter.isCatheterShot = false;
    }
}


