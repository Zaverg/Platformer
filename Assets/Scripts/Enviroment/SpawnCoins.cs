using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private DefineSurface _defineSurface;
    [SerializeField] private float _chaseSpawn;
    [SerializeField] private Coin _coin;
    [SerializeField] private float _respawnDelay;

    private List<Vector2> _surface;

    private List<Coin> _coins;
    private List<Coin> _respawnCoins;

    private void Start()
    {
        _surface = new List<Vector2>();
        _coins = new List<Coin>();
        _respawnCoins = new List<Coin>();
        _surface = _defineSurface.GetSurface();

        SpawnCoin();
    }

    private void Update()
    { 
        RespawnCoin();
    }

    private void SpawnCoin()
    {
        int min = 0;
        int max = 100;

        float coinSpawnPositionX = 0.5f;
        float coinSpawnPositionY = 1.5f;

        for (int i = 0; i < _surface.Count; i++)
        {
            if (Random.Range(min, max) <= _chaseSpawn)
            {
                Coin coin = Instantiate(_coin, new Vector3(_surface[i].x + coinSpawnPositionX, _surface[i].y + coinSpawnPositionY, 0), Quaternion.identity);
                coin.Taking += DeleteCoin;

                _coins.Add(coin);
            }
        }
    }

    private void DeleteCoin(Coin coin)
    {
        _respawnCoins.Add(coin);
        coin.gameObject.SetActive(false);
    }

    private void RespawnCoin()
    {
        if (_respawnCoins.Count == 0)
            return;

        for (int i = 0; i < _respawnCoins.Count; i++)
        {
            if (_respawnCoins[i].TimeTake + _respawnDelay <= Time.time)
            {
                _respawnCoins[i].gameObject.SetActive(true);
                _respawnCoins.RemoveAt(i);
            }
        }
    }
} 
