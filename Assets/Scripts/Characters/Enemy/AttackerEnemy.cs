using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class AttackerEnemy : State, ITransaction
{
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _damage;

    [SerializeField] private Attacker _attacker;
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private EnemyAnimator _animator;

    private float _startAttack;
    private bool _isAttack;

    private Player _player;

    private Rigidbody2D _rigidboody2d;

    private void Awake()
    {
        _rigidboody2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _detectionZone.Detected += SetTarget;
    }

    private void OnDisable()
    {
        _detectionZone.Detected -= SetTarget;
    }

    public override void Enter()
    {
        _rigidboody2d.velocity = Vector2.zero;
        _startAttack = Time.time;

        _animator.SetPreparingForAttackAnimation(true);
    }

    public override void Run()
    {
        if (_startAttack + _attackDelay <= Time.time)
        {
            _animator.SetPreparingForAttackAnimation(false);
            _isAttack = true;

            Vector2 directionToPlayer = (_player.transform.position - transform.position).normalized;
            _attacker.Attack(_damage, _attackDistance, directionToPlayer);

            _startAttack = Time.time;
        }
        else if (_isAttack)
        {
            _isAttack = false;
            _animator.SetPreparingForAttackAnimation(true);
        }

        _animator.SetAttackAnimation(_isAttack);
    }

    public bool CanTransaction()
    {
        if (_player == null)
        {
            return false;
        }
        else if ((_player.transform.position - transform.position).sqrMagnitude > _attackDistance * _attackDistance)
        {
            return false;
        }

        return true;
    }

    public override void Exit()
    {
        _animator.SetAttackAnimation(false);
        _animator.SetPreparingForAttackAnimation(false);
    }

    private void SetTarget(Player player)
    {
        _player = player;
    }
}
