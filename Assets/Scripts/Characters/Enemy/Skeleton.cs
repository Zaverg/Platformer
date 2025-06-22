using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Skeleton : Enemy
{    
    protected override void TryChangeState()
    {
       base.TryChangeState();
    }
}
