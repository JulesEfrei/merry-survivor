using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

