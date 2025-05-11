using UnityEngine;

public class BubbleBlast : MonoBehaviour
{
    [Header("References")]
    public Camera fpsCamera;            // assign your MainCamera here
    public Transform muzzlePoint;       // the empty we just made
    public ParticleSystem muzzleFlash;  // optional
    public GameObject impactEffect;     // optional small prefab
    public LineRenderer tracerPrefab;   // assign your Tracer prefab here
    public GunRecoil recoil;


    [Header("Settings")]
    public float range = 100f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    void Shoot()
    {
        // Muzzle flash
        if (muzzleFlash != null) muzzleFlash.Play();

        // Raycast
        RaycastHit hit;
        if (Physics.Raycast(
                fpsCamera.transform.position,
                fpsCamera.transform.forward,
                out hit,
                range))
        {
            Debug.Log("Hit " + hit.transform.name);

            if (impactEffect != null)
            {
                var impact = Instantiate(
                    impactEffect,
                    hit.point,
                    Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
        }
        if (recoil != null) recoil.FireRecoil();

    }
}
