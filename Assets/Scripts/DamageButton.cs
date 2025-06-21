using UnityEngine;

public class DamageButton : ButtonAction
{
    [SerializeField] private float _damage;

    protected override void OnClick()
    {
        _health.TakeDamage(_damage);
    }
}
