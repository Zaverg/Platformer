using UnityEngine;

public class DefinedSurfacePlayer : MonoBehaviour
{
    [SerializeField] private float _surfaceCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _startRayPosition;

    public bool DefinedSurface()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector3(_startRayPosition.transform.position.x, _startRayPosition.transform.position.y, _startRayPosition.transform.position.z),
            Vector2.down,
            _surfaceCheckDistance,
            _groundLayer);

        return hit.collider != null;
    }
}
