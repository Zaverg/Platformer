using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VampirismAbility : Ability
{
    public override event Action<float, float> Changed;

    [Header("Area Settings")]
    [SerializeField] private float _radius = 3f;
    [SerializeField] private SpriteRenderer _zoneVisual;

    [Header("Damage Settings")]
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _damagePerSeconds;

    [Header("Duration & Cooldown")]
    [SerializeField] private float _maxDuration;
    [SerializeField] private float _maxCooldown;

    private float _currentDuration;
    private float _currentCooldown;

    private List<Collider2D> _hits = new List<Collider2D>();

    private bool _isActive = false;
    private bool _isOnCooldown = false;

    private IHealebel _health;

    private void Awake()
    {
        _zoneVisual.transform.localScale = new Vector3(_radius / transform.lossyScale.x, _radius / transform.lossyScale.y);
        _currentDuration = _maxDuration;

        _health = GetComponentInParent<IHealebel>();
    }

    private void Update()
    {
        if (_isActive)
        {
            UpdateTimer();

            IDamageable enemy = FindNearestEnemy();
            DealVimpireDamage(enemy);
        }

        if (_isOnCooldown)
            Recovery();
    }

    private void DealVimpireDamage(IDamageable enemy)
    {
        if (enemy == null || _health == null)
            return;

        float healthPoint = _damagePerSeconds * Time.deltaTime;

        enemy.TakeDamage(healthPoint);
        _health.Heal(healthPoint); 
    }

    private void UpdateTimer()
    {
        if (_currentDuration > 0)
        {
            _currentDuration -= Time.deltaTime;
            Changed?.Invoke(_currentDuration, _maxDuration);

            return;
        }

        _isOnCooldown = true;
        _isActive = false;
        _zoneVisual.gameObject.SetActive(false);
    }

    public override void Activate()
    {
        if (_isActive == false && _isOnCooldown == false)
        {
            _isActive = true;
            _zoneVisual.gameObject.SetActive(true);
        }
    }

    private void Recovery()
    {
        if (_currentCooldown <  _maxCooldown)
        {
            _currentCooldown += Time.deltaTime;

            Changed?.Invoke(_currentCooldown, _maxCooldown);

            return;
        }

        _isOnCooldown = false;
        _currentDuration = _maxDuration;
        _currentCooldown = 0;
    }

    private IDamageable FindNearestEnemy()
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

        closer.TryGetComponent(out IDamageable enemy);

        return enemy;
    }
}
