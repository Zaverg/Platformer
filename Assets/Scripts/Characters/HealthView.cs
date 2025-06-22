using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] protected Health _health;

    private void OnEnable()
    {
        _health.Changed += UpdateHealthPointBar;
    }

    private void OnDisable()
    {
        _health.Changed -= UpdateHealthPointBar;
    }

    protected abstract void UpdateHealthPointBar(float currentHP, float maxHP);
}
