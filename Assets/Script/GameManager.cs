using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public UIManager uiManager;
    public int currentRound = 0;
    public float delayBetweenRounds = 5f;

    private bool roundInProgress = false;

    void Start()
    {
        uiManager.UpdatePlayerHealth(100);
        StartNextRound();
    }

    void Update()
    {
        if (roundInProgress && spawnManager.ActiveEnemiesCount() == 0)
        {
            roundInProgress = false;
            StartCoroutine(StartNextRoundWithDelay());
        }

        uiManager.UpdateEnemiesRemaining(spawnManager.ActiveEnemiesCount());
    }

    private IEnumerator StartNextRoundWithDelay()
    {
        Debug.Log("Round completed! Waiting for next round...");
        yield return new WaitForSeconds(delayBetweenRounds);
        StartNextRound();
    }

    public void StartNextRound()
    {
        currentRound++;
        uiManager.UpdateCurrentRound(currentRound);
        Debug.Log("Starting Round: " + currentRound);

        spawnManager.SpawnEnemies(currentRound);
        roundInProgress = true;
    }
}