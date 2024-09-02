using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Interaction.Toolkit;

public class BloodVesselGrabModelControll : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject handModel;

    [SerializeField] IvCatheterHandModelControll ivCatheter;
    [SerializeField] HandVisualizer handVisualizer;
    
    public bool isAttach { get; private set; } = false;

    private void Update()
    {
        float distance = Vector3.Distance(indicator.transform.position, gameObject.transform.position);

        print(distance);
        print(gameObject.name);

        if (/*!ivCatheter.currentTaskComplete &&*/ ivCatheter.isGrab && !isAttach && distance <= 0.1f)
        {
            indicator.SetActive(false);
            Attach();
        }
        else if (/*!ivCatheter.currentTaskComplete &&*/ isAttach && distance > 0.3f)
        {
            indicator.SetActive(true);
            Detach();
        }
    }

    private void Attach()
    {
       
        print("¤±¤±");
        handVisualizer.m_LeftHandMesh.SetActive(false);
        handModel.SetActive(true);
        isAttach = true;
    }

    private void Detach()
    {
        handVisualizer.m_LeftHandMesh.SetActive(true);
        handModel.SetActive(false);
        isAttach = false;
    }
}
