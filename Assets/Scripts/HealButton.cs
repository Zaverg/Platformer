using UnityEngine;

public class HealButton : ButtonAction
{
    [SerializeField] private float _heal;

    protected override void OnClick()
    {
        _health.Heal(_heal);
    }
}
