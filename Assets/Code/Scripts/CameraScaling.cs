using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
    float targetAspect = 16f / 9f; // Set the target aspect ratio (e.g., 16:9)
    float currentAspect = (float)Screen.width / (float)Screen.height;
    Camera.main.orthographicSize = Camera.main.orthographicSize * (targetAspect / currentAspect);
    }
}
