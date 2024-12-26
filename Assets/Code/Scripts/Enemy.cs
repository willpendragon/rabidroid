using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;

public enum EnemyState
{
    Alive,
    Dead,
}
public enum TargetedState
{
    Targeted,
    Free
}
public class Enemy : MonoBehaviour
{
    public delegate void EnemyDeath();
    public static event EnemyDeath OnEnemyDeath;
    public UnityEvent OnSlimeDeath;

    [Header("Enemy Stats")]
    public float health;
    [SerializeField] float enemyAttackRange;
    [SerializeField] float enemySightRange;
    public float enemyDamage;

    [Header("Graphics")]
    [SerializeField] SkinnedMeshRenderer enemyMeshRenderer;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] ParticleSystem enemyDeathFX;
    [SerializeField] SkinnedMeshRenderer enemyMesh;

    [Header("UI")]
    [SerializeField] Canvas enemyDataCanvas;
    [SerializeField] TextMeshProUGUI bossHPCounter;
    [SerializeField] Slider hpSlider;

    [Header("Gameplay")]
    public EnemyState enemyState;
    public TargetedState targetedState;
    public GameObject playerPrefab;
    [SerializeField] float gainedExpAmount;
    [SerializeField] int rockUnitsLoot;
    [SerializeField] GameObject lootedObject;
    public bool alertModeIsActive;
    public bool canAttack;

    private GameManager gameManager;
    private UIManager uiManager;
    public NavMeshAgent agent;
    public GameObject playerModel;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.FindGameObjectWithTag("GameUI").GetComponent<UIManager>();
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        enemyState = EnemyState.Alive;
        targetedState = TargetedState.Free;
        SetHealthPointsSlider();
    }
    void Update()
    {
        SearchForPlayer();
        UpdateHPSlider();
    }

    void SearchForPlayer()
    {
        //If the Alert Mode is Active, sets the Agent current destination to the Player current position.
        if (alertModeIsActive == true && enemyState != EnemyState.Dead)
        {
            enemyAnimator.SetInteger("animation", 2);
            agent.SetDestination(playerModel.transform.position);
        }
    }

    public void SetHealthPointsSlider()
    {
        //Retrieves the HP value from the Enemy stats and sets the UI accordingly.
        hpSlider.maxValue = health;
        hpSlider.value = health;
    }

    public void UpdateHPSlider()
    {
        hpSlider.value = health;
    }

    public void SwitchToAlertMode()
    {
        // This methods changes the Enemy's behaviour from passive to aggressive.
        alertModeIsActive = true;
    }

    public void TakeDamage(float droidAttackPower)
    {
        // Subtracts an amount of Health Points from this Enemy corresponding to the Attack Power
        // of the corresponding RabiDroid Unit.
        if (enemyState != EnemyState.Dead)
        {
            enemyAnimator.SetInteger("animation", 4);
            enemyMeshRenderer.material.color = Color.red;
            health -= droidAttackPower;
            UpdateHPSlider();
            CheckRemaniningHealth();
        }
    }
    public void CheckRemaniningHealth()

    {
        // Checks if this Enemy's health has reached 0. If positive, calls the Die method on this Enemy.
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        OnSlimeDeath.Invoke();
        OnEnemyDeath();
        Debug.Log("Enemy Death");
        enemyDataCanvas.enabled = false;
        // Sets the current state of the Enemy to Dead.
        enemyState = EnemyState.Dead;
        // Stops this Navmesh Agent's movement.
        agent.isStopped = true;
    }
    public void PlayEnemyDeathVisuals()
    {
        //Triggers the Enemy's death animation and the explosion effect.
        {
            enemyAnimator.SetInteger("animation", 5);
        }
    }
    public void InitiateDestroy()
    {
        StartCoroutine("PlayEnemyExplosionVFX");
        Destroy(this.gameObject, 4);
    }
    IEnumerator PlayEnemyExplosionVFX()
    {
        yield return new WaitForSeconds(2);
        enemyDeathFX.Play();
    }

    public void DeactivateEnemyCollider()
    {
        this.GetComponent<Collider>().enabled = false;
    }
    public void SetTargetedState()
    {
        targetedState = TargetedState.Targeted;
    }
}