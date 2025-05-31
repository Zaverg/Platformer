using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class JumpPlayer : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpAngel = 45;

    private Rigidbody2D _rigidbody;

    private bool _isJump;

    public bool IsJump => _isJump;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_rigidbody.velocity.y == 0f)
            _isJump = false;
    }

    public void Jump(float inputDirection)
    {
        _rigidbody.velocity = Vector2.zero;
        _isJump = true;

        Vector2 directionJump;
        float angelRad = _jumpAngel * Mathf.Deg2Rad;

        float jumpForceX = Mathf.Cos(angelRad);
        float jumpForceY = Mathf.Sin(angelRad);

        if (inputDirection != 0)
        {
            directionJump = new Vector2(jumpForceX * Mathf.Sign(inputDirection), jumpForceY) * _jumpForce;
        }
        else
        {
            directionJump = Vector2.up * _jumpForce;
        }

        _rigidbody.AddForce(directionJump, ForceMode2D.Impulse);
    }
}
