using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Transform doorPivot; // Assign in Inspector
    public Vector3 openRotation = new Vector3(0, -90, 0);
    public float speed = 2f;

    private bool isOpening = false;
    private Quaternion startRotation;
    private Quaternion targetRotation;

    void Start()
    {
        startRotation = doorPivot.localRotation;
        targetRotation = Quaternion.Euler(openRotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Door Triggered!");
            isOpening = true;
        }
    }

    void Update()
    {
        if (isOpening)
        {
            doorPivot.localRotation = Quaternion.Lerp(
                doorPivot.localRotation,
                targetRotation,
                Time.deltaTime * speed
            );
        }
    }
}
