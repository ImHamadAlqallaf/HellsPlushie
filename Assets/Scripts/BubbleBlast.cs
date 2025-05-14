using UnityEngine;

public class BubbleBlast : MonoBehaviour
{
    [Header("References")]
    public Camera fpsCamera;     // drag in your MainCamera
    public Transform muzzlePoint;   // empty at barrel tip
    public ParticleSystem muzzleFlash;  // optional
    public LineRenderer tracerPrefab;  // optional
    public GameObject impactEffect;  // optional
    public GunRecoil recoil;        // optional recoil component

    [Header("Settings")]
    public float range = 100f;         // ray length
    public float damage = 25f;          // damage per hit

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    void Shoot()
    {
        // 1) muzzle flash
        if (muzzleFlash != null)
            muzzleFlash.Play();

        // 2) raycast
        RaycastHit hit;
        Vector3 origin = fpsCamera.transform.position;
        Vector3 direction = fpsCamera.transform.forward;
        Vector3 endPoint = origin + direction * range;

        if (Physics.Raycast(origin, direction, out hit, range))
        {
            endPoint = hit.point;

            // 2a) damage any IDamageable we hit
            var dmg = hit.collider.GetComponent<IDamageable>();
            if (dmg != null)
                dmg.TakeDamage(damage);

            // 2b) optional impact VFX
            if (impactEffect != null)
            {
                var fx = Instantiate(
                    impactEffect,
                    hit.point,
                    Quaternion.LookRotation(hit.normal)
                );
                Destroy(fx, 2f);
            }
        }

        // 3) tracer line
        if (tracerPrefab != null && muzzlePoint != null)
        {
            var tracer = Instantiate(tracerPrefab, muzzlePoint.position, Quaternion.identity);
            tracer.SetPosition(0, muzzlePoint.position);
            tracer.SetPosition(1, endPoint);
            Destroy(tracer.gameObject, 0.05f);
        }

        // 4) recoil kick
        if (recoil != null)
            recoil.FireRecoil();
    }
}
