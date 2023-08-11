using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlarm : MonoBehaviour
{

[SerializeField] Enemy[] enemiesInRoom;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerModel")
        {
            Debug.Log("Triggered by Player");
            foreach (Enemy enemy in enemiesInRoom)
            {
                enemy.SwitchToAlertMode();
            }
        }
    }
}
