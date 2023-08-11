using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow3D : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 3f, -10f);
    public float mouseSensitivity = 10.0f;
    public float minXRotation = -75f;
    public float maxXRotation = 75f;
    private float rotationY = 0.0f;
    private float rotationX = 0.0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.captureDeltaTime;
        rotationY -= mouseY;
        rotationX += mouseX;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        rotationX = Mathf.Clamp(rotationX, minXRotation, maxXRotation);
        
        Quaternion localRotation = Quaternion.Euler(rotationY, rotationX, 0.0f);
        transform.position = target.position - (localRotation * offset);
        transform.LookAt(target.position);
    }
}
