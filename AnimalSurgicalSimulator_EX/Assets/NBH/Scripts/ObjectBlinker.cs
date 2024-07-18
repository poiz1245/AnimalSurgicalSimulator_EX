using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectBlinker : MonoBehaviour
{
    [SerializeField] XRBaseInteractor socketInteractor; // 소켓 인터렉터 참조
    [SerializeField] float blinkInterval = 0.5f; // 깜빡이는 간격
    [SerializeField] List<XRGrabInteractable> grabInteractors; // XRGrabInteractable 리스트
    [SerializeField] List<GameObject> objectsToBlink; // 깜빡이게 할 오브젝트 리스트

    bool isBlinking = false;
    bool isObjectInSocket = false;
    bool isGrabbed = false;
    int grabbedIndex = -1;

    void Start()
    {
        // 모든 오브젝트를 비활성화
        foreach (var obj in objectsToBlink)
        {
            obj.SetActive(false);
        }

        // 각 grabInteractor에 대해 SelectEntered 이벤트를 등록
        for (int i = 0; i < grabInteractors.Count; i++)
        {
            int index = i; // 캡처된 인덱스
            grabInteractors[i].selectEntered.AddListener((args) => OnGrabbed(index));
            grabInteractors[i].selectExited.AddListener((args) => OnReleased(index));
        }
    }

    void Update()
    {
        if (!isGrabbed)
        {
            if (grabbedIndex >= 0 && grabbedIndex < objectsToBlink.Count)
            {
                objectsToBlink[grabbedIndex].SetActive(false);
            }
            return;
        }

        // 소켓에 오브젝트가 있는지 확인
        isObjectInSocket = socketInteractor.hasSelection;

        // 소켓에 오브젝트가 없고 깜빡이는 중이 아니라면 깜빡이기 시작
        if (!isObjectInSocket && !isBlinking)
        {
            objectsToBlink[grabbedIndex].SetActive(true);
            StartCoroutine(BlinkObject(grabbedIndex));
        }
        else if (isObjectInSocket && !isBlinking)
        {
            objectsToBlink[grabbedIndex].SetActive(false);
        }
    }

    private IEnumerator BlinkObject(int index)
    {
        isBlinking = true;

        while (!isObjectInSocket)
        {
            objectsToBlink[index].SetActive(!objectsToBlink[index].activeSelf);
            yield return new WaitForSeconds(blinkInterval);
            isObjectInSocket = socketInteractor.hasSelection;
        }

        // 오브젝트가 소켓에 들어가면 깜빡이기 멈춤
        objectsToBlink[index].SetActive(true);
        isBlinking = false;
    }

    private void OnGrabbed(int index)
    {
        isGrabbed = true;
        grabbedIndex = index;
    }

    private void OnReleased(int index)
    {
        isGrabbed = false;
        grabbedIndex = -1;
    }
}
