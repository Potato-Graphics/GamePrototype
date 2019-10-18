using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage1 : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        transform.position = target.position + offset; 
    }
}
