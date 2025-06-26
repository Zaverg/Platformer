using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private static readonly int s_attack = Animator.StringToHash("Attack");
    private static readonly int s_speed = Animator.StringToHash("Speed");

    private Animator _animator;

    public Animator Animator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAttackAnimation()
    {
        _animator.SetTrigger(s_attack);
    }

    public void SetMoveAnumation(float speed)
    {
        _animator.SetFloat(s_speed, speed);
    }
}
