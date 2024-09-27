using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashHandSensor : MonoBehaviour
{
    [SerializeField] GameObject handTrigger;
    [SerializeField] Transform waterPosition;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] ParticleSystem waterSpray;

    int triggerCount = 0;
    float maxRayDistance = 0.8f;
    bool isRayActive = false;

    void Update()
    {
        if (isRayActive)
        {
            ShootRay();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == handTrigger)
        {
            triggerCount++;

            if (triggerCount % 2 == 1)
            {
                StartRay();
            }
            else
            {
                StopRay();
            }
        }
    }

    private void StartRay()
    {
        isRayActive = true;
        lineRenderer.enabled = true;
        waterSpray.Play();
    }

    private void StopRay()
    {
        isRayActive = false;
        lineRenderer.enabled = false;
        waterSpray.Stop();
    }

    void ShootRay()
    {
        RaycastHit hit;
        Ray ray = new Ray(waterPosition.position, Vector3.down);
        Vector3 endPoint = waterPosition.position + Vector3.down.normalized * maxRayDistance;

        if (Physics.Raycast(ray, out hit,maxRayDistance))
        {
            Debug.Log("레이에 맞은 오브젝트: " + hit.collider.name);
            //lineRenderer.SetPosition(0, waterPosition.position);
            //lineRenderer.SetPosition(1, endPoint);
        }
        else
        {
            Debug.Log("레이가 아무것도 맞지 않았습니다.");
            //lineRenderer.SetPosition(0, waterPosition.position);
            //lineRenderer.SetPosition(1, endPoint);
        }
    }
}
