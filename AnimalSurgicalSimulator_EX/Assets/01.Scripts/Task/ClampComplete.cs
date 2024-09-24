using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampComplete : MonoBehaviour
{
    [SerializeField] GameObject taskObject;

    //Animator anim;

    private void Start()
    {
        //anim = taskObject.GetComponent<Animator>();
    }
    public void TaskComplete()
    {
        taskObject.SetActive(false);
        //애니메이션 실행
        //anim.SetBool("retractor", true);
    }
}
