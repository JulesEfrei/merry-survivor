using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text playerHealthText;
    public Text enemiesRemainingText;
    public Text currentRoundText;

    private float playerHealth = 100;

    public void UpdatePlayerHealth(float health)
    {
        playerHealth = health;
        playerHealthText.text = "Health: " + playerHealth;
    }

    public void UpdateEnemiesRemaining(int enemiesRemaining)
    {
        enemiesRemainingText.text = enemiesRemaining + " enemies remaining";
    }

    public void UpdateCurrentRound(int round)
    {
        currentRoundText.text = "Round " + round;
    }
}