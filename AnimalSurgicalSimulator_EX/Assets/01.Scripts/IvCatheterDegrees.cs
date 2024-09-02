using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IvCatheterDegrees : MonoBehaviour
{
    [SerializeField] GameObject bloodVessel;


    [SerializeField] Transform catheterStartPos;
    [SerializeField] Transform catheterEndPos;
    [SerializeField] Transform skinStartPos;
    [SerializeField] Transform skinEndPos;
    [SerializeField] Transform bloodStartPos;
    [SerializeField] Transform bloodEndPos;

    [SerializeField] TextMeshProUGUI[] cartheterAngleText;

    public float angle { get; private set; }
    public bool isCatheterShot { get; private set; } = false;

    private void Update()
    {
        cartheterAngleText[0].text = angle.ToString("F2") + "¡Æ";
        
        
    }
    void OnTriggerEnter(Collider other)
    {
        Vector3 catheterDirection = catheterEndPos.position - catheterStartPos.position;
        Vector3 skinDirection = skinEndPos.position - skinStartPos.position;

        angle = Vector3.Angle(catheterDirection, skinDirection);

        if (Mathf.Abs(angle - 45f) < 5f && bloodVessel)
        {
            print("aa");
            Vector3 bloodDirection = bloodEndPos.position - bloodStartPos.position;
            angle = Vector3.Angle(catheterDirection, bloodDirection);
            cartheterAngleText[1].text = "Success";
            cartheterAngleText[1].color = Color.green;
        }
        else 
        {
            cartheterAngleText[1].text = "Fail";
            cartheterAngleText[1].color = Color.red;
        }
    }

}
