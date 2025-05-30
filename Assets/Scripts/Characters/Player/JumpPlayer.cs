using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class JumpPlayer : MonoBehaviour
{
    private static readonly int _jump = Animator.StringToHash("IsJump");

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpAngel = 45;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump(float inputDirection)
    {
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
