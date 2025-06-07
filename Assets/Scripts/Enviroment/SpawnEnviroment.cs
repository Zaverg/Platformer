using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnviroment : MonoBehaviour
{
    [SerializeField] private DefineSurface _defineSurface;
    [SerializeField] private SpawnCoins _spawnCoins;
    

    private List<Vector2> _freeSurface;

    private void Awake() 
    {
        _freeSurface = _defineSurface.GetSurface();
    }
}
