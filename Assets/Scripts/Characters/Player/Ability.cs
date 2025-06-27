using System;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public abstract event Action<float, float> Changed;
    public abstract void Activate();
}

