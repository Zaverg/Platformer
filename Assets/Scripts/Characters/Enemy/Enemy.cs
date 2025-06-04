using UnityEngine;
using UnityEngine.XR;

public abstract class Enemy : MonoBehaviour
{
    protected abstract float CurrentHealthPoints { get; set; }
    protected DetectionZone DetectionZone { get; private set; }

    public virtual void TakeDamage(float damage)
    {
        if (CurrentHealthPoints - damage > 0)
            CurrentHealthPoints -= damage;
        else
            CurrentHealthPoints = 0;
    }

    protected abstract void Move();

    protected abstract void ChangeState();
}
