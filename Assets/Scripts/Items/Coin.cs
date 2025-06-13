using System;
using UnityEngine;

public class Coin : MonoBehaviour, ITaking
{
    public event Action<Coin> Took;

    public void Take(Player player)
    {   
        Took?.Invoke(this);

        player.CollectMoney();
    }
}
