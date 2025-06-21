using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderHealthBar : HealthView
{
    protected Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected override void UpdateHealthPointBar(float currentHP, float maxHP)
    {
        _slider.value = currentHP / maxHP;
    }
}
