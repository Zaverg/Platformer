using System.Runtime.CompilerServices;
using UnityEngine;

public class DefinedSurfacePlayer : MonoBehaviour
{
    [SerializeField] private float _surfaceCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _startRayPosition;

    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;

    private void FixedUpdate()
    {
        DefineSurface();
    }

    public void DefineSurface()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector3(_startRayPosition.transform.position.x, _startRayPosition.transform.position.y, _startRayPosition.transform.position.z),
            Vector2.down,
            _surfaceCheckDistance,
            _groundLayer);

        _isGrounded = hit.collider != null;
    }
}
