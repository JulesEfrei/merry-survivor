using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;

    public int health = 1;
    public float speed = 5f;
    public float attack = 5;
    public bool isFlying = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        agent.speed = speed;

        if (player != null)
        {
            if (isFlying == false)
            {
                agent.SetDestination(player.transform.position);
            }
            else
            {
                Vector3 direction = (player.transform.position - this.gameObject.transform.position).normalized;
                this.gameObject.transform.position += direction * speed * Time.deltaTime;
            }
        }
        else
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}