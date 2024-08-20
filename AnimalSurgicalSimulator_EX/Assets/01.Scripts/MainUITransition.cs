using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainUITransition : MonoBehaviour
{
    Camera mainCamera;
    float playerYPosition;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(positionUpdate());
    }

    IEnumerator positionUpdate()
    {
        yield return new WaitForSeconds(1f);
        playerYPosition = mainCamera.transform.position.y;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, playerYPosition, gameObject.transform.position.z);
    }
}