using System;
using UnityEngine;

public class ZoneDetection : MonoBehaviour
{
    public event Action<CharacterController> DetectedPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<CharacterController>(out CharacterController player))
        {
            DetectedPlayer?.Invoke(player);
        }
    }
}
