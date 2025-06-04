using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Thorns : Enemy
{
    [SerializeField] private Patrol _patrol;
    [SerializeField] private Chase _chase;

    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private DefinedSurfacePlayer _definedSurface;

    protected override float CurrentHealthPoints { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private Transform _currentTarget;
    private State _currentState;

    private float _currentSpeed;

    private void OnEnable()
    {
        _detectionZone.Detected += DoChase;
    }

    private void OnDisable()
    {
        _detectionZone.Detected -= DoChase;
    }

    private void Update()
    {
        Move();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, _currentSpeed * Time.deltaTime);
    }

    private void DoPatrol()
    {
        Debug.Log("Patrol");
        Patrol patrol = _currentState as Patrol;

        if (patrol != null)
        {
            _currentTarget = patrol.TryUpdateTarget(_currentTarget);
        }
    }

    private void DoChase(Transform target)
    {
        _currentState = _chase;
        _currentTarget = target;
    }

    private void DoAttack()
    {

    }

    protected override void ChangeState()
    {
        throw new NotImplementedException();
    }

}