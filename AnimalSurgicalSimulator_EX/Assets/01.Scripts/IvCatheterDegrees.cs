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
    [SerializeField] IvCatheterHandModelControll attachCatheter;
    [SerializeField] BloodTrigger bloodTrigger;

    public  TextMeshProUGUI[] cartheterAngleText;

    public bool isBloodContact { get; private set; } = false ;
    public bool isCatheterShot = false;
    public bool isCatheterContact = false;
    public bool isDeepCatheter = false;
    bool isBloodTrigger = false;
    float bloodAngle;
    float angle;
    float timer = 0f;
    const float requiredTime = 3f;
    private void Update()
    {
        if (attachCatheter.isAttach)
        {
            Vector3 catheterDirection = catheterEndPos.position - catheterStartPos.position;
            Vector3 skinDirection = skinEndPos.position - skinStartPos.position;

            angle = Vector3.Angle(catheterDirection, skinDirection);
            cartheterAngleText[0].text = angle.ToString("F2") + "¡Æ";

            if (isBloodTrigger)
            {
                Vector3 bloodDirection = bloodEndPos.position - bloodStartPos.position;
                bloodAngle = Vector3.Angle(catheterDirection, bloodDirection);
                if (Mathf.Abs(angle - 45f) < 2f)
                {
                    cartheterAngleText[1].text = "Success";
                    cartheterAngleText[1].color = Color.green;

                    timer += Time.deltaTime;
                    if (timer >= requiredTime)
                    {
                        isBloodContact = true;
                    }

                    if (isCatheterContact)
                    {
                        isCatheterShot = true;
                        cartheterAngleText[1].text = "Shot";
                        cartheterAngleText[1].color = Color.green;
                        if (isDeepCatheter)
                        {
                            cartheterAngleText[1].text = "It's too deep.";
                            cartheterAngleText[1].color = Color.red;
                            isCatheterShot = false;
                        }
                    }
                    else if (!isCatheterContact)
                    {
                        isCatheterShot = false;
                    }
                }
                else
                {
                    cartheterAngleText[1].text = "Fail";
                    cartheterAngleText[1].color = Color.red;
                    isBloodContact = false;
                    timer = 0f;
                }
            }
        }
        else if (!isCatheterShot && isBloodContact)
        {
            cartheterAngleText[0].text = bloodAngle.ToString("F2") + "¡Æ";
        }
        else if (!isBloodContact)
        {
            cartheterAngleText[1].text = "Fail";
            cartheterAngleText[1].color = Color.red;
            cartheterAngleText[0].text = "0¡Æ";
        }
    }
    void OnTriggerStay(Collider other)
    {
        isBloodTrigger = true; 
    }

    void OnTriggerExit(Collider other)
    {
        isBloodTrigger = false; 
    }
}
