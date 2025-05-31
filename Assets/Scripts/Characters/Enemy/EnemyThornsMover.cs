using UnityEngine;

public class EnemyThornsMover : MonoBehaviour
{
    [SerializeField] private Patrol _patrol;

    private Transform _currentTarget;

    private void Update()
    {
        _currentTarget = _patrol.CurrentTarget;
        Vector3 target = new Vector3(_currentTarget.position.x, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target, _patrol.Speed * Time.deltaTime);
    }
}