using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorPlayer : MonoBehaviour
{
    private static readonly int s_move = Animator.StringToHash("IsMove");
    private static readonly int s_jump = Animator.StringToHash("IsJump");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoveAnimation(bool isMove)
    {
        _animator.SetBool(s_move, isMove);
    }

    public void SetJumpAnimation(bool isJump)
    {
        _animator.SetBool(s_jump, isJump);
    }
}
