using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text playerHealthText;
    public Text enemiesRemainingText;
    public Text currentRoundText;


    public void UpdatePlayerHealth(float health)
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerHealth playerController = player.GetComponent<PlayerHealth>();

        playerHealthText.text = health + "/" + playerController.baseHealth;
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