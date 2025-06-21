using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected State _defaltState;
    [SerializeField] protected Health _health;
    [SerializeField] protected List<State> _states = new List<State>();

    [SerializeField] protected State _currentState;

    private void Awake()
    {
        _currentState = _defaltState;
        _currentState.Enter();
    }

    private void OnEnable()
    {
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    }

    private void Update()
    {
        _currentState.Run();
        TryChangeState();
    }

    protected virtual void TryChangeState()
    {
        bool isTrasaction = false;

        foreach (IStateTransition transaction in _states)
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

    protected void Die()
    {
        gameObject.SetActive(false);
    }
}
