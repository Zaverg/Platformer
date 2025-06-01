using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private DefineSurface _defineSurface;
    [SerializeField] private float _chaseSpawn;
    [SerializeField] private Coin _coin;
    [SerializeField] private float _respawnDelay;

    private List<Coin> _coins;

    private void Awake()
    {
        _coins = new List<Coin>();
    }

    private void Start()
    {
        Spawn();
    }

    private void OnDisable()
    {
        foreach (Coin coin in _coins)
            coin.Took -= DeleteCoin;
    }

    private void Spawn()
    {
        int min = 0;
        int max = 100;

        float coinSpawnPositionX = 0.5f;
        float coinSpawnPositionY = 1.5f;

        List<Vector2> surface = _defineSurface.GetSurface();

        for (int i = 0; i < surface.Count; i++)
        {
            if (Random.Range(min, max) <= _chaseSpawn)
            {
                Coin coin = Instantiate(_coin, new Vector3(surface[i].x + coinSpawnPositionX, surface[i].y + coinSpawnPositionY, 0), Quaternion.identity);
                coin.Took += DeleteCoin;
            }
        }
    }

    private void DeleteCoin(Coin coin)
    {
        coin.gameObject.SetActive(false);
        StartCoroutine(Respawning(coin));
    }

    private IEnumerator Respawning(Coin coin)
    {
        yield return new WaitForSeconds(_respawnDelay);
        coin.gameObject.SetActive(true);
    }
} 
