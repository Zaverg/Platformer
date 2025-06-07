using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerAttack;

    private Collider2D _hit;

    Vector3 hitZonePosition;
    Vector3 sizeZone;

    public void Attack(float damage, float attacDistance, Vector2 direction)
    {
        hitZonePosition = (Vector2)transform.position + (direction.normalized * attacDistance);
        sizeZone = new Vector2(2, 2);

        _hit = Physics2D.OverlapBox(hitZonePosition, sizeZone, 0, _layerAttack);
        Debug.Log("Hit: " + _hit);

        if (_hit != null && _hit.TryGetComponent(out IDamageble character))
            character.TakeDamage(damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitZonePosition, sizeZone);
    }
}
