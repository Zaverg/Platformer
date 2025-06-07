using UnityEngine;

public class Health : MonoBehaviour, IDamageble
{
   [SerializeField] private float _maxHealth;

   [SerializeField] private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_currentHealth - damage >= 0)
        {
            _currentHealth -= damage;
        }
        else
        {
            _currentHealth = 0;
        }

        Debug.Log(gameObject.name + " take damage: " + damage + " HP: " + _currentHealth);
        TryDie();
    }

    private void TryDie()
    {
        if (_currentHealth <= 0)
            gameObject.SetActive(false);
    }
}
