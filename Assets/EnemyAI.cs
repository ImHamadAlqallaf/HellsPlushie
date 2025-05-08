using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private bool isChasing = false;

    public float chaseDelay = 1.5f; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isChasing && player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartChaseWithDelay());
        }
    }

    IEnumerator StartChaseWithDelay()
    {
        yield return new WaitForSeconds(chaseDelay);
        isChasing = true;
    }
}
