using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpAngel;

    private Rigidbody2D _rigidbody;

    [SerializeField] private bool _isJump;

    public bool IsJump => _isJump;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isJump && _rigidbody.velocity.y == 0f)
            _isJump = false;
    }

    public void Jump(float inputDirection)
    {
        _isJump = true;

        Vector2 directionJump;

        if (inputDirection == 0)
        {
            directionJump = Vector2.up * _jumpForce;
        }
        else
        {
            float angelRad = _jumpAngel * Mathf.Deg2Rad;

            float jumpForceX = Mathf.Cos(angelRad);
            float jumpForceY = Mathf.Sin(angelRad);

            directionJump = new Vector2(jumpForceX * Mathf.Sign(inputDirection), jumpForceY) * _jumpForce;
        }

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(directionJump, ForceMode2D.Impulse);
    }
}
