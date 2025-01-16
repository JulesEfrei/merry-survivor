using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public int enemiesPerRound = 5;

    private List<GameObject> activeEnemies = new List<GameObject>();

    public void SpawnEnemies(int round)
    {
        int enemyCount = enemiesPerRound + round;

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            SpriteRenderer enemyRenderer = newEnemy.GetComponent<SpriteRenderer>();
            if (enemyRenderer != null)
            {
                enemyRenderer.sortingLayerName = LayerMask.LayerToName(spawnPoint.gameObject.layer);
            }

            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();

            float enemyHealth = enemyController.health + round;
            float enemySpeed = enemyController.speed + round * 0.5f;
            float enemyAttack = enemyController.attack + round * 0.5f;

            enemyController.health = enemyHealth;
            enemyController.speed = enemySpeed;
            enemyController.attack = enemyAttack;

            activeEnemies.Add(newEnemy);
        }
    }

    public void ClearEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null) Destroy(enemy);
        }
        activeEnemies.Clear();
    }

    public int ActiveEnemiesCount()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);
        return activeEnemies.Count;
    }
}