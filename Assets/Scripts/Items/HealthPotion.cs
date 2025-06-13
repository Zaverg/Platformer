using System;
using UnityEngine;

public class HealthPotion : MonoBehaviour, ITaking
{
    private float _healAmount = 20;

    public event Action<HealthPotion> Took;

    public void Take(Player player)
    {
        Took?.Invoke(this);

        player.UseHealPoition(_healAmount);
    }
}
