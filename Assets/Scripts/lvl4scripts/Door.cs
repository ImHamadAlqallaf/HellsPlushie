using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnim;
    public bool requiresKey;
    public bool reqRed, reqBlue, reqGreen, reqBlack;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var inventory = other.GetComponent<PlayerInventory>();

            if (requiresKey)
            {
                if (reqRed && inventory.hasRed)
                    doorAnim.SetTrigger("OpenDoor");
                if (reqBlue && inventory.hasBlue)
                    doorAnim.SetTrigger("OpenDoor");
                if (reqGreen && inventory.hasGreen)
                    doorAnim.SetTrigger("OpenDoor");
                if (reqBlack && inventory.hasBlack)
                    doorAnim.SetTrigger("OpenDoor");
            }
            else
            {
                doorAnim.SetTrigger("OpenDoor");
            }
        }
    }
}
