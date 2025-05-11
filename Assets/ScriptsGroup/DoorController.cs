using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject doorClosed;    // Reference to Door_Closed
    public GameObject doorOpen;       // Reference to Door_Open
    public GameObject lockedText;     // Reference to LockedText
    public GameObject unlockedText;   // Reference to UnlockedText

    private bool isUnlocked = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (isUnlocked)
            {
                OpenDoor();
            }
            else
            {
                if (lockedText != null)
                    lockedText.SetActive(true);
            }
        }
    }

    public void UnlockDoor()
    {
        isUnlocked = true;
        if (lockedText != null)
            lockedText.SetActive(false);
        if (unlockedText != null)
            unlockedText.SetActive(true);
    }

    private void OpenDoor()
    {
        if (doorClosed != null)
            doorClosed.SetActive(false);
        if (doorOpen != null)
            doorOpen.SetActive(true);

        if (lockedText != null)
            lockedText.SetActive(false);
        if (unlockedText != null)
            unlockedText.SetActive(false);
    }
}
