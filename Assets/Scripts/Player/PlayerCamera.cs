using System;
using UnityEngine;


public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    public Vector3 offset;


    private void Start()
    {
        transform.position = target.position + offset;
    }

    private void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    
}