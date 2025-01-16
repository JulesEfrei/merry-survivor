using UnityEngine;
using Cainos.PixelArtTopDown_Basic;

public class AttackSpeedPerks : MonoBehaviour
{
    public void addAttackSpeed()
    {
        GameObject player = GameObject.FindWithTag("Player");
        TopDownCharacterController playerController = player.GetComponent<TopDownCharacterController>();

        playerController.fireCooldown = playerController.fireCooldown - (playerController.fireCooldown * (float)0.2);
    }
}
