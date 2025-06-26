using UnityEngine;

public class Chaser : State, IStateTransition
{
    [SerializeField] private MoverEnemy _moverEnemy; 
    [SerializeField] private DetectionZone _detectionZone;

    [SerializeField] private float _speed;
    [SerializeField] private float _chaseDistance;

    [SerializeField] private Transform _startPointPositioin;
    private Player _player;

    private void Start()
    {
        transform.position = _startPointPositioin.position;
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
        _moverEnemy.SetParams(_player.transform, _speed);
    }

    public override void Run()
    {
         _moverEnemy.Move();
    }

    public bool CanTransaction()
    {
        if (_player != null)
        {
            bool isAway = (_startPointPositioin.position - transform.position).sqrMagnitude > _chaseDistance * _chaseDistance;

            if (isAway == false)
                return true;

            _player = null;
        }

        return false;
    }

    public override void Exit()
    {
        _moverEnemy.SetParams(null);
    }

    private void SetTarget(Player player)
    {
        _player = player;
    }
}
