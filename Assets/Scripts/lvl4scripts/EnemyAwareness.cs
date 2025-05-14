using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public GameObject bearClosed;
    public GameObject bearOpenMouth;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");
            bearClosed.SetActive(false);
            bearOpenMouth.SetActive(true);
        }
    }
}
