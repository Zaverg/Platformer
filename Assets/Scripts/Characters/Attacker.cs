using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerAttack;

    private IDamageable _enemy;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private void Awake()
    {
        _wait = new WaitForSeconds(0);
    }

    public void SetTimeToWait(float seconds)
    {
        _wait = new WaitForSeconds(seconds);
    }

    public void Attack(float damage, float attacDistance, Vector2 direction)
    {
        Vector3 hitZonePosition = (Vector2)transform.position + (direction.normalized * attacDistance);

        _enemy = TargetFinder.FindNearestTarget<IDamageable>(hitZonePosition, attacDistance, _layerAttack);

        if (_coroutine == null)
           _coroutine = StartCoroutine(WaitingAnimationPlay(damage));
    }
     
    private IEnumerator WaitingAnimationPlay(float damage)
    {
        yield return _wait;

        if (_enemy != null)
            _enemy.TakeDamage(damage);

        _coroutine = null;
    }
}
