using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Damageable : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public float maxHealth = 50f;
    float _current;

    [Header("Events")]
    public UnityEvent onHit;   // invoked whenever damage is taken
    public UnityEvent onDie;   // invoked once when health hits zero

    void Awake()
    {
        _current = maxHealth;
    }

    /// <summary>
    /// IDamageable implementation.
    /// </summary>
    public bool TakeDamage(float amount)
    {
        if (_current <= 0f) return false; // already dead

        _current -= amount;
        onHit?.Invoke();

        if (_current <= 0f)
        {
            _current = 0f;
            onDie?.Invoke();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Optional: expose current health fraction [0..1]
    /// </summary>
    public float NormalizedHealth => _current / maxHealth;
}
