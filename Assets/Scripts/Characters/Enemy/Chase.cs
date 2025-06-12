using UnityEngine;

public class Chase : State, ITransaction
{
    [SerializeField] private MoverEnemy _moverEnemy; 
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private Player _player;

    [SerializeField] private float _speed;
    [SerializeField] private float _lostDistance;
    [SerializeField] private float _chaseDistance;

    private Vector3 _startPositioin;

    private void Start()
    {
        _startPositioin = transform.position;
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
        _moverEnemy.SetTarget(_player.transform);
        _moverEnemy.SetSpeed(_speed);
    }

    public override void Run()
    {
        bool isLost = (_player.transform.position - transform.position).sqrMagnitude > _lostDistance * _lostDistance;
        bool isAway = (_startPositioin - transform.position).sqrMagnitude > _chaseDistance * _chaseDistance;

        if (isLost || isAway)
            _player = null;
        else
            _moverEnemy.Move();
    }

    public bool CanTransaction()
    {
        if (_player == null)
            return false;

        return true;
    }

    public override void Exit()
    {
        _moverEnemy.SetTarget(null);
        _moverEnemy.SetSpeed(0);
    }

    private void SetTarget(Player player)
    {
        _player = player;
    }
}
