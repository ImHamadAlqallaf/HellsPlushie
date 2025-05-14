using UnityEngine;
using StarterAssets;  // for the StarterAssetsInputs

[RequireComponent(typeof(StarterAssetsInputs))]
public class HeadAndWeaponBob : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The parent transform of your camera (e.g. PlayerCameraRoot)")]
    public Transform cameraRoot;
    [Tooltip("The transform you use to hold your weapon (e.g. WeaponHand)")]
    public Transform weaponRoot;

    [Header("Walking Bob")]
    public float walkFrequency = 1.5f;   // cycles per second
    public float walkAmplitude = 0.05f;  // vertical + horizontal offset

    [Header("Sprinting Bob")]
    public float sprintFrequency = 2.5f;
    public float sprintAmplitude = 0.1f;

    [Header("Smoothing")]
    [Tooltip("How fast to lerp back to rest when you stop")]
    public float returnSpeed = 5f;

    private Vector3 _camStartPos;
    private Vector3 _weapStartPos;
    private float _timer;
    private StarterAssetsInputs _input;

    void Start()
    {
        // cache starts
        if (cameraRoot != null) _camStartPos = cameraRoot.localPosition;
        if (weaponRoot != null) _weapStartPos = weaponRoot.localPosition;
        _input = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        // movement magnitude (0..1)
        float speed = new Vector2(_input.move.x, _input.move.y).magnitude;

        if (speed > 0.1f)
        {
            // pick walk vs sprint bob
            bool sprint = _input.sprint;
            float freq = sprint ? sprintFrequency : walkFrequency;
            float amp = sprint ? sprintAmplitude : walkAmplitude;

            _timer += Time.deltaTime * freq;
            float horizontal = Mathf.Sin(_timer) * amp;
            float vertical = Mathf.Cos(_timer * 2f) * amp;

            // apply to camera
            if (cameraRoot != null)
                cameraRoot.localPosition = _camStartPos + new Vector3(horizontal, vertical, 0);

            // apply to weapon
            if (weaponRoot != null)
                weaponRoot.localPosition = _weapStartPos + new Vector3(horizontal, vertical, 0);
        }
        else
        {
            // reset timer & smoothly return
            _timer = 0f;
            if (cameraRoot != null)
                cameraRoot.localPosition = Vector3.Lerp(
                    cameraRoot.localPosition, _camStartPos, Time.deltaTime * returnSpeed);
            if (weaponRoot != null)
                weaponRoot.localPosition = Vector3.Lerp(
                    weaponRoot.localPosition, _weapStartPos, Time.deltaTime * returnSpeed);
        }
    }
}
