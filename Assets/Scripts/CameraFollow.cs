using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
 
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Following();
    }

    private void Following()
    {
        Vector3 target = new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, _speed * Time.deltaTime); 
    }
}
