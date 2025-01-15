using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartTheGame()
    {
        print("Test");
        SceneManager.LoadScene("GameScene");
    }
}

