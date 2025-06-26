using UnityEngine;

public class SpriteTurn : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private float _turnZone = 0.1f;

    private void Update()
    {
        TurnInDirection(_rigidbody.velocity.x);    
    }

    private void TurnInDirection(float moveDirection)
    {
        if (Mathf.Abs(moveDirection) < _turnZone)
            return;

        float targetRotationY = moveDirection > 0 ? 0f : 180f;

        transform.rotation = Quaternion.Euler(0, targetRotationY, 0);
    }
}
