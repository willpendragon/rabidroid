/*using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player; // Add this line to assign a reference to the player.
    public GameObject enemyPrefab; // Drag and drop your enemy prefab in the Unity editor.
    public float spawnInterval = 5.0f; // Set the time interval between enemy spawns.
    public float spawnRadius = 5.0f; // Set the radius around the spawner where enemies can spawn.

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Modify this line to store the reference of the instantiated enemy.
            spawnedEnemy.GetComponent<Enemy>().playerPrefab = player; // Add this line to pass the reference of the player to the spawned enemy.
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector2 GetRandomSpawnPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle;
        Vector2 spawnPosition = (Vector2)transform.position + (randomDirection * spawnRadius);
        return spawnPosition;
    }
}
*/