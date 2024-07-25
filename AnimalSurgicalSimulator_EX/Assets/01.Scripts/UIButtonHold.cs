using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHolding = false;
    [SerializeField] GameObject surgicalBed;

    public enum ButtonName
    {
        BedUp,
        BedDown,
        Reset
    }

    [SerializeField] ButtonName buttonName;
    // Update 메서드에서 실행할 동작
    private void Update()
    {
        if (isHolding)
        {
            if (buttonName == ButtonName.BedUp)
            {
                BedUp();
            }
            else if(buttonName == ButtonName.BedDown)
            {
                BedDown();
            }
            else if(buttonName == ButtonName.Reset)
            {
                SceneReset();
            }
        }
    }

    // 버튼을 눌렀을 때 호출되는 메서드
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        HapticsTest.instance.CustomBasic(0.05f, 0.1f);
    }

    // 버튼에서 손을 뗐을 때 호출되는 메서드
    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        Stop();
    }


    public void BedUp()
    {
        surgicalBed.transform.DOLocalMove(new Vector3(0, 0.5f, 0), 3).SetEase(Ease.Linear);
    }
    public void BedDown()
    {
        surgicalBed.transform.DOLocalMove(new Vector3(0, -0.1f, 0), 3).SetEase(Ease.Linear);
    }
    void Stop()
    {
        surgicalBed.transform.DOKill();
    }
    public void SceneReset()
    {
        //SceneManager.LoadScene("OperationRoom");
        SceneManager.LoadScene("OperationRoomNBH");
    }
}
