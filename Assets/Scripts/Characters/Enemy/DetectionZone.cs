using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DetectionZone : MonoBehaviour
{
    private const int Inverted = -1;

    [SerializeField] private Transform _spriteCharacter;
    public event Action<Player> Detected;

    private float _currentDirection;

    private void Start()
    {
        _currentDirection = _spriteCharacter.rotation.y;
    }

    private void Update()
    {
        if (_spriteCharacter.rotation.y != _currentDirection)
        {
            _currentDirection = _spriteCharacter.rotation.y;
            transform.localPosition = new Vector2(transform.localPosition.x * Inverted, transform.localPosition.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player palyer))
            Detected?.Invoke(palyer);
    }
}
