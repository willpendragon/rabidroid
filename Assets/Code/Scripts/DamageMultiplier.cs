using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMultiplier : MonoBehaviour
{
private bool multiplierIsActive;
void OnTriggerEnter2D (Collider2D other)
{
    
    if (multiplierIsActive == false)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SwordAttack>().MultiplyDamage();
        StartCoroutine("MultiplierCooldown");
        multiplierIsActive = true;
    }
}
    IEnumerator MultiplierCooldown()
    {
        yield return new WaitForSeconds(3f);
        multiplierIsActive = false;
    }
}
