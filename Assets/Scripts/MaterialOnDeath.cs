using UnityEngine;

[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(MeshRenderer))]
public class MaterialOnDeath : MonoBehaviour
{
    [Tooltip("Material to apply when health reaches zero")]
    public Material deadMaterial;

    MeshRenderer _renderer;
    Damageable _damageable;

    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _damageable = GetComponent<Damageable>();
        _damageable.onDie.AddListener(OnDeath);
    }

    void OnDeath()
    {
        if (deadMaterial != null)
            _renderer.material = deadMaterial;
    }
}
