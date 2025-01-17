using UnityEngine;

public class HealPerks : MonoBehaviour
{
    public void heal()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerHealth playerController = player.GetComponent<PlayerHealth>();

        playerController.health = playerController.baseHealth + (playerController.baseHealth * (float)0.4);
    }
}
