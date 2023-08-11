using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Vector3 spawnPoint;
    public void SpawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
    }
}
