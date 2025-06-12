using System;
using UnityEngine;

public class Heal : MonoBehaviour, ITaking
{
    private float countHeal = 20;

    public event Action<Heal> Took;

    public void Take(Player player)
    {
        Took?.Invoke(this);

        player.GetComponent<Health>().Heal(countHeal);
    }
}
