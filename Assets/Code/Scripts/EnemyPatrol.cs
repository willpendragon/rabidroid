using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float aggroDistance = 5f;
    public float minDistance = 0.5f;
    private GameObject playerPrefab;
    public float speed = 3f;
    public float attackDistance = 0.5f;

void Awake()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
    }
   void Update()
    {
       Vector3 direction = (playerPrefab.transform.position - this.gameObject.transform.position).normalized;
       float distance = Vector3.Distance(this.gameObject.transform.position, playerPrefab.transform.position);
       if (distance < aggroDistance && distance > minDistance)
       {
            this.gameObject.transform.position += direction * speed * Time.deltaTime;
       }
       if (distance <= attackDistance)
       {
        Debug.Log("Attack");
       }
    }
}