using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Taking;

    public void Take() =>
        Taking?.Invoke(this);
    
}
