using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DetectionZone : MonoBehaviour
{
    private const int Inverted = -1;

    [SerializeField] private SpriteRenderer _spriteCharacter;
    public event Action<Player> Detected;

    private bool _isFlip;

    private void Start()
    {
        _isFlip = _spriteCharacter.flipX;
    }

    private void Update()
    {
        if (_spriteCharacter.flipX != _isFlip)
        {
            _isFlip = _spriteCharacter.flipX;
            transform.localPosition = new Vector2(transform.localPosition.x * Inverted, transform.localPosition.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player palyer))
            Detected?.Invoke(palyer);
    }
}
