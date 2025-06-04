using UnityEngine;

public class MoverEnemy : MonoBehaviour
{
    private Transform _currentTarget;
    private float _speed;

    private void Awake()
    {
        _currentTarget = null;    
    }

    public void Move()
    {
        if (_currentTarget != null) 
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget.transform.position, _speed * Time.deltaTime);
    }

    public void SetTarget(Transform target)
    {
       _currentTarget = target;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
