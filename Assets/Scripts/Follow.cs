using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform[] targetTransform;
    public int index = 0;
    public Vector3 positionOffset;
    public Vector3 lookAtOffset;

    void Start()
    {
        this.transform.parent = null;
    }

    void LateUpdate()
    {
        if (targetTransform != null)
        {
            Vector3 offset = targetTransform[index].TransformDirection(positionOffset);
            this.transform.position = targetTransform[index].position + offset;
            this.transform.LookAt(targetTransform[index].position + targetTransform[index].TransformDirection(lookAtOffset));
        }
    }
}
