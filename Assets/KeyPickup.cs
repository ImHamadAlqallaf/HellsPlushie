using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public bool isRedKey, isBlueKey, isGreenKey, isBlackKey;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (isRedKey)
            {
                inventory.hasRed = true;
            }
            if (isBlueKey)
            {
                inventory.hasBlue = true;
            }
            if (isGreenKey)
            {
                inventory.hasGreen = true;
            }
            if (isBlackKey)
            {
                inventory.hasBlack = true;
            }

            Destroy(transform.root.gameObject); // Destroys the entire key structure
        }
    }
}
