using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Vampirism : Ability
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _damage;
    [SerializeField] private float _maxDuration;
    [SerializeField] private float _currentDuration;
    [SerializeField] private float _maxCooldown;
    [SerializeField] private float _currentCooldown;
    [SerializeField] private SpriteRenderer _zone;

    public override event Action<float, float> Changed;

    private List<Collider2D> _hits = new List<Collider2D>();
    private bool _isActive = false;
    private bool _isCooldown = false;
    private float _second = 1;
    private float _targetTime = 0;

    private void Awake()
    {
        _zone.transform.localScale = new Vector3(_radius / transform.lossyScale.x, _radius / transform.lossyScale.y);

        _currentDuration = _maxDuration;
    }

    private void Update()
    {
        if (_currentDuration > 0 && _isActive)
        {
            Collider2D hit = DetectCloserEnemys();

            if (CanDamage() && hit != null)
            {
                hit.TryGetComponent(out IDamageble enemy);
                enemy.TakeDamage(_damage);
            }
        }
        else if (_isActive && _isCooldown == false)
        {
            _isCooldown = true;
            _zone.gameObject.SetActive(false);
            _targetTime = Time.time + _second;
        }

        if (_isCooldown) 
            Recovery();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    //rename
    public bool CanDamage()
    {
        if (Time.time >= _targetTime)
        {
            _targetTime = Time.time + _second;
            _currentDuration -= _second;

            Changed?.Invoke(_currentDuration, _maxDuration);

            return true;
        }

        return false;
    }

    public override void Activation()
    {
        if (_isActive == false && _isCooldown == false)
        {
            _targetTime = Time.time + _second;
            _isActive = true;
            _zone.gameObject.SetActive(true);
        }
    }

    private void Recovery()
    {
        if (Time.time >= _targetTime)
        {
            _targetTime = Time.time + _second;
            _currentCooldown += _second;

            Changed?.Invoke(_currentCooldown, _maxCooldown);
        }
        
        if (_currentCooldown == _maxCooldown)
        {
            _currentCooldown = 0;
            _currentDuration = _maxDuration;
            _isCooldown = false;
            _isActive = false;
        }

    }

    private Collider2D DetectCloserEnemys()
    {
        _hits = Physics2D.OverlapCircleAll(transform.position, _radius, _layer).ToList();

        if (_hits.Count == 0)
            return null;

        Collider2D closer = _hits[0];

        foreach (Collider2D _hit in _hits)
        {
            if ((closer.transform.position - transform.position).sqrMagnitude > (_hit.transform.position - transform.position).sqrMagnitude)
                closer = _hit;
        }

        return closer;
    }
}
