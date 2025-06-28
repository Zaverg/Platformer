using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    private static Collider2D[] _hits = new Collider2D[10];

    public static T FindNearestTarget<T>(Vector3 position, float radius, LayerMask layer) where T : class
    {
        int count = Physics2D.OverlapCircleNonAlloc(position, radius, _hits, layer);

        if (count == 0)
            return null;

        T closer = null;
        float minDistance = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (_hits[i].TryGetComponent(out T target) == false)
                continue;

            float distance = (position - _hits[i].transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                closer = target;
            }
        }

        return closer;
    }
}
