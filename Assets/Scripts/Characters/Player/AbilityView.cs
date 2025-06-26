using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AbilityView : MonoBehaviour
{
    [SerializeField] private Ability _ability;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _ability.Changed += UpdateAbilityBar;
    }

    private void OnDisable()
    {
        _ability.Changed -= UpdateAbilityBar;
    }

    private void UpdateAbilityBar(float second, float max)
    {
        _slider.value = second / max;
    }
}
