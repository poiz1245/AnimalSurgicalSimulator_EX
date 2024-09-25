using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPositionReset : MonoBehaviour
{
    [SerializeField] GameObject dogModel;
    [SerializeField] GameObject[] crossSections;

    Vector3 originalDogPosition;
    Vector3[] originalCrossSectionPositions;
    Quaternion originalDogRotation;
    Quaternion[] originalCrossSectionRotations;

    void Start()
    {
        originalDogPosition = dogModel.transform.position;
        originalDogRotation = dogModel.transform.rotation;

        originalCrossSectionPositions = new Vector3[crossSections.Length];
        originalCrossSectionRotations = new Quaternion[crossSections.Length];
        for (int i = 0; i < crossSections.Length; i++)
        {
            originalCrossSectionPositions[i] = crossSections[i].transform.position;
            originalCrossSectionRotations[i] = crossSections[i].transform.rotation;
        }
    }

    public void ResetPosition()
    {
        dogModel.transform.position = originalDogPosition;
        dogModel.transform.rotation = originalDogRotation;

        for (int i = 0; i < crossSections.Length; i++)
        {
            crossSections[i].transform.position = originalCrossSectionPositions[i];
            crossSections[i].transform.rotation = originalCrossSectionRotations[i];
        }
    }
}
