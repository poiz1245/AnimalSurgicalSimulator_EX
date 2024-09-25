using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPositionReset : MonoBehaviour
{
    [SerializeField] Transform dogModel;
    [SerializeField] Transform[] crossSections;

    Transform originalDogTransform;
    Transform[] originalCrossSectionTransform;

    private void Awake()
    {
        originalCrossSectionTransform = new Transform[crossSections.Length];
        originalDogTransform = new Vector3(0,0,0);
    }
    void Start()
    {
        originalDogTransform.position = dogModel.position;
        originalDogTransform.rotation = dogModel.rotation;

        for (int i = 0; i < crossSections.Length; i++)
        {
            originalCrossSectionTransform[i].position = crossSections[i].transform.position;
            originalCrossSectionTransform[i].rotation = crossSections[i].transform.rotation;
        }
    }

    public void ResetPosition()
    {
        //dogModel = originalDogTransform;
        dogModel.position = originalDogTransform.position;
        dogModel.rotation = originalDogTransform.rotation;

        for (int i = 0; i < crossSections.Length; i++)
        {
            //crossSections[i] = originalCrossSectionTransform[i];
            crossSections[i].transform.position = originalCrossSectionTransform[i].position;
            crossSections[i].transform.rotation = originalCrossSectionTransform[i].rotation;
        }
    }
}
