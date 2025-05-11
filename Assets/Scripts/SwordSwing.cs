using UnityEngine;
using System.Collections;

public class SwordSwing : MonoBehaviour
{
    [Tooltip("How many degrees to swing along the chosen axis")]
    public float swingAngle = 70f;
    [Tooltip("Total time (in seconds) for swing down + return up")]
    public float swingDuration = 0.4f;
    [Tooltip("Local axis to swing around")]
    public Vector3 swingAxis = Vector3.up;


    private bool _isSwinging;
    private Quaternion _startRot;

    void Start()
    {
        // record the “rest” orientation
        _startRot = transform.localRotation;
    }

    void Update()
    {
        if (!gameObject.activeInHierarchy) return;

        // left?click to swing
        if (Input.GetMouseButtonDown(0) && !_isSwinging)
            StartCoroutine(DoSwing());
    }

    IEnumerator DoSwing()
    {
        _isSwinging = true;
        float half = swingDuration * 0.5f;
        float t = 0f;

        // rotation downwards by swingAngle about swingAxis
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
