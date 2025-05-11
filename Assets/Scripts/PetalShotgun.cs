using UnityEngine;

public class PetalShotgun : MonoBehaviour
{
    [Header("References")]
    public Camera fpsCamera;     // your MainCamera
    public Transform muzzlePoint;   // at barrel tip
    public ParticleSystem muzzleFlash;  // optional
    public LineRenderer tracerPrefab;  // optional per-pellet tracer
    public GameObject impactEffect;  // optional
    public GunRecoil recoil;        // optional

    [Header("Settings")]
    public int pelletCount = 8;       // # of pellets
    [Tooltip("cone spread in degrees")]
    public float spreadAngle = 10f;
    public float range = 60f;
    public float damagePerPellet = 12f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ShootSpread();
    }

    void ShootSpread()
    {
        // 1) muzzle flash
        if (muzzleFlash != null)
            muzzleFlash.Play();

        // 2) recoil kick
        if (recoil != null)
            recoil.FireRecoil();

        Vector3 origin = fpsCamera.transform.position;

        // 3) fire each pellet
        for (int i = 0; i < pelletCount; i++)
        {
            // randomize direction within cone
            Vector3 dir = fpsCamera.transform.forward;
            float yaw = Random.Range(-spreadAngle, spreadAngle);
            float pitch = Random.Range(-spreadAngle, spreadAngle);
            dir = Quaternion.Euler(pitch, yaw, 0f) * dir;

            // raycast
            RaycastHit hit;
            Vector3 endPoint = origin + dir * range;
            if (Physics.Raycast(origin, dir, out hit, range))
            {
                endPoint = hit.point;

                // 3a) apply damage
                var dmg = hit.collider.GetComponent<IDamageable>();
                if (dmg != null)
                    dmg.TakeDamage(damagePerPellet);

                // 3b) impact VFX
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

            // 4) tracer
            if (tracerPrefab != null && muzzlePoint != null)
            {
                var tracer = Instantiate(tracerPrefab, muzzlePoint.position, Quaternion.identity);
                tracer.SetPosition(0, muzzlePoint.position);
                tracer.SetPosition(1, endPoint);
                Destroy(tracer.gameObject, 0.05f);
            }
        }
    }
}
