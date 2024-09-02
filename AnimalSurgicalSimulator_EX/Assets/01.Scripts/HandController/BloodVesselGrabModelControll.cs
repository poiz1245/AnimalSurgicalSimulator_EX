using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Interaction.Toolkit;

public class BloodVesselGrabModelControll : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject handModel;
    [SerializeField] TextMeshProUGUI distanceText;

    [SerializeField] IvCatheterHandModelControll ivCatheter;
    [SerializeField] HandVisualizer handVisualizer;
    
    public bool isAttach { get; private set; } = false;

    private void Update()
    {
        float distance = Vector3.Distance(indicator.transform.position, gameObject.transform.position);
        distanceText.text = distance.ToString();

        if (/*!ivCatheter.currentTaskComplete &&ivCatheter.isGrab &&*/  !isAttach && distance <= 0.2f)
        {
            indicator.SetActive(false);
            Attach();
        }
        else if (/*!ivCatheter.currentTaskComplete &&*/ isAttach && distance > 0.2f)
        {
            indicator.SetActive(true);
            Detach();
        }
    }

    private void Attach()
    {
       
        print("¤±¤±");
        handVisualizer.SetLeftHandMeshVisibility(false);
        handModel.SetActive(true);
        isAttach = true;
    }

    private void Detach()
    {
        print("oo");
        handVisualizer.SetLeftHandMeshVisibility(true);
        handModel.SetActive(false);
        isAttach = false;
    }
}
