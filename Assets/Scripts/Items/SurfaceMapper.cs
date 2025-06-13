using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SurfaceMapper : MonoBehaviour
{
    private const string NameSurface = "Grass";

    [SerializeField] private Tilemap _tileMap;
    private List<Vector2> _surface;

    public List<Vector2> Define()
    {
        BoundsInt bounds = _tileMap.cellBounds;
        _surface = new List<Vector2>();

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int positionTile = new Vector3Int(x, y, 0);

                TileBase tile = _tileMap.GetTile(positionTile);

                if (tile != null && tile.name == NameSurface)
                {
                    Vector2 worldPosition = _tileMap.CellToWorld(positionTile);
                    _surface.Add(worldPosition);
                }
            }
        }

        return new List<Vector2>(_surface);
    }

    public List<Vector2> GetSurface() =>
        new List<Vector2>(_surface);
}
