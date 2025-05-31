using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string AXIS_MOVING_X = "Horizontal";

    [SerializeField] private KeyCode _jumpButton;

    private float _inputDirection;

    public event Action<float> Jumped;
    public event Action<float> Moved;
    
    public bool IsMove => _inputDirection != 0;

    private void Update()
    {
        _inputDirection = Input.GetAxisRaw(AXIS_MOVING_X);

        if (Input.GetKeyDown(_jumpButton))
        {
            Jumped?.Invoke(_inputDirection);
        }

        if (IsMove)
        {
            Moved?.Invoke(_inputDirection);
        }
    }
}
