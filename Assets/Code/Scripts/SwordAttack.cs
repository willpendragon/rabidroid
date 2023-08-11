using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwordAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 10;
    public float attackDuration = 1.5f;
    public float attackCooldown = 1.0f;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Movement playerMovement;
    private bool isAttacking = false;
    private float lastAttackTime = -Mathf.Infinity;
    private Vector2 lastAttackDirection = Vector2.up;
    public Weapon equippedWeapon;
    // Update is called once per frame
    void Awake()
    {

    }
    
    void Update()
    {
        if (Time.time - lastAttackTime >= attackCooldown && Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            playerAnimator.SetBool("isAttacking", true);
            playerMovement.currentState = PlayerState.attack;
            isAttacking = true;
            Invoke("EndAttack", attackDuration);
            Attack();
            lastAttackTime = Time.time;
        }

        // Update the attack point position based on the player's movement
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (moveDirection.magnitude > 0f)
        {
            lastAttackDirection = moveDirection;
        }
        attackPoint.position = (Vector2)transform.position + lastAttackDirection * 0.5f;
    }

    void Attack()
    {
        // Shoot raycast in the direction of the last attack
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, lastAttackDirection, attackRange, enemyLayer);
        Debug.DrawRay(attackPoint.position, lastAttackDirection * attackRange, Color.red, 0.1f);

        // Apply damage to enemy if hit
        if (hit.collider != null && hit.collider.GetComponent<Enemy>())
        {
            //hit.collider.GetComponent<Enemy>().TakeDamage(attackDamage + equippedWeapon.attackPower);
        }
    }

    void EndAttack()
    {
        playerMovement.currentState = PlayerState.idle;
        playerAnimator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void MultiplyDamage()
    {
        attackDamage = attackDamage +5;
    }
}