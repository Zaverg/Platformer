using System.Collections.Generic;
using UnityEngine;

public class Patroller : State
{
    [SerializeField] private MoverEnemy _moverEnemy;
    [SerializeField] private float _speed;

    [SerializeField] private List<Transform> _pointToPatrols = new List<Transform>();
    [SerializeField] private float _arrivalDistance = 0.2f;

    private Transform _currentTarget;

    public override void Enter()
    {
        _currentTarget = _pointToPatrols[0];
        _moverEnemy.SetParams(_currentTarget, _speed);
    }

    public override void Run()
    {
        if (Mathf.Abs(_currentTarget.position.x - transform.position.x) <= _arrivalDistance)
        {
            int index = (_pointToPatrols.IndexOf(_currentTarget) + 1) % (_pointToPatrols.Count);

            _currentTarget = _pointToPatrols[index];

            _moverEnemy.SetParams(_currentTarget, _speed);
        }

        _moverEnemy.Move();
    }

    public override void Exit()
    {
        _moverEnemy.SetParams(null);
    }
}
