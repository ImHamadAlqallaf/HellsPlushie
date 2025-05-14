using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Position Sway")]
    public float swayAmount = 0.02f;   // how far to move on input
    public float maxSwayAmount = 0.06f;   // clamp the movement
    public float swaySmooth = 4f;     // how quickly to catch up

    private Vector3 _initialPos;

    void Start()
    {
        // record the starting local position
        _initialPos = transform.localPosition;
    }

    void Update()
    {
        // get mouse movement (old Input system)
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // calculate target offset
        float moveX = -mouseX * swayAmount;
        float moveY = -mouseY * swayAmount;

        // clamp to avoid extreme offsets
        moveX = Mathf.Clamp(moveX, -maxSwayAmount, maxSwayAmount);
        moveY = Mathf.Clamp(moveY, -maxSwayAmount, maxSwayAmount);

        Vector3 targetPos = _initialPos + new Vector3(moveX, moveY, 0);

        // smoothly interpolate weapon hand towards that target
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPos,
            Time.deltaTime * swaySmooth
        );
    }
}
