using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;

public class HapticsTest : MonoBehaviour
{
    [SerializeField][Range(0.0f, 1.0f)] float intensity = 1.0f;
    [SerializeField][Range(0.0f, 1.0f)] float duration = 1.0f;
    [SerializeField][Range(0.0f, 360.0f)] float angleX = 0.0f;
    [SerializeField][Range(-0.5f, 0.5f)] float offsetY = 0.0f;
    private void OnTriggerEnter(Collider other)
    {
        Baisc();
        //CustomBasic();
        //CustomRange();
    }

    private void Baisc()
    {
        // Bhaptics Portal 내에서 제작한 햅틱을 재생시킬때 사용하는 명령어
        // BhapticsLibrary.Play(BhapticsEvent.EventID); 아래 명령어 처럼 사용시에는 EventID 는 항상 대문자
        BhapticsLibrary.Play(BhapticsEvent.EXAMPLE);
        // BhapticsLibrary.Play("eventID"); 아래 명령어 처럼 사용시에는 EventID 는 BHaptics 에서 만든 EventID 정확하게 입력
        BhapticsLibrary.Play("example");

    }

    private void CustomBasic()
    {
        //Bhaptics Portal 내에서 제작한 햅틱의 옵션을 재 정의하여 사용하는 명령어
        BhapticsLibrary.PlayParam(
            BhapticsEvent.EXAMPLE, // 재생시킬 햅틱 이름 (BhapticsEvent.EventID) EventID는 항상 대소문자 
            0.5f,   // 햅틱 강도
            0.5f,   // 햅틱 지속 시간
            360.0f,  // 햅틱이 시계 방향으로 회전
            0.3f    // 햅틱을 위 아래로 이동
        );
    }
    //변수 Range 속성을 통해 햅틱 이벤트 실행
    private void CustomRange()
    {
        BhapticsLibrary.PlayParam(
            BhapticsEvent.EXAMPLE,
            intensity,
            duration,
            angleX,
            offsetY
        );
    }
}
