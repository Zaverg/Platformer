using System;
using UnityEngine;

public class Thorns : Enemy
{
    [SerializeField] private Patrol _patrol;
    [SerializeField] private Chase _chase;

    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private DefinedSurfacePlayer _definedSurface;

    protected override float CurrentHealthPoints { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private Transform _currentTarget;
    private State _currentState;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
       
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    private void TryUpdateTarget(State state)
    {
        if (CheckTargetDistance(_currentTarget, state.ArrivalDistance))
        {
            state.ChangeTarget(_currentTarget);
        }
    }
}