using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private DefineSurface _defineSurface;
    [SerializeField] private SpawnCoins _spawnCoins;
    [SerializeField] private SpawnHealth _spawnHeal;

    private List<Vector2> _freeSurface;

    private void Awake() 
    { 
        _freeSurface = _defineSurface.Define();
    }

    private void Start()
    {
        _freeSurface = _spawnCoins.InhabitedSurface(_freeSurface);
        _spawnCoins.Spawn();

        _freeSurface = _spawnHeal.InhabitedSurface(_freeSurface);
        _spawnHeal.Spawn();
    }
}
