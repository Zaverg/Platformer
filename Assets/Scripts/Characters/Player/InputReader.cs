using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string AxisMovingX = "Horizontal";

    [SerializeField] private KeyCode _jumpButton;
    [SerializeField] private KeyCode _abilityActicate;

    private int _mouseButtonAttack = 0;
    private float _inputDirection;

    public event Action<float> Jumped;
    public event Action<float> Moved;
    public event Action AbilityActivating;
    public event Action Attacked;

    public bool IsMove => _inputDirection != 0;

    private void Update()
    {
        _inputDirection = Input.GetAxisRaw(AxisMovingX);

        if (Input.GetKeyDown(_jumpButton))
        {
            Jumped?.Invoke(_inputDirection);
        }

        if (IsMove)
        {
            Moved?.Invoke(_inputDirection);
        }

        if (Input.GetMouseButtonDown(_mouseButtonAttack))
        {
            Attacked?.Invoke();
        }

        if (Input.GetKeyDown(_abilityActicate))
        {
            AbilityActivating?.Invoke();
        }
    }
}
