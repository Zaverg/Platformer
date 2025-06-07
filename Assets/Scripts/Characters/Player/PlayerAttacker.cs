using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;

    [SerializeField] private float _damage;
    [SerializeField] private float _distance;
    [SerializeField] private float _attackDelay;

    public void Attack()
    {
        _attacker.Attack(_damage, _distance);
    }
}
