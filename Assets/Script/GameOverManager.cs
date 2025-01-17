using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text roundsText;
    private void Start()
    {
        //int roundsPlayed = PlayerPrefs.GetInt("RoundsPlayed", 0);
        //roundsText.text = "Rounds Played: " + roundsPlayed;
    }

    public void RetryGame()
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