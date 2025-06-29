using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;

    [SerializeField] private float _damage;
    [SerializeField] private float _distance;
    [SerializeField] private float _attackDelay;
    [SerializeField] private AnimatorPlayer _animator;

    private float _lastAttack;
    private bool CanAttack => Time.time >= _lastAttack + _attackDelay;

    private void Awake()
    {
        float halfLength = _animator.Animator.GetCurrentAnimatorStateInfo(0).length / 2;

        _attacker.SetTimeToWait(halfLength);
    }

    public void Attack(Vector2 direction)
    {
        if (CanAttack == false)
            return;

        _animator.SetAttackAnimation();
        _attacker.Attack(_damage, _distance, direction);
        _lastAttack = Time.time;
    }
}
