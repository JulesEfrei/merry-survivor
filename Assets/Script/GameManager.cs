using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public int currentRound = 0;

    public void StartNextRound()
    {
        currentRound++;
        Debug.Log("Starting Round: " + currentRound);

        spawnManager.ClearEnemies();
        spawnManager.SpawnEnemies(currentRound);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        // Logique pour finir la partie
    }

    void Update()
    {
        // Vérifier si tous les ennemis sont morts
        if (spawnManager.transform.childCount == 0)
        {
            StartNextRound();
        }
    }
}