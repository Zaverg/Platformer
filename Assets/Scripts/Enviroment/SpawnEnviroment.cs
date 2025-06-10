using System.Collections.Generic;
using UnityEngine;

public class SpawnEnviroment : MonoBehaviour
{
    [SerializeField] private DefineSurface _defineSurface;
    [SerializeField] private SpawnCoins2 _spawnCoins;

   // [SerializeField] private List<IEnviroment> _enviroment;

    private List<Vector2> _freeSurface;

    private void Awake() 
    { 
        _freeSurface = _defineSurface.Define();
    }

    private void Start()
    {
        _freeSurface = _spawnCoins.InhabitedSurface(_freeSurface);
        _spawnCoins.Spawn();
    }

    private void Spawn()
    {
        
    }  
}
