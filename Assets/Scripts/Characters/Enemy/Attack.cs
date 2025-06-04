using UnityEngine;

public class Attack : State
{
    [SerializeField] private float _attackDistance;
    [SerializeField] private DetectionZone _detectionZone;

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
        Debug.Log("Attack");
    }

    public override bool CanRun()
    {
        if (_player == null)
            return false;

        if ((_player.transform.position - transform.position).sqrMagnitude > _attackDistance * _attackDistance)
            return false;

        return true;
    }

    private void SetTarget(Player player)
    {
        _player = player;
    }
}
