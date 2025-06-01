using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float _speedPatrol;

    [SerializeField] private List<Transform> _pointToPatrols = new List<Transform>();
    [SerializeField] private float _arrivalDistance = 0.2f;

    private Transform _currentTarget;

    public Transform CurrentTarget => _currentTarget;
    public float Speed => _speedPatrol;

    private void Start()
    {
        _currentTarget = _pointToPatrols[0];
    }

    private void Update()
    {
        PatrolArea();
    }

    private void PatrolArea()
    {
        if (Mathf.Abs(_currentTarget.position.x - transform.position.x) <= _arrivalDistance)
        {
            int index = (_pointToPatrols.IndexOf(_currentTarget) + 1) % (_pointToPatrols.Count);

            _currentTarget = _pointToPatrols[index];
        }
    }
}
