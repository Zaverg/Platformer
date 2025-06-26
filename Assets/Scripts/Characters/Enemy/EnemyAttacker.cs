using UnityEngine;

public class EnemyAttacker : State, IStateTransition
{
    [SerializeField] private float _attackDistance;

    [SerializeField] private float _attackDelayMin;
    [SerializeField] private float _attackDelayMax;
    [SerializeField] private float _damage;

    [SerializeField] private Attacker _attacker;
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private EnemyAnimator _animator;

    private float _startAttack;
    private float _attackDelay;

    private bool _isAttack;

    private Player _player;

    private void Awake()
    {
        _attackDelay = Random.Range(_attackDelayMin, _attackDelayMax);

        float halfLength = _animator.Animator.GetCurrentAnimatorStateInfo(0).length / 2;

        _attacker.SetTimeToWait(halfLength);
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
        _startAttack = Time.time;
    }

    public override void Run()
    {
        if (_startAttack + _attackDelay <= Time.time)
        {
            Vector2 directionToPlayer = (_player.transform.position - transform.position).normalized;
            _attacker.Attack(_damage, _attackDistance, directionToPlayer);

            _startAttack = Time.time;
            _attackDelay = Random.Range(_attackDelayMin, _attackDelayMax);
            _animator.SetAttackAnimation();

            _isAttack = true;
        }
    }

    public bool CanTransaction()
    {
        if (_player == null)
        {
            return false;
        }
        else if ((_player.transform.position - transform.position).sqrMagnitude > _attackDistance * _attackDistance && _isAttack)
        {
            _isAttack = false;
            return false;
        }

        return true;
    }

    public override void Exit()
    {
        _player = null;
    }

    private void SetTarget(Player player)
    {
        _player = player;
    }
}
