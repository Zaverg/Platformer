using System;
using UnityEngine;

public class ZoneDetection : MonoBehaviour
{
    [SerializeField] private float _timeToLose;

    private PlayerController _target;
    private Coroutine _loseTarget;
    private bool _isCoroutineRunning;

    public event Action<PlayerController> DetectedPlayer;

    public bool Detected { get; private set; }
    public PlayerController Target => _target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            Detected = true;
            DetectedPlayer?.Invoke(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_target != null && collision.gameObject == _target.gameObject)
        {
            Detected = false;
        }
    }
}
