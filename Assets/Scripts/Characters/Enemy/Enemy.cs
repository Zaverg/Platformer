using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected abstract float CurrentHealthPoints { get; set; }

    protected State ChangeState(State newState, State current)
    {
        if (current == newState)
            return current;

        return newState;
    }

    protected bool CheckTargetDistance(Transform target, float distance)
    {
        if (Mathf.Abs(target.position.x - transform.position.x) <= distance)
            return true;

        return false;
    }
    
    public virtual void TakeDamage(float damage)
    {
        if (CurrentHealthPoints - damage > 0)
            CurrentHealthPoints -= damage;
        else
            CurrentHealthPoints = 0;
    }
}
