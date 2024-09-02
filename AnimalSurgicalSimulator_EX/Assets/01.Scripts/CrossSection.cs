using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSection : MonoBehaviour
{
    [SerializeField] Renderer[] crossSection;
    Vector3 planeDirection;
    [SerializeField] Transform crossSectionPlaneX;
    [SerializeField] Transform crossSectionPlaneY;
    [SerializeField] Transform crossSectionPlaneZ;

    void Update()
    {
        //planeDirection = transform.forward;
        for (int i = 0; i < crossSection.Length; i++)
        {
            crossSection[i].material.SetFloat("_PlanePositionX", crossSectionPlaneX.position.x);
            crossSection[i].material.SetFloat("_PlanePositionY", crossSectionPlaneX.position.y);
            crossSection[i].material.SetFloat("_PlanePositionZ", crossSectionPlaneX.position.z);
            //crossSection[i].material.SetVector("_PlaneDirection", planeDirection);
            //crossSection[i].material.SetVector("_PlanePosition", transform.position);
        }
    }
}
