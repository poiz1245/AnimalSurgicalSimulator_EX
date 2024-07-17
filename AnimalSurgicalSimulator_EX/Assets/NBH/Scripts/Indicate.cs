using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicate : MonoBehaviour
{
    public void OnIndicate()
    {
        gameObject.SetActive(true);
    }

    public void OffIndicate()
    {
        gameObject.SetActive(false);
    }
}
