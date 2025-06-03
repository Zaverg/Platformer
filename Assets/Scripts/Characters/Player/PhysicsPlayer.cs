using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsPlayer : MonoBehaviour
{
    [SerializeField] private float _stickForce;
    [SerializeField] private float _maxForceDrag;
    [SerializeField] private float _minForceDrag;

    private Rigidbody2D _rigidbody2D;
    private float _forceDrag;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    public void StickToSurface(bool isMove)
    {
        if(isMove)
            _rigidbody2D.AddForce(Vector2.down *  _stickForce);
    }
}
