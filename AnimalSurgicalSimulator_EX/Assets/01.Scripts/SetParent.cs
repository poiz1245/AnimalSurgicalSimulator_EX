using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    [SerializeField] Transform rightHand;
    [SerializeField] Transform drillAttach;
    public void AddParent()
    {
        gameObject.transform.SetParent(rightHand);
        gameObject.transform.position = drillAttach.position;
    }
    public void DeleteParent()
    {
        gameObject.transform.SetParent(null);

    }
}
