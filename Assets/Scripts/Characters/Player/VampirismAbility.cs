using System;
using System.Collections;
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

    private IHealebel _health;
    private Coroutine _corutine;

    private void Awake()
    {
        _zoneVisual.transform.localScale = new Vector3(_radius / transform.lossyScale.x, _radius / transform.lossyScale.y);
        _currentDuration = _maxDuration;

        _health = GetComponentInParent<IHealebel>();
    }

    private void DealVimpireDamage(IDamageable enemy)
    {
        if (enemy == null || _health == null)
            return;

        float healthPoint = _damagePerSeconds * Time.deltaTime;

        enemy.TakeDamage(healthPoint);
        _health.Heal(healthPoint); 
    }

    private void UpdateDuration()
    {
        _currentDuration -= Time.deltaTime;
        Changed?.Invoke(_currentDuration, _maxDuration);
    }

    public override void Activate()
    {
        if (_corutine == null)
            _corutine = StartCoroutine(StartAbility());
        
    }

    private void Recovery()
    {
        if (_currentCooldown < _maxCooldown)
        {
            _currentCooldown += Time.deltaTime;

            Changed?.Invoke(_currentCooldown, _maxCooldown);

            return;
        }

        _currentDuration = _maxDuration;
        _currentCooldown = 0;

        StopCoroutine(_corutine);
        _corutine = null;
    }

    private IEnumerator StartAbility() 
    {
        _zoneVisual.gameObject.SetActive(true);

        while (enabled)
        {
            if (_currentDuration > 0)
            {
                UpdateDuration();

                IDamageable enemy = TargetFinder.FindNearestTarget<IDamageable>(transform.position, _radius, _layer);
                DealVimpireDamage(enemy);

                if (_currentDuration <= 0)
                    _zoneVisual.gameObject.SetActive(false);
            }
            else if (_currentDuration <= 0)
            {
                Recovery();
            }

            yield return null;
        }
    }
}
