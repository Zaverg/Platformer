using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    private static readonly int s_move = Animator.StringToHash("IsMove");
    private static readonly int s_jump = Animator.StringToHash("IsJump");

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private MovePlayer _movePlayer;
    [SerializeField] private JumpPlayer _jumpPlayer;
    [SerializeField] private DefinedSurfacePlayer _definedSurfacePlayer;

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputReader.Jumped += _jumpPlayer.Jump;
        _inputReader.Moved += _movePlayer.Move;
    }

    private void OnDisable()
    {
        _inputReader.Jumped -= _jumpPlayer.Jump;
        _inputReader.Moved -= _movePlayer.Move;
    }

    private void Update()
    {
        if (_definedSurfacePlayer.IsGrounded)
        { 
            if (_jumpPlayer.IsJump == false && _inputReader.IsMove == false)
            {        
                _rigidbody.velocity = Vector2.zero;
            }
     
            if(_inputReader.IsMove && _jumpPlayer.IsJump == false)
            {
                _animator.SetBool(s_move, true);
            }
            else
            {
                _animator.SetBool(s_move, false);
            }

            if (_jumpPlayer.IsJump)
            {
                _animator.SetBool(s_jump, true);
            }
        }

        if (_rigidbody.velocity.y < 0.1f)
        {
            _animator.SetBool(s_jump, false);
        }
    }
}