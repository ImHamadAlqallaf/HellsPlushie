using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GunRecoil))]
public class PetalShotgun : MonoBehaviour
{
    [Header("References")]
    public Camera fpsCamera;            // assign your MainCamera
    public Transform muzzlePoint;       // your MuzzlePoint on the shotgun
    public ParticleSystem muzzleFlash;  // optional muzzle?flash PS
    public LineRenderer tracerPrefab;   // optional tracer prefab
    public GameObject impactEffect;     // optional impact prefab
    public GunRecoil recoil;            // your GunRecoil component

    [Header("Shotgun Settings")]
    public int pelletCount = 8;      // number of rays/pellets per shot
    public float spreadAngle = 12f;    // max degrees off?center
    public float range = 50f;    // how far each pellet goes

    void Awake()
    {
        // auto?assign recoil if you forgot
        if (recoil == null)
            recoil = GetComponent<GunRecoil>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ShootSpread();
    }

    void ShootSpread()
    {
        // 1) Muzzle flash
        if (muzzleFlash != null)
            muzzleFlash.Play();

        // 2) Recoil
        recoil?.FireRecoil();

        // 3) Fire each pellet
        for (int i = 0; i < pelletCount; i++)
        {
            // random spread within a cone
            Vector3 dir = fpsCamera.transform.forward;
            float yaw = Random.Range(-spreadAngle, spreadAngle);
            float pitch = Random.Range(-spreadAngle, spreadAngle);
            dir = Quaternion.Euler(pitch, yaw, 0) * dir;

            // raycast
            RaycastHit hit;
            Vector3 origin = fpsCamera.transform.position;
            Vector3 endPos = origin + dir * range;
            if (Physics.Raycast(origin, dir, out hit, range))
            {
                endPos = hit.point;
                // impact effect
                if (impactEffect != null)
                {
                    var impact = Instantiate(
                        impactEffect,
                        hit.point,
                        Quaternion.LookRotation(hit.normal));
                    Destroy(impact, 2f);
                }
            }

            // tracer line
            if (tracerPrefab != null)
            {
                var tracer = Instantiate(tracerPrefab);
                tracer.SetPosition(0, muzzlePoint.position);
                tracer.SetPosition(1, endPos);
                StartCoroutine(FadeTracer(tracer));
            }
        }
    }

    IEnumerator FadeTracer(LineRenderer tr)
    {
        float t = 0f, duration = 0.1f;
        Color startCol = tr.startColor;
        while (t < duration)
        {
            t += Time.deltaTime;
            float a = 1f - (t / duration);
            Color col = new Color(startCol.r, startCol.g, startCol.b, a);
            tr.startColor = col;
            tr.endColor = col;
            yield return null;
        }
        Destroy(tr.gameObject);
    }
}
