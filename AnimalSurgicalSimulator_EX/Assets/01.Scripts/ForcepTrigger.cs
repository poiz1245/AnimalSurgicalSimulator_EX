using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Hands.Gestures;
using UnityEngine.XR.Hands.Samples.VisualizerSample;

public class ForcepTrigger : MonoBehaviour
{
    public HandVisualizer handVisualizer;
    public void Vibration()
    {
        /* if (sisorShape.fingerShapeConditions[0]. <= 0.1f)
         {
             print("aa");
             HapticsTest.instance.CustomBasic(1, 0.1f);

         }
         else if (sisorShape.fingerShapeConditions[0].targets[0].desired > 0.1f && sisorShape.fingerShapeConditions[0].targets[0].desired <= 0.2f)
         {
             print("bb");
             HapticsTest.instance.CustomBasic(1, 0.1f);
         }*/
    }
}
