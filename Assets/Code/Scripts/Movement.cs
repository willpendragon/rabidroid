using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    idle
}

public class Movement : MonoBehaviour
{

public float speed = 5f;
public PlayerState currentState;
float horizontalInput;
float verticalInput;
private Vector3 movement;

[SerializeField]Rigidbody2D playerRigidbody;
[SerializeField] Animator playerAnimator;

void Start()
{
    currentState = PlayerState.idle;
}
void Update() 
{
    movement = Vector3.zero;
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");
    UpdateAnimationAndMove();
}

void UpdateAnimationAndMove()
{
    if (movement != Vector3.zero && currentState != PlayerState.attack)
    {
        MoveCharacter();
        playerAnimator.SetFloat("movementX", movement.x);
        playerAnimator.SetFloat("movementY", movement.y);
        playerAnimator.SetBool("isMoving", true);
    }
    else
    {
        playerAnimator.SetBool("isMoving", false);
    }
}

void MoveCharacter()
{
    movement.Normalize();   
    playerRigidbody.MovePosition(transform.position + movement * speed * Time.deltaTime);
}
}
