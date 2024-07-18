using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectBlinker : MonoBehaviour
{
    [SerializeField] XRBaseInteractor socketInteractor; // 소켓 인터렉터 참조
    [SerializeField] GameObject objectToBlink; // 깜빡이게 할 오브젝트
    [SerializeField] float blinkInterval = 0.5f; // 깜빡이는 간격
    [SerializeField] XRGrabInteractable[] grabInteractor;

    bool isBlinking = false;
    bool isObjectInSocket = false;

    void Start()
    {
        objectToBlink.SetActive(false);
    }
    void Update()
    {
        // 소켓에 오브젝트가 있는지 확인
        isObjectInSocket = socketInteractor.hasSelection;

        // 소켓에 오브젝트가 없고 깜빡이는 중이 아니라면 깜빡이기 시작
        if (!isObjectInSocket && !isBlinking)
        {
            objectToBlink.SetActive(true);
            StartCoroutine(BlinkObject());
        }
        else if(isObjectInSocket && !isBlinking)
        {
            objectToBlink.SetActive(false);
        }
    }

    private IEnumerator BlinkObject()
    {
        isBlinking = true;

        while (!isObjectInSocket)
        {
            objectToBlink.SetActive(!objectToBlink.activeSelf);
            yield return new WaitForSeconds(blinkInterval);
            isObjectInSocket = socketInteractor.hasSelection;
        }

        // 오브젝트가 소켓에 들어가면 깜빡이기 멈춤
        objectToBlink.SetActive(true);
        isBlinking = false;
    }
}
