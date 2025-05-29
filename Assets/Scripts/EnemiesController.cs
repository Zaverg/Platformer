using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Patrol,
    Chase
}

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private float _speedPatrol;
    [SerializeField] private float _speedChase;
    [SerializeField] private ZoneDetection _zoneDetection;
    [SerializeField] private List<Transform> _pointToPatrols = new List<Transform>();
    [SerializeField] private float _distanceChase;
    [SerializeField] private float _arrivalDistance = 0.2f;

    private Transform _mainPoint;

    private CharacterController _target;
    private Transform _currentPoint;
    private State _currentState;

    private void Start()
    {
        _currentState = State.Patrol;
        _mainPoint = _pointToPatrols[0];

        _currentPoint = _mainPoint;
    }

    private void OnEnable()
    {
        _zoneDetection.DetectedPlayer += DetectedPlayer;
    }

    private void OnDisable()
    {
        _zoneDetection.DetectedPlayer -= DetectedPlayer;
    }

    private void Update()
    {
        if (_currentState == State.Patrol)
        {
            Patrolling();
        }
        else if (_currentState == State.Chase)
        {
            if ((_mainPoint.transform.position - _target.transform.position).sqrMagnitude >= _distanceChase * _distanceChase)
            {
                DetectedPlayer(null);

                return;
            }

            Chase();
        }
    }

    private void Patrolling()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentPoint.position, _speedPatrol * Time.deltaTime);

        if (IsHere(_currentPoint))
        {
            int index = (_pointToPatrols.IndexOf(_currentPoint) + 1) % (_pointToPatrols.Count);
            int flip = -1;

            _currentPoint = _pointToPatrols[index];
            _zoneDetection.transform.localPosition = new Vector3(_zoneDetection.transform.localPosition.x * flip, _zoneDetection.transform.localPosition.y, 0);
        }
    }

    private void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speedChase * Time.deltaTime);
    }

    private bool IsHere(Transform point)
    {
        if ((point.position - transform.position).sqrMagnitude < _arrivalDistance * _arrivalDistance)
            return true;

        return false;
    }

    private void DetectedPlayer(CharacterController player)
    {
        _target = player;
        _currentState = TryChangeState();
    }

    private State TryChangeState()
    {
        if (_target != null)
            return State.Chase;

        return State.Patrol;
    }
}