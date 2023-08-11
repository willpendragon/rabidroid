using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public float horizontalInput;
    public float verticalInput;
    [SerializeField] Animator characterAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;

        if (movement.magnitude > 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(newRotation);
        }
        
        rb.MovePosition(transform.position + movement);
        characterAnimator.SetFloat("Speed", movement.magnitude);
    }
}