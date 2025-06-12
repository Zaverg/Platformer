using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour, IItemsSpawner
{
    [SerializeField] private Coin _coinPrefab;

    [SerializeField] private int _countCoins;
    [SerializeField] private int _minCountInStep;
    [SerializeField] private int _maxCountInStep;

    [SerializeField] private float _respawnDelay;

    [SerializeField] private int _minIndent;

    [SerializeField] private float _xPositionCoin;
    [SerializeField] private float _yPositionCoin;

    private int _maxIndent;
    private float _maxPracentFillingMap = 80;

    private List<int> _steps;
    private List<Coin> _coins;
    private List<Vector2> _inhabitedSurface;

    private void Awake()
    {
        _steps = new List<int>();
        _inhabitedSurface = new List<Vector2>();
        _coins = new List<Coin>();
    }

    private void OnDisable()
    {
        foreach (Coin coin in _coins)
            coin.Took -= DeleteCoin;
    }

    public List<Vector2> InhabitedSurface(List<Vector2> freeSurface)
    {
        List<Vector2> surface = new List<Vector2>(freeSurface);

        ExceesdedSpawnLimit(surface);
        CreateSteps();

        _maxIndent = (surface.Count - _countCoins) / _steps.Count;

        int startStep = Random.Range(_minIndent, _maxIndent);

        for (int i = 0; i < _steps.Count; i++)
        {
            for (int j = startStep; j < startStep + _steps[i]; j++)
            {
                _inhabitedSurface.Add(surface[j]);
            }

            startStep += _steps[i] + Random.Range(_minIndent, _maxIndent);
        }

        for (int i = 0; i < surface.Count; i++)
        {
            if (_inhabitedSurface.Contains(surface[i]))
            {
                surface.RemoveAt(i);

                i--;
            }
        }

        return surface;
    }

    private void ExceesdedSpawnLimit(List<Vector2> surface)
    {
        float maxCount = surface.Count * _maxPracentFillingMap / 100;

        if (_countCoins > maxCount)
        {
            _countCoins = (int) maxCount;
        }
    }

    private void CreateSteps()
    {
        int remaining = _countCoins;

        while (remaining > 0)
        {
            int maxStep = Mathf.Min(_maxCountInStep, remaining);
            int minStep = Mathf.Min(_minCountInStep, maxStep);

            int stepSize = Random.Range(minStep, maxStep + 1); 

            _steps.Add(stepSize);
            remaining -= stepSize;
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < _inhabitedSurface.Count; i++)
        {
            Coin coin = Instantiate(_coinPrefab, new Vector3(_inhabitedSurface[i].x + _xPositionCoin, _inhabitedSurface[i].y + _yPositionCoin, 0), Quaternion.identity);
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
