using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverEnemy : MonoBehaviour
{
    [SerializeField] private Transform _currentTarget;
    [SerializeField] private EnemyAnimator _enemyAnimator;

    private Rigidbody2D _rigidbody2D;

    private float _speed;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        if (_currentTarget != null)
        {
            Vector2 direction = (_currentTarget.position - transform.position).normalized;
            _rigidbody2D.velocity = new Vector2(direction.x * _speed, _rigidbody2D.velocity.y);
        }
    }

    public void SetParams(Transform target, float speed = 0)
    {
        _rigidbody2D.velocity = Vector2.zero;

        _currentTarget = target;
        _speed = speed;
        _enemyAnimator.SetMoveAnumation(_speed);
    }
}
