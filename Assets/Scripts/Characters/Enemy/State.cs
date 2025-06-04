using Unity.VisualScripting;
using UnityEngine;

public enum StateName
{
    Patrol,
    Chase,
    Attack
}

public abstract class State : MonoBehaviour
{
    public float ArrivalDistance {get; private set; }
    public StateName StateName { get; set; }
}
