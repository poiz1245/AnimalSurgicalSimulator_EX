using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Hands.Gestures;
using UnityEngine.XR.Hands.Samples.Gestures.DebugTools;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using static TaskManager;

public class ForcepTrigger : MonoBehaviour
{
    [SerializeField] GetMiddleFingerShapeCondition middleFingerShapeCondition;

    

    private void Start()
    {
        middleFingerShapeCondition.OnValueChanged += Vibration;
    }

    void Vibration(float value)
    {
        HapticsTest.instance.CustomBasic(1, 0.1f);

        /*if (value <= 0.4f)
        {
            print("aa");
            HapticsTest.instance.CustomBasic(1, 0.1f);

        }
        else if (value > 0.4f && value <= 0.8f)
        {
            print("bb");
            HapticsTest.instance.CustomBasic(1, 0.1f);
        }
        else if (value > 0.8f && value <= 1f)
        {
            print("cc");
            HapticsTest.instance.CustomBasic(1, 0.1f);
        }*/
    }
}
