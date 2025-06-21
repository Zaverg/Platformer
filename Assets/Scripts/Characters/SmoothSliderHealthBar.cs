using System.Collections;
using UnityEngine;

public class SmoothSliderHealthBar : SliderHealthBar
{   
    [SerializeField, Min(0.01f)] private float _speedChange;

    private Coroutine _coroutine;

    protected override void UpdateHealthPointBar(float currentHealth, float maxHealth)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartCangingHP(currentHealth / maxHealth));
    }

    private IEnumerator StartCangingHP(float target)
    {
        float startValue = _slider.value;
        float value = 0;

        while (_slider.value != target)
        {
            _slider.value = Mathf.Lerp(startValue, target, value);
            value = Mathf.Clamp01(value + _speedChange * Time.deltaTime);

            yield return null;
        }
    }
}
