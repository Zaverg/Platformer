using UnityEngine;

public enum State
{
    Patrol,
    Chase
}

public class EnemyThornsMover : MonoBehaviour
{
    [SerializeField] private Patrol _patrol;
    [SerializeField] private Chase _chase;
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private DefinedSurfacePlayer _definedSurface;

    private Transform _currentTarget;

    private void OnEnable()
    {
        
    }

    private void Update()
    {
        _currentTarget = _patrol.CurrentTarget;
        Vector3 target = new Vector3(_currentTarget.position.x, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target, _patrol.Speed * Time.deltaTime);
    }

    private void ChangeState(Transform target)
    {
         
    }
}