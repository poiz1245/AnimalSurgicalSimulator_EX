using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands.Gestures;
using UnityEngine.XR.Hands.Samples.Gestures.DebugTools;
using UnityEngine.XR.Hands;

public class GetMiddleFingerShapeCondition : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The handedness to get the finger states for.")]
    Handedness m_Handedness;

    XRFingerShape[] m_XRFingerShapes;

    static List<XRHandSubsystem> s_SubsystemsReuse = new List<XRHandSubsystem>();

    float value;

    public enum currentState
    {
        Open,
        Close
    }

    currentState state = currentState.Open;

    public delegate void ValueChanged(float value);
    public event ValueChanged OnValueChanged;

    void Start()
    {
        if (m_Handedness == Handedness.Invalid)
        {
            m_Handedness = Handedness.Right;
        }

        m_XRFingerShapes = new XRFingerShape[(int)XRHandFingerID.Little - (int)XRHandFingerID.Thumb + 1];
    }

    void Update()
    {
        var subsystem = TryGetSubsystem();

        if (subsystem == null)
            return;

        var hand = m_Handedness == Handedness.Left ? subsystem.leftHand : subsystem.rightHand;
        m_XRFingerShapes[2] = hand.CalculateFingerShape((XRHandFingerID)2, XRFingerShapeTypes.All);
        UpdateFingerShapeUIs(2);
    }
    void UpdateFingerShapeUIs(int fingerIndex)
    {
        var shapes = m_XRFingerShapes[fingerIndex];

        if (shapes.TryGetPinch(out var pinch))
        {
            SetFingerShape(pinch);
        }

        /*for (var i = 0; i < m_XRFingerShapeDebugGraphs.Length; i++)
        {
            if (shapes.TryGetFullCurl(out var fullCurl))
            {
                SetFingerShape((int)XRFingerShapeType.FullCurl, fullCurl);
            }

            if (shapes.TryGetBaseCurl(out var baseCurl))
            {
                SetFingerShape((int)XRFingerShapeType.BaseCurl, baseCurl);
            }

            if (shapes.TryGetTipCurl(out var tipCurl))
            {
                SetFingerShape((int)XRFingerShapeType.TipCurl, tipCurl);
            }

            if (shapes.TryGetPinch(out var pinch))
            {
                SetFingerShape((int)XRFingerShapeType.Pinch, pinch);
            }

            if (shapes.TryGetSpread(out var spread))
            {
                SetFingerShape((int)XRFingerShapeType.Spread, spread);
            }

        }*/
    }

    void SetFingerShape(float value)
    {
        this.value = Mathf.Clamp(value, 0f, 1f);
        this.value = Mathf.Round(value * 10f) / 10f;

        if (state == currentState.Open && this.value == 0.4f)
        {
            OnValueChanged?.Invoke(this.value);
            state = currentState.Close;
        }
        else if (state == currentState.Close && this.value == 0.7f)
        {
            OnValueChanged?.Invoke(this.value);
        }
    }

    public float GetFingerShape()
    {
        return value;
    }
    static XRHandSubsystem TryGetSubsystem()
    {
        SubsystemManager.GetSubsystems(s_SubsystemsReuse);
        return s_SubsystemsReuse.Count > 0 ? s_SubsystemsReuse[0] : null;
    }
}
