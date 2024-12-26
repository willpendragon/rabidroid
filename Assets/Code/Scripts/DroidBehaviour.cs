using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DroidState
{
    aggressive,
    peaceful,
}

public class DroidBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D droidRigidbody;
    public float minDistance = 1.0f;
    private GameObject playerPrefab;
    public float speed = 3f;

    [Header("Combat")]
    public string enemyTag = "Enemy";
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    private GameObject enemyPrefab;
    private float attackTimer = 0f;
    private float distanceToPlayer;
    private float distanceToEnemy;
    public DroidState droidState;
    [SerializeField] float scanRadius;
    [SerializeField] Animator droidAnim;

    public GameObject enemyTarget;

    void Awake()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        droidState = DroidState.peaceful;
    }
    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
        if (droidState == DroidState.aggressive)
        {
            ScanForEnemy();
        }
        else
            MoveToPlayer();

        // Cooldown timer
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }
    void ScanForEnemy()
    {
        Debug.Log("Scanning for Enemy");
        Collider2D[] actorsInSight = Physics2D.OverlapCircleAll(transform.position, scanRadius);
        foreach (Collider2D col in actorsInSight)
        {
            if (col.tag == "Enemy" && Vector3.Distance(this.gameObject.transform.position, col.gameObject.transform.position) < 1)
            {
                enemyTarget = col.gameObject;
                AttackEnemy();
                Debug.Log("Droid is Attacking");
            }
        }
    }
    void AttackEnemy()
    {
        Vector2 enemyPosition = enemyTarget.transform.position;
        Vector2 direction = (enemyPosition - droidRigidbody.position).normalized;
        droidRigidbody.velocity = direction * speed;
        if (attackTimer <= 0)
        {
            droidAnim.SetBool("isAttacking", true);
            Debug.Log("Droid attacked the enemy!");
            // Reset the attack timer
            attackTimer = attackCooldown;
        }
    }
    void MoveToPlayer()
    {
        Debug.Log("MovingToPlayer");
        Vector2 playerPosition = playerPrefab.transform.position;
        Vector2 direction = (playerPosition - droidRigidbody.position).normalized;
        distanceToPlayer = Vector3.Distance(this.gameObject.transform.position, playerPrefab.transform.position);
        if (enemyPrefab != null)
        {
            distanceToEnemy = Vector3.Distance(this.gameObject.transform.position, enemyPrefab.transform.position);

        }
        if (distanceToPlayer > minDistance)
        {
            droidRigidbody.velocity = direction * speed;
        }
    }
    public void SwitchMode()
    {
        if (droidState != DroidState.aggressive)
        {
            droidState = DroidState.aggressive;
        }
        else
        {
            droidState = DroidState.peaceful;
            droidAnim.SetBool("isAttacking", false);
        }

    }
}