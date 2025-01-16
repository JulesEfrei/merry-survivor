using UnityEngine;

public class MaxHealthPerks : MonoBehaviour
{
    public void maxHealth()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerHealth playerController = player.GetComponent<PlayerHealth>();

        playerController.baseHealth = playerController.baseHealth + (float)10;
        playerController.health = playerController.baseHealth;
    }
}
