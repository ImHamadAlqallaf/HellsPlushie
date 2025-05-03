using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 12f;

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
        Cursor.visible = false;
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

        // Jump Input Buffer
        if (Input.GetButtonDown("Jump"))
            lastJumpPressTime = Time.time;
    }

    void FixedUpdate()
    {
        // Ground Check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // Movement
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        Vector3 targetVelocity = move.normalized * moveSpeed;
        Vector3 currentVelocity = rb.velocity;
        Vector3 velocityChange = targetVelocity - new Vector3(currentVelocity.x, 0f, currentVelocity.z);

        if (isGrounded)
        {
            rb.drag = 5f;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        else
        {
            rb.drag = 0f;
            rb.AddForce(velocityChange * 0.1f, ForceMode.VelocityChange);
        }

        // Jump
        if ((Time.time - lastJumpPressTime < jumpBufferTime) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            lastJumpPressTime = 0f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1.1f);
    }
}
