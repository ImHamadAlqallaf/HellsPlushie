using UnityEngine;
using System.Collections;

public class SwordSwing : MonoBehaviour
{
    [Tooltip("Degrees to rotate for the swing arc")]
    public float swingAngle = 70f;
    [Tooltip("Total duration (s) of down+up swing")]
    public float swingDuration = 0.4f;
    [Tooltip("Local axis to swing around")]
    public Vector3 swingAxis = Vector3.forward;

    private bool _isSwinging;
    private Quaternion _startRot;

    void Start()
    {
        // capture the “resting” orientation once
        _startRot = transform.localRotation;
    }

    void OnEnable()
    {
        // if we come back mid-switch, snap back to rest
        transform.localRotation = _startRot;
        _isSwinging = false;
    }

    void OnDisable()
    {
        // ensure no coroutine leaves us in the wrong pose
        transform.localRotation = _startRot;
        _isSwinging = false;
    }

    void Update()
    {
        if (!_isSwinging && Input.GetMouseButtonDown(0))
            StartCoroutine(DoSwing());
    }

    IEnumerator DoSwing()
    {
        _isSwinging = true;
        float half = swingDuration * 0.5f;
        float t = 0f;

        Quaternion downRot = _startRot * Quaternion.AngleAxis(swingAngle, swingAxis);

        // swing down
        while (t < half)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(_startRot, downRot, t / half);
            yield return null;
        }

        // swing back up
        t = 0f;
        while (t < half)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(downRot, _startRot, t / half);
            yield return null;
        }

        transform.localRotation = _startRot;
        _isSwinging = false;
    }
}
