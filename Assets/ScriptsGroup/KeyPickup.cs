using UnityEngine;
using UnityEngine.UI;

public class KeyPickup : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpened;
    public Text interactionText;  // ?? Drag your UI Text here in the Inspector

    private bool isPlayerInRange = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.text = ""; // hide text at start
    }

    void Update()
    {
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

            if (interactionText != null)
                interactionText.text = "Press E to pick up the key";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

            if (interactionText != null)
                interactionText.text = ""; // hide text
        }
    }

    void OpenDoorAndPickupKey()
    {
        if (doorClosed != null) doorClosed.SetActive(false);
        if (doorOpened != null) doorOpened.SetActive(true);

        if (interactionText != null)
            interactionText.text = ""; // hide text on pickup

        Destroy(gameObject); // remove the key
    }
}
