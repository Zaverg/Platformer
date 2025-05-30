using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnCoins : MonoBehaviour
{
    private const string NAME_SURFACE = "Grass";

    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private float _chaseSpawn;
    [SerializeField] private Coin _coin;

    private List<Vector2> _surface;
    private List<Coin> _coins;

    private void Start()
    {
        _surface = new List<Vector2>();
        _coins = new List<Coin>();

        DefineSurface();
        SpawnCoin();
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
                coin.Taking += TakeCoin;

                _coins.Add(coin);
            }
        }
    }

    private void DefineSurface()
    {
        BoundsInt bounds = _tileMap.cellBounds;

        for(int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int positionTile = new Vector3Int(x, y, 0);
                
                TileBase tile = _tileMap.GetTile(positionTile);

                if (tile != null && tile.name == NAME_SURFACE)
                {
                    Vector2 worldPosition = _tileMap.CellToWorld(positionTile);
                    _surface.Add(worldPosition);
                }
            }
        }
    }

    private void TakeCoin(Coin coin)
    {
        coin.Taking -= TakeCoin;

        coin.gameObject.SetActive(false);
    }
} 
