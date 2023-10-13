using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    public float rotationSpeed = 10.0f; // Adjust the speed as needed

    void Update()
    {
        // Rotate the camera around the Z-axis at a constant speed
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
