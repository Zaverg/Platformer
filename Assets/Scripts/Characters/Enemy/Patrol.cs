using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    [SerializeField] private MoverEnemy _moverEnemy;
    [SerializeField] private float _speedPatrol;

    [SerializeField] private List<Transform> _pointToPatrols = new List<Transform>();
    [SerializeField] private float _arrivalDistance = 0.2f;

    private Transform _currentTarget;

    public override void Enter()
    {
        _currentTarget = _pointToPatrols[0];

        _moverEnemy.SetSpeed(_speedPatrol);
        _moverEnemy.SetTarget(_currentTarget);
    }

    public override bool CanTransaction(State currentState)
    {
        if (currentState == this)
            return false;
        
        return true;
    }

    public override void Run()
    {
        if (Mathf.Abs(_currentTarget.position.x - transform.position.x) <= _arrivalDistance)
        {
            int index = (_pointToPatrols.IndexOf(_currentTarget) + 1) % (_pointToPatrols.Count);

            _currentTarget = _pointToPatrols[index];

            _moverEnemy.SetTarget(_currentTarget);
        }

        _moverEnemy.Move();
    }

    public override void Exit()
    {
        _currentTarget = null;
    }
}
