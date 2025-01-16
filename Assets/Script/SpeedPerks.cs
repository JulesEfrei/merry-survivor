using UnityEngine;
using Cainos.PixelArtTopDown_Basic;

public class SpeedPerks : MonoBehaviour
{
    public void addSpeed()
    {
        GameObject player = GameObject.FindWithTag("Player");
        TopDownCharacterController playerController = player.GetComponent<TopDownCharacterController>();

        playerController.speed = playerController.speed + (playerController.speed * (float)0.2);
    }
}
