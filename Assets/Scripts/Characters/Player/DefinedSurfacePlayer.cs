using System.Collections.Generic;
using UnityEngine;

public class DefinedSurfacePlayer : MonoBehaviour
{
    [SerializeField] private float _surfaceCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _startRayPosition;
    [SerializeField] private float _xStepRayPosition;

    private int _countRays = 3;

    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;

    private void FixedUpdate()
    {
        DefineSurface();
    }

    public void DefineSurface()
    {
        int count = 0;

        for (int i = 0; i < _countRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(
            new Vector3(_startRayPosition.transform.position.x + i * _xStepRayPosition, _startRayPosition.transform.position.y, _startRayPosition.transform.position.z),
            Vector2.down,
            _surfaceCheckDistance,
            _groundLayer);

            if (hit.collider != null)
                count++;
        }

        _isGrounded = count > 0;
    }
}
