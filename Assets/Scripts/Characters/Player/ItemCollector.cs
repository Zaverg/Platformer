using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITaking enviroment))
        {
            enviroment.Take(_player);
        }
    }
}
