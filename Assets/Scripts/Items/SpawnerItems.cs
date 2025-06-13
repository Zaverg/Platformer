using System.Collections.Generic;
using UnityEngine;

public class SpawnerItems : MonoBehaviour
{
    [SerializeField] private SurfaceMapper _defineSurface;
    [SerializeField] private SpawnerCoins _spawnCoins;
    [SerializeField] private SpawnerHealth _spawnHeal;

    private List<Vector2> _freeSurface;

    private void Awake() 
    { 
        _freeSurface = _defineSurface.Define();
    }

    private void Start()
    {
        _freeSurface = _spawnCoins.ReserveTilePositions(_freeSurface);
        _spawnCoins.Spawn();

        _freeSurface = _spawnHeal.InhabitedSurface(_freeSurface);
        _spawnHeal.Spawn();
    }
}
