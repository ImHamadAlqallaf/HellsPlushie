using UnityEngine;

public class BossSimpleAI : MonoBehaviour
{
    public Animator animator;
    public GameObject[] projectilePrefabs;
    public Transform throwPoint;
    public float throwInterval = 2f;
    public float attackRange = 10f;
    public int health = 100;
    public float rotationSpeed = 6.5f; // Added rotation speed control

    private Transform player;
    private float throwTimer = 0f;
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (health <= 0) return;

        float distance = Vector3.Distance(player.position, transform.position);

        // Always face the player when in range
        if (distance <= attackRange * 1.5f) // Slightly larger range for rotation
        {
            FacePlayer();
        }

        if (!isAttacking && distance <= attackRange)
        {
            isAttacking = true;
        }

        if (isAttacking)
        {
            throwTimer += Time.deltaTime;
            if (throwTimer >= throwInterval)
            {
                ThrowObject();
                throwTimer = 0f;
            }
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Keep the boss upright (remove if you want vertical rotation)
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    void ThrowObject()
    {
        int index = Random.Range(0, projectilePrefabs.Length);
        GameObject obj = Instantiate(projectilePrefabs[index], throwPoint.position, Quaternion.identity);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 dir = (player.position - throwPoint.position).normalized;
            rb.velocity = dir * 20f;
        }
    }

    public void TakeDamage(int amount)
    {
        if (health <= 0) return;

        health -= amount;
        if (health <= 0)
        {
            animator.SetTrigger("Fall");
        }
    }
}