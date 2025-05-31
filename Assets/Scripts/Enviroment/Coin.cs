using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _timeTake;

    public event Action<Coin> Took;

    public float TimeTake => _timeTake;
    
    public void Take()
    {   
        Took?.Invoke(this);
        _timeTake = Time.time;
    }
}
