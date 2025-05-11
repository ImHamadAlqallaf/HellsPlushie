using UnityEngine;

public class SimpleBoss : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 10f;
    public float moveSpeed = 3f;
    public int bossHealth = 3;

    private bool isChasing = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < chaseDistance)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f; // Prevent tilting up/down
            transform.forward = direction; // Boss faces you

            transform.position += direction * moveSpeed * Time.deltaTime;
        }

    }

    public void TakeDamage(int damage)
    {
        bossHealth -= damage;

        if (bossHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Boss defeated!");
    }
}
