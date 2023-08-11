using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public enum WeaponState
    {
        equipped,
        inactive
    }
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] GameObject raycastOrigin;
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 direction;
    [SerializeField] public float maxDistance;
    [SerializeField] float weaponDamage;
    [SerializeField] Animator swordAnimator;
    [SerializeField] float playerAttackPower;
    [SerializeField] ParticleSystem attackVFX;
    [SerializeField] Rigidbody playerRigidbody;
    [SerializeField] float weaponResourceCost = 2f;
    private GameManager gameManager;
    private UIManager uIManager;
    public WeaponState weaponCurrentState;

    // Start is called before the first frame update
    void Start()
    {
        weaponCurrentState = WeaponState.inactive;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        uIManager = GameObject.FindGameObjectWithTag("GameUI").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("attack") && weaponCurrentState == WeaponState.inactive)
        {
            DrawWeapon();
            Debug.Log("Drawing weapon");
        }
        else if (Input.GetButtonDown("attack") && weaponCurrentState == WeaponState.equipped)
        {
            UseWeapon();
        }
    }
    void DrawWeapon()
    {
        weaponPrefab.SetActive(true);
        weaponCurrentState = WeaponState.equipped;
    }
    void UseWeapon()
    {
        Debug.Log("Using weapon");
        startPosition = raycastOrigin.transform.position;
        direction = transform.forward;
        attackVFX.Play();
        RaycastHit hit;
        int weaponResourceCostInt = Mathf.RoundToInt(weaponResourceCost);
        gameManager.rockUnits -= weaponResourceCostInt;
        uIManager.UpdateRockUnitsCounter();
        Debug.DrawRay(startPosition, direction, Color.red, maxDistance);
        if (Physics.Raycast(startPosition, direction, out hit, maxDistance) && hit.collider.gameObject.layer == 9)
        {

        }
        else if (Physics.Raycast(startPosition, direction, out hit, maxDistance) && hit.collider.gameObject.layer == 3)
        {
            Debug.Log("Hit Enemy" + hit.collider.gameObject.name);
            hit.collider.gameObject.GetComponentInParent<Enemy>().TakeDamage(playerAttackPower);
            //hit.collider.gameObject.GetComponent<Enemy>().health -= weaponDamage;
            swordAnimator.SetBool("swordAttack", true);
            StartCoroutine("AttackCooldown");
            Debug.Log("Hit Enemy");
        }
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1F);
        swordAnimator.SetBool("swordAttack", false);
    }
}
