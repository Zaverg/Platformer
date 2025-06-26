using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorPlayer : MonoBehaviour
{
    private static readonly int s_move = Animator.StringToHash("IsMove");
    private static readonly int s_jump = Animator.StringToHash("Jump");
    private static readonly int s_attack = Animator.StringToHash("Attack");

    private Animator _animator;

    public Animator Animator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoveAnimation(bool isMove)
    {
        _animator.SetBool(s_move, isMove);
    }

    public void SetJumpAnimation()
    {
        _animator.SetTrigger(s_jump);
    }

    public void SetAttackAnimation()
    {
        _animator.SetTrigger(s_attack);
    }
}
