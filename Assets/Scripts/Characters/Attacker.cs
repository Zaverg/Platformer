using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerAttack;

    private Collider2D _hit;

    private Vector3 hitZonePosition;
    private Vector3 sizeZone;

    public void Attack(float damage, float attacDistance, Vector2 direction)
    {
        hitZonePosition = (Vector2)transform.position + (direction.normalized * attacDistance);
        sizeZone = new Vector2(attacDistance, attacDistance);

        float angel = 0;

        _hit = Physics2D.OverlapBox(hitZonePosition, sizeZone, angel, _layerAttack);

        if (_hit != null && _hit.TryGetComponent(out IDamageble character))
            character.TakeDamage(damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitZonePosition, sizeZone);
    }
}
