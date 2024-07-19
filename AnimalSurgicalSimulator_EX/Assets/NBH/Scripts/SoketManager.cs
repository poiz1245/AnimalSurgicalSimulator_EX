using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketManager : MonoBehaviour
{
    [System.Serializable]
    public class SocketConfig
    {
        public CustomSocket socket;
        public List<GameObject> allowedObjects;
    }

    public SocketConfig[] socketConfigs;

    private void OnEnable()
    {
        foreach (var config in socketConfigs)
        {
            config.socket.allowedObjects = config.allowedObjects;
            config.socket.selectEntered.AddListener(OnSelectEntered);
        }
    }

    private void OnDisable()
    {
        foreach (var config in socketConfigs)
        {
            config.socket.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject as CustomSocket;
        var interactableObject = args.interactableObject.transform.gameObject;

        foreach (var config in socketConfigs)
        {
            if (config.socket == interactor)
            {
                if (config.allowedObjects.Contains(interactableObject))
                {
                    // 올바른 오브젝트가 소켓에 들어갔습니다.
                    return;
                }
                else
                {
                    // 잘못된 오브젝트가 소켓에 들어갔으므로 제거합니다.
                    interactor.interactionManager.SelectExit(interactor, args.interactableObject);
                }
            }
        }
    }
}
