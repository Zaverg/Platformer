using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour, IEnviroment
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _respawnDelay;

    [SerializeField] private int _stepSpawn;
    [SerializeField] private int _indent;

     private int _countInStep;

    private List<Coin> _coins;
    private List<Vector2> _inhabitedSurface;

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

    public List<Vector2> InhabitedSurface(List<Vector2> freeSurface)
    {
        _inhabitedSurface = new List<Vector2>();
        Debug.Log(freeSurface.Count);
        _countInStep = (freeSurface.Count - _indent * _stepSpawn) / _stepSpawn;

        int temp = _indent;

        for (int i = 1; i <= _stepSpawn; i++)
        {
            for (int j = temp; j < temp + _countInStep && j < freeSurface.Count; j++)
            {
                _inhabitedSurface.Add(freeSurface[j]);
            }

            temp += _countInStep + _indent;
        }

        for (int i = 0; i < freeSurface.Count; i++)
        {
            if (_inhabitedSurface.Contains(freeSurface[i]))
            {
                freeSurface.RemoveAt(i);

                i--;
            }
        }

        return freeSurface;
    }

    private void Spawn()
    {
        for (int i = 0; i < _inhabitedSurface.Count;  i++)
        {
            Coin coin = Instantiate(_coinPrefab, new Vector3(_inhabitedSurface[i].x + 0.5f, _inhabitedSurface[i].y + 1.5f, 0), Quaternion.identity);
            coin.Took += DeleteCoin;

            _coins.Add(coin);
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
