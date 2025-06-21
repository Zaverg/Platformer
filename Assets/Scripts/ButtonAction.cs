using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonAction : MonoBehaviour
{
    [SerializeField] protected Health _health;
    [SerializeField] protected Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    protected abstract void OnClick();
}
