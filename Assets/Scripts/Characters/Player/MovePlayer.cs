using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _climbForce = 10f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(float inputDirection)
    {
        _spriteRenderer.flipX = inputDirection < 0;

        _rigidbody.AddForce(Vector2.right * inputDirection * _speed * _climbForce);
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _speed);  
    }
}
