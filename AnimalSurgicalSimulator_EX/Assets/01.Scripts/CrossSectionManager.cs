using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSectionManager : MonoBehaviour
{
    public Renderer[] crossSection;
    public Transform crossSectionPlaneX;
    public Transform crossSectionPlaneY;
    public Transform crossSectionPlaneZ;

    void Update()
    {

        for (int i = 0; i < crossSection.Length; i++)
        {
            /*crossSection[i].material.SetFloat("_PlanePositionX", crossSectionPlaneX.localPosition.x);
            crossSection[i].material.SetFloat("_PlanePositionY", crossSectionPlaneY.localPosition.y);
            crossSection[i].material.SetFloat("_PlanePositionZ", crossSectionPlaneZ.localPosition.z);*/
            crossSection[i].material.SetFloat("_PlanePositionX", crossSectionPlaneX.position.x);
            crossSection[i].material.SetFloat("_PlanePositionY", crossSectionPlaneY.position.y);
            crossSection[i].material.SetFloat("_PlanePositionZ", crossSectionPlaneZ.position.z);

            crossSection[i].material.SetVector("_PlaneDirectionX", crossSectionPlaneX.transform.forward);
            crossSection[i].material.SetVector("_PlaneDirectionY", crossSectionPlaneY.transform.forward);
            crossSection[i].material.SetVector("_PlaneDirectionZ", crossSectionPlaneZ.transform.forward);
        }
    }
}
