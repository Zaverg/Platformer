using System.Collections.Generic;
using UnityEngine;

public class Thorns : Enemy
{
    [SerializeField] private List<State> _states = new List<State>();

    [SerializeField] private State _currentState;

    private void Awake()
    {
        _currentState = _states[2];
        _currentState.Enter();
    }

    private void Update()
    {
        _currentState.Run();
        TryChangeState();
    }

    protected override void TryChangeState()
    {
        foreach (State state in _states)
        {
            if (state.CanTransaction(_currentState))
            {
                _currentState.Exit();
                _currentState = state;
                _currentState.Enter();

                break;
            }
        }
    }
}