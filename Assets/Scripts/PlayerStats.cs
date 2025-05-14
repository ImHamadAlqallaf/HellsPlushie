using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class PlayerStats : MonoBehaviour
{
    [Header("UI")]
    public Slider healthSlider;
    public Slider staminaSlider;

    [Header("Health")]
    public float maxHealth = 100f;
    private float _currentHealth;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float drainRate = 20f;  // per second while sprinting
    public float regenRate = 15f;  // per second while not sprinting
    private float _currentStamina;

    [Header("Sprint Input")]
    public StarterAssetsInputs inputActions;

    void Start()
    {
        _currentHealth = maxHealth;
        _currentStamina = maxStamina;

        healthSlider.maxValue = maxHealth;
        staminaSlider.maxValue = maxStamina;
        healthSlider.value = _currentHealth;
        staminaSlider.value = _currentStamina;
    }

    void Update()
    {
        HandleStamina();
        healthSlider.value = _currentHealth;
        staminaSlider.value = _currentStamina;
    }

    void HandleStamina()
    {
        // If they're trying to sprint but have no stamina, immediately cancel sprint.
        if (inputActions.sprint && _currentStamina <= 0f)
        {
            inputActions.sprint = false;
        }

        // Drain only if sprint is actually on and we have stamina
        if (inputActions.sprint && _currentStamina > 0f)
        {
            _currentStamina -= drainRate * Time.deltaTime;
            if (_currentStamina <= 0f)
            {
                _currentStamina = 0f;
                inputActions.sprint = false;   // force?stop at zero
            }
        }
        else
        {
            // Regen whenever not sprinting
            _currentStamina += regenRate * Time.deltaTime;
            if (_currentStamina > maxStamina)
                _currentStamina = maxStamina;
        }
    }

    public void TakeDamage(float dmg)
    {
        _currentHealth -= dmg;
        if (_currentHealth < 0f) _currentHealth = 0f;
    }
}
