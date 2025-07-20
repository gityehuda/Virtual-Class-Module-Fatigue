using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSittingPosition : MonoBehaviour
{
    [HideInInspector] public Vector3 positionSitting;
    [HideInInspector] public Quaternion rotationSitting;

    [HideInInspector] public Vector3 positionStandUp;
    private void Start()
    {
        positionSitting = gameObject.transform.TransformPoint(new Vector3(0,-0.4262442f,-0.05f));
        rotationSitting = gameObject.transform.rotation * Quaternion.Euler(0,180f,0);
        positionStandUp = gameObject.transform.position - new Vector3(0, 0.4976918f, 0);
        positionStandUp = gameObject.transform.TransformPoint(new Vector3(0, -0.4976918f, 0.562f));
    }
}

