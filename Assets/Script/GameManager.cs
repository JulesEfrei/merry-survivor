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
        // Activer le panel de sélection
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

        // Cacher le panel de sélection
        bonusSelectionPanel.SetActive(false);

        if (!selectionMade)
        {
            Debug.Log("Time expired, starting next round automatically.");
        }

        StartNextRound();
    }

    private void GenerateBonusButtons()
    {
        // Supprimer les anciens boutons
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        // Générer 3 boutons aléatoires
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, bonusPrefabs.Length);
            GameObject bonusButton = Instantiate(bonusPrefabs[randomIndex], buttonParent);

            // Ajouter un listener pour le bouton
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

        // Appliquer l'effet du bonus ici (par exemple, augmenter les stats du joueur)
        ApplyBonusEffect(bonus);

        selectionMade = true;

        // Cacher le panel de sélection
        bonusSelectionPanel.SetActive(false);

        // Lancer la prochaine manche immédiatement
        StartNextRound();
    }

    private void ApplyBonusEffect(GameObject bonus)
    {
        // Implémentez ici les effets des bonus
        // Exemple : augmenter la santé, les dégâts, etc.
    }

    public void StartNextRound()
    {
        currentRound++;
        uiManager.UpdateCurrentRound(currentRound);
        Debug.Log("Starting Round: " + currentRound);

        spawnManager.SpawnEnemies(currentRound);
        roundInProgress = true;
    }
}