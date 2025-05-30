using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Taking;
    private float _timeTake;

    public float TimeTake => _timeTake;
    
    public void Take()
    {   
        Taking?.Invoke(this);
        _timeTake = Time.time;
    }
}
