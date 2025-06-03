using System.Collections.Generic;
using UnityEngine;

public class DefinedSurfacePlayer : MonoBehaviour
{
    [SerializeField] private float _surfaceCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _startRayPosition;
    [SerializeField] private float _xStepRayPosition;

    private int _countRays = 3;
    private List<RaycastHit2D> _hits;

    private Vector2 _directionMove;
    private Vector2 _averageNormal;

    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;
    public Vector2 DirectionMove => _directionMove;
    public Vector2 AverageNormal => _averageNormal;

    private void Awake()
    {
        _hits = new List<RaycastHit2D>();
    }

    private void FixedUpdate()
    {
        DefineSurface();
    }

    public Vector2 DefineAngel(float inputDirection)
    {
        if (_hits.Count == 0)
        {
            _directionMove = Vector2.right;
            return Vector2.zero;
        }

        _averageNormal = Vector2.zero;
        RaycastHit2D guidenHit = _hits[_hits.Count / 2];
        RaycastHit2D mainHit = _hits[_hits.Count / 2];

        if (inputDirection > 0)
            guidenHit = _hits[_hits.Count - 1];
        else if (inputDirection < 0)
            guidenHit = _hits[0];

        Vector2 avergeNormal = (guidenHit.normal + mainHit.normal) / 2;

        _directionMove = new Vector2(avergeNormal.y, -avergeNormal.x).normalized;

        Debug.DrawRay(transform.position, _directionMove * 2f, Color.green, 0.1f);

        return avergeNormal;
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

            Debug.DrawRay(rayStart, hit.normal, Color.red, 0.1f);

            Debug.DrawRay(rayStart, Vector2.down * _surfaceCheckDistance, Color.blue, 0.1f);

            if (hit.collider != null)
            {
                _hits.Add(hit);
                
                Debug.DrawRay(hit.point, hit.normal, Color.red, 0.1f);
            }
        }


        _isGrounded = _hits.Count > 0;
    }
}
