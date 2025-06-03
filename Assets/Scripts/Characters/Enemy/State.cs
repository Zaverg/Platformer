using UnityEngine;

public abstract class State : MonoBehaviour
{
    public float ArrivalDistance {get; private set; }

    public abstract Transform ChangeTarget(Transform current);
 
}
