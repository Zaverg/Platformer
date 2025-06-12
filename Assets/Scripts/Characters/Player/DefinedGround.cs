using System.Collections.Generic;
using UnityEngine;

public class DefinedGround  : MonoBehaviour
{
    [SerializeField] private float _surfaceCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _startRayPosition;
    [SerializeField] private float _xStepRayPosition;

    private int _countRays = 3;
    private List<RaycastHit2D> _hits;

    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _hits = new List<RaycastHit2D>();
    }

    private void FixedUpdate()
    {
        DefineSurface();
    }

    private void DefineSurface()
    {
        _hits.Clear();

        for (int i = 0; i < _countRays; i++)
        {
            Vector3 rayStart = new Vector3(
                _startRayPosition.position.x + i * _xStepRayPosition,
                _startRayPosition.position.y,
                _startRayPosition.position.z
            );

            RaycastHit2D hit = Physics2D.Raycast(
            new Vector3(_startRayPosition.transform.position.x + i * _xStepRayPosition, _startRayPosition.transform.position.y, _startRayPosition.transform.position.z),
            Vector2.down,
            _surfaceCheckDistance,
            _groundLayer);

            if (hit.collider != null)
            {
                _hits.Add(hit);
            }
        }

        _isGrounded = _hits.Count > 0;
    }
}
