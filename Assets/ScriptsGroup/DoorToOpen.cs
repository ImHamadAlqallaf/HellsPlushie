using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToOpen : MonoBehaviour
{
    public GameObject doorOpened, doorClosed, intIcon;
    public float openTime;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intIcon.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                intIcon.SetActive(false);
                doorOpened.SetActive(true);
                doorClosed.SetActive(false);
                StartCoroutine(closeDoor());
            }
        }
    }

    IEnumerator closeDoor()
    {
        yield return new WaitForSeconds(openTime);
        doorClosed.SetActive(true);
        doorOpened.SetActive(false);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intIcon.SetActive(false);
        }
    }
}