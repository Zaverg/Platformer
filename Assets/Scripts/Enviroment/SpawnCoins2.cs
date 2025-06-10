using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnCoins2 : MonoBehaviour, IEnviroment
{
    [SerializeField] private Coin _coinPrefab;

    [SerializeField] private int _countCoins;
    [SerializeField] private int _minCountInStep;
    [SerializeField] private int _maxCountInStep;

    [SerializeField] private float _respawnDelay;

    [SerializeField] private int _minIndent;
    [SerializeField] private int _maxIndent;

    private float _maxPracentFillingMap = 80;

    [SerializeField]  private List<int> _steps;

    [SerializeField] private List<Coin> _coins;
    [SerializeField] private List<Vector2> _inhabitedSurface;

    private void Awake()
    {
        _steps = new List<int>();
        _inhabitedSurface = new List<Vector2>();
        _coins = new List<Coin>();
    }

    private void Start()
    {
     
    }

    private void OnDisable()
    {
        foreach (Coin coin in _coins)
            coin.Took -= DeleteCoin;
    }

    public List<Vector2> InhabitedSurface(List<Vector2> freeSurface)
    {
        CheckSpawnLimit(freeSurface);
        CreateSteps();

        _maxIndent = (freeSurface.Count - _countCoins) / _steps.Count;

        int startStep = Random.Range(_minIndent, _maxIndent);

        for (int i = 0; i < _steps.Count; i++)
        {
            for (int j = startStep; j < startStep + _steps[i]; j++)
            {
                _inhabitedSurface.Add(freeSurface[j]);
            }

            startStep += _steps[i] + Random.Range(_minIndent, _maxIndent);
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

    //rename
    private void CheckSpawnLimit(List<Vector2> freeSurface)
    {
        float maxCount = freeSurface.Count * _maxPracentFillingMap / 100;

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
