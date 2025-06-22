using UnityEngine;

public class SpriteTurn : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private void Update()
    {
        if (_rigidbody.velocity.x != 0)
            TurnInDirection(_rigidbody.velocity.x);    
    }

    private void TurnInDirection(float inputDirection)
    {
        float rigth = 0;
        float left = 180;

        float degrees = inputDirection > 0 ? rigth : left;

        transform.rotation = Quaternion.Euler(0, degrees, 0);
    }
}
