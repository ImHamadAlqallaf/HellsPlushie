using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1.5f;
    public float smoothing = 1.5f;

    private Vector2 smoothedMouse;
    private Vector2 currentLookingPos;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 rawInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        rawInput *= sensitivity * smoothing;

        smoothedMouse = Vector2.Lerp(smoothedMouse, rawInput, 1f / smoothing);
        currentLookingPos += smoothedMouse;

        
        currentLookingPos.y = Mathf.Clamp(currentLookingPos.y, -90f, 90f);

   
        transform.localRotation = Quaternion.Euler(-currentLookingPos.y, currentLookingPos.x, 0f);
    }
}
