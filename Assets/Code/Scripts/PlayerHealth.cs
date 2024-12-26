using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    [SerializeField] private float attackInterval = 0.5f;
    public bool playerIsTakingDamage;
    private Enemy currentEnemy; // Track the current enemy causing damage
    [SerializeField] private float disengageDistance = 2f; // Distance to stop damage

    public void Start()
    {
        UIManager.Instance.SetupPlayerHPBar(maxHealth, currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");
        UpdatePlayerHPBar();

        if (currentHealth <= 0)
        {
            Debug.Log("Player has died!");
            // Handle death logic here
        }
    }

    public void UpdatePlayerHPBar()
    {
        UIManager.Instance.UpdatePlayerHPBar(currentHealth);
    }

    private IEnumerator ContinuousDamage(Enemy enemy, float interval)
    {
        playerIsTakingDamage = true;
        currentEnemy = enemy;

        while (playerIsTakingDamage)
        {
            TakeDamage(enemy.enemyDamage);
            yield return new WaitForSeconds(interval);

            // Check distance to stop damage
            float distance = Vector3.Distance(transform.position, currentEnemy.transform.position);
            if (distance > disengageDistance)
            {
                Debug.Log("Player moved away from Enemy!");
                playerIsTakingDamage = false; // Stop taking damage
            }
        }

        currentEnemy = null; // Clear reference after stopping damage
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player collided with Enemy");

            Enemy enemy = hit.gameObject.GetComponent<Enemy>();
            if (enemy != null && !playerIsTakingDamage)
            {
                StartCoroutine(ContinuousDamage(enemy, attackInterval)); // Apply damage continuously
            }
        }
    }
}
