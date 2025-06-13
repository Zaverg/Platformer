using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealth : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private HealthPotion _healPrefab;

    [SerializeField] private float _respawnDelay;

    [SerializeField] private float _xPositionHeal;
    [SerializeField] private float _yPositionHeal;
 
    private List<HealthPotion> _heals;
    private List<Vector2> _inhabitedSurface;

    private void Awake()
    {
        _heals = new List<HealthPotion>();
        _inhabitedSurface = new List<Vector2>();
    }

    private void OnDisable()
    {
        foreach (HealthPotion heal in _heals)
            heal.Took -= DeleteHeal;
    }

    public List<Vector2> InhabitedSurface(List<Vector2> freeSurface)
    {
        List<Vector2> surface = new List<Vector2>(freeSurface);

        if (_count > surface.Count)
            _count = surface.Count;

        int stepSize = surface.Count / _count;

        for (int i = 0; i < _count; i++)
        {
            int start = stepSize * i;
            int end = start + stepSize;

            int index = Random.Range(start, end);

            _inhabitedSurface.Add(surface[index]);
        }

        for (int i = 0; i < surface.Count; i++)
        {
            if (_inhabitedSurface.Contains(surface[i]))
            {
                surface.RemoveAt(i);

                i--;
            }
        }

        return surface;
    }

    public void Spawn()
    {
        for (int i = 0; i < _inhabitedSurface.Count; i++)
        {
            HealthPotion heal = Instantiate(_healPrefab, new Vector3(_inhabitedSurface[i].x + _xPositionHeal, _inhabitedSurface[i].y + _yPositionHeal, 0), Quaternion.identity);
            heal.Took += DeleteHeal;

            _heals.Add(heal);
        }
    }

    private void DeleteHeal(HealthPotion heal)
    {
        heal.gameObject.SetActive(false);
        StartCoroutine(Respawning(heal));
    }

    private IEnumerator Respawning(HealthPotion heal)
    {
        yield return new WaitForSeconds(_respawnDelay);
        heal.gameObject.SetActive(true);
    }
}
