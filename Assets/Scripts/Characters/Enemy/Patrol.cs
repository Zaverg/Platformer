using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    [SerializeField] private float _speedPatrol;

    [SerializeField] private List<Transform> _pointToPatrols = new List<Transform>();
    [SerializeField] private float _arrivalDistance = 0.2f;

    private Transform _currentTarget;

    public Transform CurrentTarget => _currentTarget;
    public float Speed => _speedPatrol;
    public float ArrivalDistance => _arrivalDistance;

    private void Start()
    {
        _currentTarget = _pointToPatrols[0];
    }

    private void Update()
    {
        Vector3 target = new Vector3(_currentTarget.position.x, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target, _speedPatrol * Time.deltaTime);
    }

    private void TryUpdateTarget()
    {
        if (Mathf.Abs(_currentTarget.position.x - transform.position.x) <= _arrivalDistance)
        {
            int index = (_pointToPatrols.IndexOf(_currentTarget) + 1) % (_pointToPatrols.Count);

            _currentTarget = _pointToPatrols[index];
        }
    }

    public override Transform ChangeTarget(Transform current)
    {
        int index = (_pointToPatrols.IndexOf(current) + 1) % (_pointToPatrols.Count);

        return _pointToPatrols[index];
    }
}
