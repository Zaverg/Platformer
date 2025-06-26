using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerAttack;

    private Collider2D _hit;

    private Vector3 _hitZonePosition;
    private Vector3 _sizeZone;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private void Awake()
    {
        _wait = new WaitForSeconds(0);
    }

    public void Attack(float damage, float attacDistance, Vector2 direction)
    {
        _hitZonePosition = (Vector2)transform.position + (direction.normalized * attacDistance);
        _sizeZone = new Vector2(attacDistance, attacDistance);

        float angel = 0;

        _hit = Physics2D.OverlapBox(_hitZonePosition, _sizeZone, angel, _layerAttack);

        if (_coroutine == null)
           _coroutine = StartCoroutine(WaitingAnimationPlay(damage));
        
    }

    public void SetTimeToWait(float seconds)
    {
        _wait = new WaitForSeconds(seconds);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_hitZonePosition, _sizeZone);
    }
     
    private IEnumerator WaitingAnimationPlay(float damage)
    {
        yield return _wait;

        if (_hit != null && _hit.TryGetComponent(out IDamageble character))
            character.TakeDamage(damage);

        _coroutine = null;
    }
}
