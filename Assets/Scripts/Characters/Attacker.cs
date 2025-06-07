using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerAttack;

    private Collider2D _hit;
   
    public void Attack(float damage, float attacDistance)
    {
        Vector2 hitZonePosition = new Vector2(attacDistance, attacDistance);
        _hit = Physics2D.OverlapArea(transform.position, hitZonePosition, _layerAttack);

        if (_hit != null && _hit.TryGetComponent(out IDamageble character))
            character.TakeDamage(damage);
    }
}
