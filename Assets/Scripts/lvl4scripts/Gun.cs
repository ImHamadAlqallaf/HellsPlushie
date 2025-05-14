using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 20f;
    public float verticalRange = 20f;
    public float fireRate;
    private float nextTimeToFire;
    public float bigDamage = 2f;
    public float smallDamage = 1f;
    private BoxCollider gunTrigger;

    // Only cast ray against enemies
    public LayerMask enemyLayerMask;
    public EnemyManager enemyManager;

    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.isTrigger = true;
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        RaycastHit hit;

        // Raycast only against the Enemy layer
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, enemyLayerMask))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                float dist = Vector3.Distance(hit.point, transform.position);
                float damage = dist > range * 0.5f ? smallDamage : bigDamage;
                enemy.TakeDamage(damage);
            }
        }

        nextTimeToFire = Time.time + fireRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemyManager.RemoveEnemy(enemy);
        }
    }
}
