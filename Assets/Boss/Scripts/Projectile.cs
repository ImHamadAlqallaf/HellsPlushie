using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;  // <--- This line is essential!

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Boss hit!");
            collision.gameObject.GetComponent<BossAI>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
