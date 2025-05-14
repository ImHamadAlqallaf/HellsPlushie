public interface IDamageable
{
    /// <summary>
    /// Apply this much damage to the object. 
    /// Return true if the object died as a result.
    /// </summary>
    bool TakeDamage(float amount);
}
