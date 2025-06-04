using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DetectionZone : MonoBehaviour
{
    public event Action<Player> Detected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player palyer))
            Detected?.Invoke(palyer);
    }
}
