using UnityEngine;
using Cainos.PixelArtTopDown_Basic;

public class AttackPerks : MonoBehaviour
{
    public void addAttackRatio()
    {
        GameObject player = GameObject.FindWithTag("Player");
        TopDownCharacterController playerController = player.GetComponent<TopDownCharacterController>();

        playerController.attackRatio = playerController.attackRatio + (playerController.attackRatio * (float)0.3);
    }
}
