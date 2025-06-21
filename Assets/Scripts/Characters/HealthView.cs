using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] protected Health _health;

    private void OnEnable()
    {
        _health.HealthChanged += UpdateHealthPointBar;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateHealthPointBar;
    }

    protected abstract void UpdateHealthPointBar(float currentHP, float maxHP);
}
