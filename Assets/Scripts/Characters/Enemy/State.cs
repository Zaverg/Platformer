using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract void Run();
    public abstract bool CanRun();
}
