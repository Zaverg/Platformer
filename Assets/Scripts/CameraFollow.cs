using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
 
    private void LateUpdate()
    {
        Vector3 target = new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, _speed * Time.deltaTime);
    }
}
