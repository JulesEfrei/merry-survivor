using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{

    public float health = 100;
    private UIManager uiManager;
    public Image damageImage;
    public float fadeDuration = 0.5f;

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

        // Affiche l'image de dégâts
        damageImage.DOFade(1, fadeDuration).OnComplete(() =>
        {
            damageImage.DOFade(0, fadeDuration);
        });
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
