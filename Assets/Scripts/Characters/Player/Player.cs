using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private DefinedSurfacePlayer _definedSurfacePlayer;
    [SerializeField] private AnimatorPlayer _animatorPlayer;
    [SerializeField] private PhysicsPlayer _physicsPlayer;

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
        if (_definedSurfacePlayer.IsGrounded && _inputReader.IsMove == false && _jumper.IsJump == false)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.drag = 1000000;
        }
        else
        {
            _rigidbody.drag = 0;
        }
            
        bool shouldMove = _jumper.IsJump == false && _inputReader.IsMove && _definedSurfacePlayer.IsGrounded;
        _animatorPlayer.SetMoveAnimation(shouldMove);

        bool shouldJump = _jumper.IsJump && _rigidbody.velocity.y > 0.1f;
        _animatorPlayer.SetJumpAnimation(shouldJump);
    }

    private void OnMovementInput(float inputDirection)
    {
        _spriteRenderer.flipX = inputDirection < 0;

        if (_jumper.IsJump == false && _definedSurfacePlayer.IsGrounded)
        {
            Vector2 direction = GetDirectionMove(inputDirection);

            _mover.Move(direction);

            _rigidbody.AddForce(Vector2.down * 4);
        }
    }

    private void OnJumpInput(float inputDirection)
    {
        if (_definedSurfacePlayer.IsGrounded && _jumper.IsJump == false)
            _jumper.Jump(inputDirection);
    }

    private Vector2 GetDirectionMove(float inputDirection)
    {
        Vector2 angelNormal = _definedSurfacePlayer.DefineAngel(inputDirection);
        Vector2 angelMove = new Vector2(angelNormal.y, -angelNormal.x).normalized;

        return angelMove * inputDirection;
    }
}