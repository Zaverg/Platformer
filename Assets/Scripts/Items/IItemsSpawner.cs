using System.Collections.Generic;
using UnityEngine;

public interface IItemsSpawner
{
    public List<Vector2> ReserveTilePositions(List<Vector2> freeSurface);
    public void Spawn();
}
