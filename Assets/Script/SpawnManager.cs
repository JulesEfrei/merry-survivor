using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Types d'ennemis
    public Transform[] spawnPoints;  // Points de spawn
    public int enemiesPerRound = 5;  // Nombre d'ennemis par manche

    private List<GameObject> activeEnemies = new List<GameObject>();

    public int baseEnemyHealth = 1;
    public float baseEnemySpeed = 2f;

    public void SpawnEnemies(int round)
    {
        int enemyCount = enemiesPerRound + round;
        int enemyHealth = baseEnemyHealth + round;
        float enemySpeed = baseEnemySpeed + round * 0.5f;

        for (int i = 0; i < enemyCount; i++)
        {
            // Choisir un type d'ennemi aléatoire
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Choisir un point de spawn aléatoire
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instancier l'ennemi
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            SpriteRenderer enemyRenderer = newEnemy.GetComponent<SpriteRenderer>();
            if (enemyRenderer != null)
            {
                enemyRenderer.sortingLayerName = LayerMask.LayerToName(spawnPoint.gameObject.layer);
            }

            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
            enemyController.health = enemyHealth;
            enemyController.speed = enemySpeed;

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