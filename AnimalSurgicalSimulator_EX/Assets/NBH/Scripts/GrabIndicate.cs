using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabIndicate : MonoBehaviour
{
    [SerializeField] GameObject indicate;
    private void Start()
    {
        indicate.SetActive(false);
    }
}
