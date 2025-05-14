using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnim;
    public bool requiresKey;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var inventory = other.GetComponent<PlayerInventory>();

            if (!requiresKey || inventory.hasKey)
            {
                doorAnim.SetTrigger("OpenDoor");
            }
        }
    }
}
