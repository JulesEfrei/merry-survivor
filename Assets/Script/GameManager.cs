using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public UIManager uiManager;

    public GameObject bonusSelectionPanel;
    public Transform buttonParent;
    public Text countdownText;

    public GameObject[] bonusPrefabs;

    public int currentRound = 0;
    public float selectionDuration = 15f;

    private bool roundInProgress = false;
    private bool selectionMade = false;

    void Start()
    {
        uiManager.UpdatePlayerHealth(100);
        StartNextRound();
    }

    void Update()
    {
        if (roundInProgress && spawnManager.ActiveEnemiesCount() == 0)
        {
            roundInProgress = false;
            StartCoroutine(DisplayBonusSelection());
        }

        uiManager.UpdateEnemiesRemaining(spawnManager.ActiveEnemiesCount());
    }

    private IEnumerator DisplayBonusSelection()
    {
        bonusSelectionPanel.SetActive(true);
        GenerateBonusButtons();

        float timer = selectionDuration;
        selectionMade = false;

        while (timer > 0 && !selectionMade)
        {
            countdownText.text = "Time remaining: " + Mathf.Ceil(timer).ToString() + "s";
            timer -= Time.deltaTime;
            yield return null;
        }

        bonusSelectionPanel.SetActive(false);

        if (!selectionMade)
        {
            Debug.Log("Time expired, starting next round automatically.");
            StartNextRound();
        }

    }

    private void GenerateBonusButtons()
    {
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, bonusPrefabs.Length);
            GameObject bonusButton = Instantiate(bonusPrefabs[randomIndex], buttonParent);

            Button button = bonusButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnBonusSelected(bonusButton));
            }
        }
    }

    private void OnBonusSelected(GameObject bonus)
    {
        Debug.Log("Bonus selected: " + bonus.name);

        selectionMade = true;

        bonusSelectionPanel.SetActive(false);

        StartNextRound();
    }

    public void StartNextRound()
    {
        currentRound++;
        uiManager.UpdateCurrentRound(currentRound);
        uiManager.UpdatePlayerHealth(GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().health);
        Debug.Log("Starting Round: " + currentRound);

        spawnManager.SpawnEnemies(currentRound);
        roundInProgress = true;
    }
}