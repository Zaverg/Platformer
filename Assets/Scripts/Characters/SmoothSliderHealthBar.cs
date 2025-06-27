using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothSliderHealthBar : HealthView
{
    [SerializeField, Min(0.01f)] private float _speedChange;

    private Coroutine _coroutine;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected override void UpdateHealthPointBar(float currentHealth, float maxHealth)
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(StartCangingHealth(currentHealth / maxHealth));
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
