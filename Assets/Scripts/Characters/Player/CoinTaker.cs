using UnityEngine;

public class CoinTaker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
            coin.Take();
    }
}
