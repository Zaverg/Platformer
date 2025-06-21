using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageble, IHealebel
{
    public event Action<float, float> HealthChanged;
    public event Action Died;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _minHealth;
    [SerializeField] private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0) 
            return;

        _currentHealth = Math.Clamp(_currentHealth - damage, 0, _maxHealth);

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
        HandleDeath();
    }

    public void Heal(float heal)
    {
        if (heal <= 0) 
            return;

        _currentHealth = Math.Clamp(_currentHealth + heal, 0, _maxHealth);

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void HandleDeath()
    {
        if (_currentHealth <= 0)
            Died?.Invoke();
    }
}
