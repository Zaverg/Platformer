using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageble, IHealebel
{
    public event Action<float, float> Changed;
    public event Action Died;

    [SerializeField] private float _max;
    [SerializeField] private float _min;
    [SerializeField] private float _current;

    private void Awake()
    {
        _current = _max;
        Changed?.Invoke(_current, _max);
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0) 
            return;

        _current = Math.Clamp(_current - damage, 0, _max);

        Changed?.Invoke(_current, _max);
        HandleDeath();
    }

    public void Heal(float heal)
    {
        if (heal <= 0) 
            return;

        _current = Math.Clamp(_current + heal, 0, _max);

        Changed?.Invoke(_current, _max);
    }

    private void HandleDeath()
    {
        if (_current <= 0)
            Died?.Invoke();
    }
}
