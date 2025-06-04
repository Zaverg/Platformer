using UnityEngine;

public class Thorns : Enemy
{
    [SerializeField] private Patrol _patrol;
    [SerializeField] private Chase _chase;
    [SerializeField] private Attack _attack;

    [SerializeField] private State _currentState;

    private void Awake()
    {
        _currentState = _patrol.CanRun() ? _patrol : null;
    }

    private void Update()
    {
        _currentState.Run();
        TryChangeState();
    }

    protected override void TryChangeState()
    {
        bool canChase = _chase.CanRun();
        bool canAttack = _attack.CanRun();

        if ((canChase == false && canAttack == false) && _currentState != _patrol)
        {
            _currentState = _patrol;
        }
        else if (canChase && canAttack == false && _currentState != _chase)
        {
            _currentState = _chase;
        }
        else if (canAttack)
        {
            _currentState = _attack;
        }
    }
}