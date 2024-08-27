using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandWashCount : MonoBehaviour
{
    [SerializeField] ScrubTaskHandModelControll scrub;
    [SerializeField] Slider slider;
    [SerializeField] GameObject guidePanel;
    [SerializeField] GameObject completePanel;

    // Update is called once per frame
    void Update()
    {
        slider.value = scrub.scrubhand * 0.03f;
        if (scrub.scrubhand == 30)
        {
            guidePanel.SetActive(false);
            completePanel.SetActive(true);
        }
    }
}
