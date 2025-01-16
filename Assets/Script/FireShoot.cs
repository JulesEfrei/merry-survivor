using UnityEngine;
using Cainos.PixelArtTopDown_Basic;

public class FireballShot : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public float baseAttack = 2;
    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            GameObject player = GameObject.FindWithTag("Player");
            float playerAttack = player.GetComponent<TopDownCharacterController>().attackRatio;

            float enemyHealth = enemy.health;
            float damage = this.baseAttack * playerAttack;

            if (enemyHealth - damage <= 0)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                enemy.health -= damage;
            }

        }
        else if (collision.gameObject.CompareTag("Player"))
        {

            //Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}