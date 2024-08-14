using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] GameObject currentPanel;
    GameObject prevPanel;

    public void Focus(GameObject myPanel)
    {
        prevPanel = currentPanel;
        currentPanel = myPanel;

        prevPanel.SetActive(false);
        currentPanel.SetActive(true);
    }

    public void Back()
    {
        currentPanel = prevPanel;
        prevPanel = null;

        currentPanel.SetActive(true);
    }

    public void SimulationStart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}