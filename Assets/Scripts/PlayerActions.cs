using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField][Range(0.1f, 1f)] private float jumpSensitivity = 0.5f; // Lower = more sensitive

    [Header("Look")]
    [SerializeField] private float mouseSensitivity = 150f;
    [SerializeField] private Camera playerCamera;

    private Rigidbody rb;
    private float xRotation;
    private bool isGrounded;
    private float lastJumpPressTime;
    private const float jumpBufferTime = 0.2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (!playerCamera) playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Jump Buffer System
        if (Input.GetButtonDown("Jump"))
            lastJumpPressTime = Time.time;
    }

    void FixedUpdate()
    {
        // Ground Check (adjusted for tall character)
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f,
                                    Vector3.down,
                                    0.2f); // Short raycast

        // WASD Movement
        Vector3 move = transform.right * Input.GetAxis("Horizontal") +
                       transform.forward * Input.GetAxis("Vertical");
        rb.velocity = new Vector3(move.x * moveSpeed, rb.velocity.y, move.z * moveSpeed);

        // Jump with sensitivity control
        if ((Time.time - lastJumpPressTime < jumpBufferTime * jumpSensitivity) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            lastJumpPressTime = 0; // Consume the jump input
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize ground check
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f,
                        transform.position + Vector3.up * 0.1f + Vector3.down * 0.2f);
    }
}