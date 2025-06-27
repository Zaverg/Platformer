using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothSliderHealthBar : HealthView
{
    [SerializeField, Min(0.01f)] private float _speedChange;

    private Coroutine _coroutine;
    private Slider _slider;

    private float _target;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected override void UpdateHealthPointBar(float currentHealth, float maxHealth)
    {
        _target = currentHealth / maxHealth;

        if (_coroutine == null)
            _coroutine = StartCoroutine(StartCangingHealth(_target));
    }

    private IEnumerator StartCangingHealth(float target)
    {
        float startValue = _slider.value;
        float value = 0;

        while (_slider.value != target)
        {
            _slider.value = Mathf.Lerp(startValue, target, value);
            value = Mathf.Clamp01(value + _speedChange * Time.deltaTime);

            yield return null;
        }

        _coroutine = null;
    }
}
