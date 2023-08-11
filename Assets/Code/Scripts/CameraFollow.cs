using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform; // reference to the player's transform

    public Vector3 offset; // offset between the camera and the player

    // Update is called once per frame

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        transform.position = playerTransform.position + offset;
    }
}
