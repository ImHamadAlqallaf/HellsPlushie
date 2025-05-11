using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpened;

    private bool isPlayerInRange = false;

    void Update()
    {
        // Check for E key press while player is near
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoorAndPickupKey();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // Optional: show UI prompt like "Press E to pick up key"
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // Optional: hide UI prompt
        }
    }

    void OpenDoorAndPickupKey()
    {
        if (doorClosed != null) doorClosed.SetActive(false);
        if (doorOpened != null) doorOpened.SetActive(true);

        Destroy(gameObject); // Destroy the key
    }
}
