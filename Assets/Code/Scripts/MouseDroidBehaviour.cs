using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum DroidMentalState
{
    peaceful,
    aggressive
}
public class MouseDroidBehaviour : MonoBehaviour
{
    [SerializeField] public LayerMask enemyLayerMask;
    [SerializeField] Transform sphereOrigin;
    [SerializeField] float enemyScannerRadius;
    [SerializeField] float droidAttackPower;
    [SerializeField] ParticleSystem electroSlashParticle;
    [SerializeField] MouseDroidSFX mouseDroidSFX;
    [SerializeField] ParticleSystem remoteCommandVFX;
    public GameObject targetedEnemy;
    public GameObject playerPrefab;
    private NavMeshAgent agent;
    public DroidMentalState currentDroidState;
    public float distance;
    private bool isCooldownActive = false;

    private float cooldownDuration = 1.0f;

    public void Awake()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("PlayerModel");
    }
    private void Start()
    {
        currentDroidState = DroidMentalState.peaceful;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(playerPrefab.transform.position);
    }

    public void OnEnable()
    {
        RemoteControl.OnPeacefulModeActivated += SetPeacefulMode;
        RemoteControl.OnAggressiveModeActivated += SetAggressiveMode;
    }
    public void OnDisable()
    {
        RemoteControl.OnPeacefulModeActivated -= SetPeacefulMode;
        RemoteControl.OnAggressiveModeActivated -= SetAggressiveMode;
    }
    private void Update()
    {
        if (currentDroidState == DroidMentalState.peaceful)
        {
            agent.SetDestination(playerPrefab.transform.position);
            //Droid is peaceful, returning to Player
        }
        else if (currentDroidState == DroidMentalState.aggressive && targetedEnemy == null)
        {
            ScanForEnemies();
            //Droid is aggressive, looking for Enemies
        }
        else if (targetedEnemy != null)
        {
            AttackEnemy(targetedEnemy);
            agent.SetDestination(targetedEnemy.transform.position);
        }
    }
    public void SetAggressiveMode()
    {
        remoteCommandVFX.Play();
        currentDroidState = DroidMentalState.aggressive;
    }
    public void SetPeacefulMode()
    {
        currentDroidState = DroidMentalState.peaceful;
        electroSlashParticle.Stop();
    }
    public void ScanForEnemies()
    {
        Debug.Log("Scanning Enemies");
        Collider[] hitColliders = Physics.OverlapSphere(sphereOrigin.position, enemyScannerRadius);
        foreach (var hitCollider in hitColliders)
        {
            Enemy hitEnemy = hitCollider.gameObject.GetComponent<Enemy>();
            if (hitCollider.tag == "Enemy" /*&& hitEnemy.targetedState == TargetedState.free*/)
            {
                hitEnemy.SetTargetedState();
                targetedEnemy = hitCollider.gameObject;
                AttackEnemy(targetedEnemy);
                Debug.Log("Target Acquired");
            }
        }
    }
    public void AttackEnemy(GameObject enemy)
    {
        if (!isCooldownActive)
        {
            isCooldownActive = true;
            electroSlashParticle.Play();
            Debug.Log("Attacking Enemy");
            targetedEnemy.GetComponent<Enemy>().TakeDamage(droidAttackPower);
            mouseDroidSFX.PlayAttackSound();
            StartCoroutine("CooldownCoroutine");
        }
        else
        {
            electroSlashParticle.Stop();
            Debug.Log("Cooldown is still active");
        }
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
    private IEnumerator CooldownCoroutine()
    {
     
        yield return new WaitForSeconds(cooldownDuration);
        isCooldownActive = false;
    }
}
