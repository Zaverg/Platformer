using UnityEngine;

public class Chase : State
{
    [SerializeField] private MoverEnemy _moverEnemy;
    [SerializeField] private float _speed;
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private float _chaseDistance;

    private Player _player;

    private void OnEnable()
    {
        _detectionZone.Detected += SetTarget;
    }

    private void OnDisable()
    {
        _detectionZone.Detected -= SetTarget;
    }

    public override void Run()
    {
        if ((_player.transform.position - transform.position).sqrMagnitude > _chaseDistance * _chaseDistance)
            _player = null;
        else
            _moverEnemy.Move();
    }

    public override bool CanRun()
    {
        bool canRun = false;

        if (_player != null)
        {
            canRun = true;
            _moverEnemy.SetTarget(_player.transform);
            _moverEnemy.SetSpeed(_speed);
        }

        return canRun;
    } 

    private void SetTarget(Player player)
    {
        _player = player;
    }
}
