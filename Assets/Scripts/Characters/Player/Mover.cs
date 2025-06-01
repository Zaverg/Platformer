using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _climbForce = 10f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float inputDirection, Vector2 surfaceDirection)
    {
        _rigidbody.AddForce(surfaceDirection * inputDirection * _speed * _climbForce);
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _speed);  
    }
}
