using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHP;
    public void TakeDamage(float attackDamage)
    {
        Debug.Log("Damage");
        playerHP -= attackDamage;
    }
    public void UpdatePlayerHPBar()
    {
        UIManager uiManager = GameObject.FindGameObjectWithTag("GameUI").GetComponent<UIManager>();
        uiManager.UpdatePlayerHPBar();
    }
}
