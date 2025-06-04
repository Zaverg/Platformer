using System;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    [SerializeField] private float _speedPatrol;

    [SerializeField] private List<Transform> _pointToPatrols = new List<Transform>();
    [SerializeField] private float _arrivalDistance = 0.2f;

    private Transform _currentTarget;

    public StateName StateName => StateName.Patrol;

    public Transform CurrentTarget => _currentTarget;
    public float Speed => _speedPatrol;
    public float ArrivalDistance => _arrivalDistance;

    private void Start()
    {
        _currentTarget = _pointToPatrols[0];
    }

    public Transform TryUpdateTarget(Transform currentTarget)
    {
        if (Mathf.Abs(currentTarget.position.x - transform.position.x) <= _arrivalDistance)
        {
            int index = (_pointToPatrols.IndexOf(_currentTarget) + 1) % (_pointToPatrols.Count);

            return _pointToPatrols[index];
        }

        return currentTarget;
    }
}
