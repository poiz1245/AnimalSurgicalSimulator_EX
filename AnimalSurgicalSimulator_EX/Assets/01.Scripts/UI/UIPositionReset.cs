using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPositionReset : MonoBehaviour
{
    [SerializeField] Transform dogModel;
    [SerializeField] Transform[] crossSections;

    Transform originalDogTransform;
    Transform[] originalCrossSectionTransform;

    void Start()
    {
        originalDogTransform = dogModel;
        //originalDogTransform.position = dogModel.position;
        //originalDogTransform.rotation = dogModel.rotation;

        for (int i = 0; i < crossSections.Length; i++)
        {
            originalCrossSectionTransform[i] = crossSections[i];
            /*originalCrossSectionTransform[i].position = crossSections[i].transform.position;
            originalCrossSectionTransform[i].rotation = crossSections[i].transform.rotation;*/
        }
    }

    public void ResetPosition()
    {
        dogModel = originalDogTransform;
        /*dogModel.position = originalDogTransform.position;
        dogModel.rotation = originalDogTransform.rotation;*/

        for (int i = 0; i < crossSections.Length; i++)
        {
            crossSections[i] = originalCrossSectionTransform[i];
            /*crossSections[i].transform.position = originalCrossSectionTransform[i].position;
            crossSections[i].transform.rotation = originalCrossSectionTransform[i].rotation;*/
        }
    }
}
