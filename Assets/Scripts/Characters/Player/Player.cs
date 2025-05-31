using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private MovePlayer _movePlayer;
    [SerializeField] private JumpPlayer _jumpPlayer;
    [SerializeField] private DefinedSurfacePlayer _definedSurfacePlayer;
    [SerializeField] private AnimatorPlayer _animatorPlayer;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _inputReader.Jumped += OnJumpInput;
        _inputReader.Moved += OnMovementInput;
    }

    private void OnDisable()
    {
        _inputReader.Jumped -= OnJumpInput;
        _inputReader.Moved -= OnMovementInput;
    }

    private void Update()
    {
        if (_jumpPlayer.IsJump == false && _inputReader.IsMove == false && _definedSurfacePlayer.IsGrounded)      
            _rigidbody.velocity = Vector2.zero;
        
        bool shouldMove = _jumpPlayer.IsJump == false && _inputReader.IsMove && _definedSurfacePlayer.IsGrounded;
        _animatorPlayer.SetMoveAnimation(shouldMove);

        _animatorPlayer.SetJumpAnimation(_jumpPlayer.IsJump);
    }

    private void OnMovementInput(float inputDirection)
    {
        _spriteRenderer.flipX = inputDirection < 0;

        if (_jumpPlayer.IsJump == false && _definedSurfacePlayer.IsGrounded)
            _movePlayer.Move(inputDirection);
    }

    private void OnJumpInput(float inputDirection)
    {
        if (_definedSurfacePlayer.IsGrounded && _jumpPlayer.IsJump == false)
            _jumpPlayer.Jump(inputDirection);
    }
}