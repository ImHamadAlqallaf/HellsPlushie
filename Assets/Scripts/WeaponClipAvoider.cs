using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class WeaponClipAvoider : MonoBehaviour
{
    [Tooltip("Parent transform containing all your weapon models")]
    public Transform weaponsParent;

    [Tooltip("Radius of the spherecast to detect obstacles")]
    public float sphereRadius = 0.1f;

    [Tooltip("How quickly the weapon returns to its rest position")]
    public float returnSpeed = 15f;

    Camera _cam;
    List<Transform> _weapons = new List<Transform>();
    Dictionary<Transform, Vector3> _restPositions = new Dictionary<Transform, Vector3>();

    void Start()
    {
        _cam = GetComponent<Camera>();

        // Cache every direct child of weaponsParent and record its local rest position
        foreach (Transform w in weaponsParent)
        {
            _weapons.Add(w);
            _restPositions[w] = w.localPosition;
        }
    }

    void LateUpdate()
    {
        // Find the one weapon that's currently active
        Transform activeWeapon = _weapons.Find(w => w.gameObject.activeInHierarchy);
        if (activeWeapon == null) return;

        // World-space start (camera) and target (where the weapon normally sits)
        Vector3 camPos = _cam.transform.position;
        Vector3 desiredWorld = _cam.transform.TransformPoint(_restPositions[activeWeapon]);
        Vector3 dir = (desiredWorld - camPos).normalized;
        float maxDist = Vector3.Distance(camPos, desiredWorld);

        // Sphere?cast from camera toward the desired weapon position
        if (Physics.SphereCast(camPos, sphereRadius, dir, out var hit, maxDist))
        {
            // If something blocks the path, place the weapon just in front of it
            Vector3 pushPos = hit.point + hit.normal * sphereRadius;
            activeWeapon.position = pushPos;
        }
        else
        {
            // Otherwise smoothly interpolate it back to its normal local position
            Vector3 restLocal = _restPositions[activeWeapon];
            activeWeapon.localPosition = Vector3.Lerp(
                activeWeapon.localPosition,
                restLocal,
                Time.deltaTime * returnSpeed
            );
        }
    }
}
