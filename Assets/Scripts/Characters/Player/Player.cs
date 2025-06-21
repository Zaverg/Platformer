using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private PlayerAttacker _attacker;
    [SerializeField] private GroundDetector _definedSurfacePlayer;
    [SerializeField] private AnimatorPlayer _animatorPlayer;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Health _health;

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
        _inputReader.Attacked += OnAttackInput;
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _inputReader.Jumped -= OnJumpInput;
        _inputReader.Moved -= OnMovementInput;
        _inputReader.Attacked -= OnAttackInput;
        _health.Died -= Die;
    }

    private void Update()
    {
        if (_definedSurfacePlayer.IsGrounded == false && _jumper.IsJump == false || _inputReader.IsMove == false && _jumper.IsJump == false)
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }
            
        bool shouldMove = _jumper.IsJump == false && _inputReader.IsMove && _definedSurfacePlayer.IsGrounded;
        _animatorPlayer.SetMoveAnimation(shouldMove);

        bool shouldJump = _jumper.IsJump && _definedSurfacePlayer.IsGrounded;
        _animatorPlayer.SetJumpAnimation(shouldJump);
    }

    public void CollectMoney()
    {
        _wallet.TakeMoney();
    }

    public void UseHealPoition(float amountHeal)
    {
        _health.Heal(amountHeal);
    }

    private void OnMovementInput(float inputDirection)
    {
        _spriteRenderer.flipX = inputDirection < 0;

        if (_jumper.IsJump == false && _definedSurfacePlayer.IsGrounded)
        {
            Vector2 direction = Vector2.right * inputDirection;
            _mover.Move(direction);

            _rigidbody.AddForce(Vector2.down * 4);
        }
    }

    private void OnJumpInput(float inputDirection)
    {
        if (_definedSurfacePlayer.IsGrounded && _jumper.IsJump == false)
            _jumper.Jump(inputDirection);
    }

    private void OnAttackInput()
    {
        Vector2 direction = _spriteRenderer.flipX ? Vector2.left : Vector2.right;

        _attacker.Attack(direction);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}