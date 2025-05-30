using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    private const string AXIS_MOVING_X = "Horizontal";

    private static readonly int _move = Animator.StringToHash("IsMove");
    private static readonly int _jump = Animator.StringToHash("IsJump");

    [SerializeField] private MovePlayer _movePlayer;
    [SerializeField] private JumpPlayer _jumpPlayer;
    [SerializeField] private DefinedSurfacePlayer _definedSurfacePlayer;

    [SerializeField] private KeyCode _jumpButton;

    private bool _isGrounded;
    private bool _isJump;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    private float _inputDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _inputDirection = Input.GetAxisRaw(AXIS_MOVING_X);

        if (_inputDirection == 0 && _isGrounded)
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool(_move, false);
        }

        if (Input.GetKeyDown(_jumpButton) && _isGrounded)
        {
            _isJump = true;
        }
        
        if (IsFall())
        {
            _animator.SetBool(_jump, false);
        }
    }

    private void FixedUpdate()
    {
        _isGrounded = _definedSurfacePlayer.DefinedSurface();

        if (_isJump)
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool(_move, false);

            _jumpPlayer.Jump(_inputDirection);
            _animator.SetBool(_jump, true);

            _isJump = false;
            _isGrounded = false;
        }
        else if (_isGrounded && Mathf.Abs(_inputDirection) > 0f)
        {
            FlipSprite();
            _movePlayer.Move(_inputDirection);
            _animator.SetBool(_move, true);          
        }
    }

    private bool IsFall() =>
        _rigidbody.velocity.y < 0.1f;
        
    private void FlipSprite() =>
        _spriteRenderer.flipX = _inputDirection < 0;
}