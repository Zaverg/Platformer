using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _climbForce = 10f;

    [SerializeField] private float _surfaceCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _startRayPosition;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpAngel = 45;

    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isJump;

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
        _inputDirection = Input.GetAxisRaw("Horizontal");

        if (_inputDirection == 0 && _isGrounded)
            _rigidbody.velocity = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isJump = true;
        }
    }

    private void FixedUpdate()
    {
        DefinedSurface();

        if (_isJump && _isGrounded)
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool("IsMove", false);

            Jump();

            _isGrounded = false;
        }
        else if (_isGrounded)
        {
            Move();
        }
        else if (IsFall())
        {
            _animator.SetBool("IsJump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
            TakeCoin(coin);
    }

    private void DefinedSurface()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector3(_startRayPosition.transform.position.x, _startRayPosition.transform.position.y, _startRayPosition.transform.position.z),
            Vector2.down,
            _surfaceCheckDistance,
            _groundLayer);

        _isGrounded = hit.collider != null;
    }

    private void Move()
    {
        if (Mathf.Abs(_inputDirection) > 0.1f)
        {
            FlipSprite();
            _animator.SetBool("IsMove", true);
            _rigidbody.AddForce(Vector2.right * _inputDirection * _speed * _climbForce);
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _speed);
        }
        else
        {
            _animator.SetBool("IsMove", false);
        }
    }

    private void Jump()
    {
        Vector2 directionJump;
        float angelRad = _jumpAngel * Mathf.Deg2Rad;

        float jumpForceX = Mathf.Cos(angelRad);
        float jumpForceY = Mathf.Sin(angelRad);

        if (_inputDirection != 0)
        {
            directionJump = new Vector2(jumpForceX * Mathf.Sign(_inputDirection), jumpForceY) * _jumpForce;
        }
        else
        {
            directionJump = Vector2.up * _jumpForce;
        }

        _animator.SetBool("IsJump", true);
        _rigidbody.AddForce(directionJump, ForceMode2D.Impulse);
        _isJump = false;
    }

    private bool IsFall() =>
        _rigidbody.velocity.y < 0.1f;
        
    private void FlipSprite() =>
        _spriteRenderer.flipX = _inputDirection < 0 ? true : false;

    private void TakeCoin(Coin coin)
    {
        coin.Take();
    }
}