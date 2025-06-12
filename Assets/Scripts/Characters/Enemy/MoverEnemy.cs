using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverEnemy : MonoBehaviour
{
    [SerializeField] private Transform _currentTarget; 
    private Rigidbody2D _rigidbody2D;

    private float _speed;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _currentTarget = null;
    }

    public void Move()
    {
        if (_currentTarget != null)
        {
            Vector2 direction = (_currentTarget.position - transform.position).normalized;
            _rigidbody2D.velocity = new Vector2(direction.x * _speed, _rigidbody2D.velocity.y);
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
}

    public void SetTarget(Transform target)
    {
       _currentTarget = target;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
