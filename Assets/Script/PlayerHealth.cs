using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
public class PlayerHealth : MonoBehaviour
{

    public float baseHealth = 100;
    public float health = 100;
    private UIManager uiManager;
    public Image damageImage;
    public float fadeDuration = 0.5f;
    public float resetDelay = 2f;

    private float currentAlpha = 0f;
    private Coroutine resetCoroutine;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        uiManager.UpdatePlayerHealth(health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }

        uiManager.UpdatePlayerHealth(health);

        if (damageImage != null)
        {
            currentAlpha += 0.1f;
            currentAlpha = Mathf.Clamp(currentAlpha, 0f, 1f);
            damageImage.DOFade(currentAlpha, fadeDuration);

            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
            }
            resetCoroutine = StartCoroutine(ResetOpacityAfterDelay());
        }
        else
        {
            Debug.LogWarning("damageImage n'est pas assignée dans l'inspecteur.");
        }
    }

    private IEnumerator ResetOpacityAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);

        currentAlpha = 0f;
        damageImage.DOFade(currentAlpha, fadeDuration);
    }

    private void Die()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("GameOverScene");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            TakeDamage(enemy.attack);
        }
    }
}
