using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public int maxHealth = 100;

    [Header("Movement Settings")]
    public float moveSpeed = 6f;         // Speed while chasing
    public float acceleration = 10f;     // How quickly the boss speeds up
    public float turnSpeed = 500f;       // Turning speed while chasing

    private Animator animator;
    private NavMeshAgent agent;
    private int currentHealth;
    private bool isDead = false;
    private bool hasWokenUp = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Set movement values
        agent.speed = moveSpeed;
        agent.acceleration = acceleration;
        agent.angularSpeed = turnSpeed;

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < chaseRange && !hasWokenUp)
        {
            animator.SetTrigger("WakeUp");
            hasWokenUp = true;
        }

        if (hasWokenUp)
        {
            if (distance > attackRange)
            {
                animator.SetBool("IsChasing", true);
                animator.SetBool("IsAttacking", false);
                agent.SetDestination(player.position);
            }
            else
            {
                animator.SetBool("IsChasing", false);
                animator.SetBool("IsAttacking", true);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
        agent.enabled = false;
    }
}
