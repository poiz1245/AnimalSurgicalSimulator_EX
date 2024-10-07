using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CrossSectionManager))]
public class CrossSectionEditor : Editor
{
    Renderer[] crossSection;
    Transform crossSectionPlaneX;
    Transform crossSectionPlaneY;
    Transform crossSectionPlaneZ;
    CrossSectionManager crossSectionManager;

    private void OnEnable()
    {
        EditorApplication.update += UpdateEditor;

        crossSectionManager = (CrossSectionManager)target;
    }

    private void OnDisable()
    {
        EditorApplication.update -= UpdateEditor;
    }

    private void UpdateEditor()
    {
        UpdateCrossSections(crossSectionManager);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private void UpdateCrossSections(CrossSectionManager crossSectionManager)
    {
        Renderer[] crossSection = crossSectionManager.crossSection;
        Transform crossSectionPlaneX = crossSectionManager.crossSectionPlaneX;
        Transform crossSectionPlaneY = crossSectionManager.crossSectionPlaneY;
        Transform crossSectionPlaneZ = crossSectionManager.crossSectionPlaneZ;

        if (crossSection != null)
        {
            for (int i = 0; i < crossSection.Length; i++)
            {
                crossSection[i].sharedMaterial.SetFloat("_PlanePositionX", crossSectionPlaneX.position.x);
                crossSection[i].sharedMaterial.SetFloat("_PlanePositionY", crossSectionPlaneY.position.y);
                crossSection[i].sharedMaterial.SetFloat("_PlanePositionZ", crossSectionPlaneZ.position.z);

                /*crossSection[i].sharedMaterial.SetVector("_PlaneDirectionX", crossSectionPlaneX.TransformDirection(crossSectionManager.transform.forward));
                crossSection[i].sharedMaterial.SetVector("_PlaneDirectionY", crossSectionPlaneY.TransformDirection(crossSectionManager.transform.forward));
                crossSection[i].sharedMaterial.SetVector("_PlaneDirectionZ", crossSectionPlaneZ.TransformDirection(crossSectionManager.transform.forward));*/

                crossSection[i].sharedMaterial.SetVector("_PlaneDirectionX", crossSectionPlaneX.transform.forward);
                crossSection[i].sharedMaterial.SetVector("_PlaneDirectionY", crossSectionPlaneY.transform.forward);
                crossSection[i].sharedMaterial.SetVector("_PlaneDirectionZ", crossSectionPlaneZ.transform.forward);

                /*crossSection[i].sharedMaterial.SetVector("_PlaneDirectionX", new Vector3(-1,0,0));
                crossSection[i].sharedMaterial.SetVector("_PlaneDirectionY", new Vector3(0,1,0));
                crossSection[i].sharedMaterial.SetVector("_PlaneDirectionZ", new Vector3(0,0,1));*/
            }
        }
    }
}
