using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private static readonly int s_attack = Animator.StringToHash("IsAttack");
    private static readonly int s_move = Animator.StringToHash("IsMove");
    private static readonly int s_speed = Animator.StringToHash("Speed");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAttackAnimation(bool isAttak)
    {
        _animator.SetBool(s_attack, isAttak);
    }

    public void SetMoveAnumation(bool isMove, float speed)
    {
        _animator.SetBool(s_move, isMove);
        _animator.SetFloat(s_speed, speed);
    }
}
