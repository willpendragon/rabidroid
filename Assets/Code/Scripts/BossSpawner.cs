using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab;
    [SerializeField] Vector3 spawnPoint;
    
    void Awake()
    {
        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        Instantiate(bossPrefab, spawnPoint, Quaternion.identity);
    }
}
