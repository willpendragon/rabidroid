using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
public GameObject enemyPrefab; // Reference to the enemy prefab to spawn
public Transform spawnPoint; // Reference to the spawn point
public float spawnInterval = 2f; // Time interval between spawns

void Start()
{
    SpawnEnemy();
    InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
}

void SpawnEnemy()
{
    Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
}
}
