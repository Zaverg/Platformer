using System.Collections.Generic;
using UnityEngine;

public class Thorns : Enemy
{
    [SerializeField] private State _defaltState;
    [SerializeField] private List<State> _states = new List<State>();

    [SerializeField] private State _currentState;

    private void Awake()
    {
        _currentState = _defaltState;
        _currentState.Enter();
    }

    private void Update()
    {
        _currentState.Run();
        TryChangeState();
    }

    protected override void TryChangeState()
    {
        bool isTrasaction = false;

        foreach (ITransaction transaction in _states)
        {
            if (transaction.CanTransaction() == false)
                continue;

            isTrasaction = true;

            if (_currentState != transaction as State)
            {
                _currentState.Exit();
                _currentState = transaction as State;
                _currentState.Enter();
            }

            break;
        }

        if (isTrasaction == false && _currentState != _defaltState)
        {
            _currentState.Exit();
            _currentState = _defaltState;
            _currentState.Enter();
        }
    }
}