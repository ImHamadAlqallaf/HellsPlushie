using UnityEngine;
using UnityEngine.UI; // If using UnityEngine.UI.Text
// using TMPro; // If using TextMeshProUGUI

public class BookPickUp : MonoBehaviour
{
    [Header("Door GameObjects")]
    public GameObject doorClosed;
    public GameObject doorOpened;

    [Header("UI Prompt")]
    public GameObject interactionTextUI; // This is a GameObject that holds the text

    private bool isPlayerInRange = false;

    void Start()
    {
        if (interactionTextUI != null)
        {
            interactionTextUI.SetActive(false); // Hide on start
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoorAndPickupBook();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionTextUI != null)
                interactionTextUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactionTextUI != null)
                interactionTextUI.SetActive(false);
        }
    }

    void OpenDoorAndPickupBook()
    {
        if (doorClosed != null)
            doorClosed.SetActive(false);

        if (doorOpened != null)
            doorOpened.SetActive(true);

        if (interactionTextUI != null)
            interactionTextUI.SetActive(false);

        Destroy(gameObject);
    }
}
