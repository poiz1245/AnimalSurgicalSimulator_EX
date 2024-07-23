using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;
using UnityEngine.XR.OpenXR.Features.Interactions;

public class HapticsTest : MonoBehaviour
{

    public static HapticsTest instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    public void CustomBasic(float amplitude, float duration)
    {
        //Bhaptics Portal 내에서 제작한 햅틱의 옵션을 재 정의하여 사용하는 명령어
        BhapticsLibrary.PlayParam(
            BhapticsEvent.EXAMPLE, // 재생시킬 햅틱 이름 (BhapticsEvent.EventID) EventID는 항상 대소문자 
            amplitude,   // 햅틱 강도
            duration    // 햅틱 지속 시간
        );
    }
}
