using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPositionReset : MonoBehaviour
{
    [SerializeField] Transform dogModel;
    [SerializeField] Transform[] crossSections;

    Transform originalDogTransform;
    Transform[] originalCrossSectionTransform;
    
    /*Vector3 originalDogPosition;
    Vector3[] originalCrossSectionPositions;
    Quaternion originalDogRotation;
    Quaternion[] originalCrossSectionRotations;*/

    void Start()
    {
        originalDogTransform.position = dogModel.position;
        originalDogTransform.rotation = dogModel.rotation;

        for (int i = 0; i < crossSections.Length; i++)
        {
            originalCrossSectionTransform[i].position = crossSections[i].transform.position;
            originalCrossSectionTransform[i].rotation = crossSections[i].transform.rotation;
        }
        /* originalDogPosition = dogModel.transform.position;
         originalDogRotation = dogModel.transform.rotation;

         originalCrossSectionPositions = new Vector3[crossSections.Length];
         originalCrossSectionRotations = new Quaternion[crossSections.Length];
         for (int i = 0; i < crossSections.Length; i++)
         {
             originalCrossSectionPositions[i] = crossSections[i].transform.position;
             originalCrossSectionRotations[i] = crossSections[i].transform.rotation;
         }*/
    }

    public void ResetPosition()
    {
        dogModel.position = originalDogTransform.position;
        dogModel.rotation = originalDogTransform.rotation;

        for (int i = 0; i < crossSections.Length; i++)
        {
            crossSections[i].transform.position = originalCrossSectionTransform[i].position;
            crossSections[i].transform.rotation = originalCrossSectionTransform[i].rotation;
        }
    }
}
