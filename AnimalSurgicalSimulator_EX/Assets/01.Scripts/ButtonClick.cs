using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] GameObject currentPanel;
    GameObject prevPanel;

    Stack<GameObject> prevPanelList = new Stack<GameObject>();
    public void Focus(GameObject myPanel)
    {
        prevPanel = currentPanel;
        prevPanelList.Push(prevPanel);
        currentPanel = myPanel;

        prevPanel.SetActive(false);
        currentPanel.SetActive(true);
    }

    public void Back()
    {
        prevPanel = currentPanel;

        if (prevPanelList.Count > 0) 
        {
            currentPanel = prevPanelList.Pop();
        }

        prevPanel.SetActive(false);
        currentPanel.SetActive(true);
    }

    public void SimulationStart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}