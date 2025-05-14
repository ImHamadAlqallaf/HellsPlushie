using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;
    public float aimDistance = 100f;

    private float cameraPitch = 0f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        LookAround();
        Aim();
    }

    Vector3 velocity = Vector3.zero;
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        velocity.y += Physics.gravity.y * Time.deltaTime; // Apply gravity

        controller.Move((move * speed + velocity) * Time.deltaTime);
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * mouseX);
    }

    void Aim()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, aimDistance))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            Debug.Log("Aiming at: " + hit.collider.name);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * aimDistance, Color.green);
        }
    }
}
