using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
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

    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;
    public Vector2 DirectionMove => _directionMove;

    private void Awake()
    {
        _hits = new List<RaycastHit2D>();
        _xStepRayPosition = _xStepRayPosition / (_countRays - 1);
    }

    private void FixedUpdate()
    {
        DefineSurface();
        DefineAngel();
    }

    public void DefineSurface()
    {
        for (int i = 0; i < _countRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(
            new Vector3(_startRayPosition.transform.position.x + i * _xStepRayPosition, _startRayPosition.transform.position.y, _startRayPosition.transform.position.z),
            Vector2.down,
            _surfaceCheckDistance,
            _groundLayer);

            Debug.DrawRay(hit.point, hit.normal, Color.red, 0.1f);

            if (hit.collider != null)
                _hits.Add(hit);
        }


        _isGrounded = _hits.Count > 0;
    }

    private void DefineAngel()
    {
        float maxAngel = -1;
        Vector2 normal = new Vector2(0, 0);

        foreach (RaycastHit2D hit in _hits)
        {
            float angle = Vector2.Angle(Vector2.up, hit.normal);

            if (angle > maxAngel)
            {
                maxAngel = angle;
                normal = hit.normal;
            }
        }

        _directionMove = new Vector2(normal.y, -normal.x).normalized;
    }
}
