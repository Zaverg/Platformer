using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private static readonly int s_attack = Animator.StringToHash("Attack");
    private static readonly int s_preparing = Animator.StringToHash("Preparing");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAttackAnimation(bool isAttak)
    {
        _animator.SetBool(s_attack, isAttak);
    }

    public void SetPreparingForAttackAnimation(bool isPreparing)
    {
        _animator.SetBool(s_preparing, isPreparing);
    }
}
