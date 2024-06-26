using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [Header("Health")]
    [SerializeField] private int _maxHealth;

    [Header("Events")]
    [SerializeField] private GameEvent _onHealthChange;
    [SerializeField] private GameEvent _onDead;

    private int _currentHealth;

    public int MaxHealth => _maxHealth;

    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void Die() => _onDead.Invoke();

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _onHealthChange.Invoke();

        if (_currentHealth <= 0) Die();
    }

    public void RestoreHealth(int heal)
    {
        _currentHealth += heal;
        _onHealthChange.Invoke();
        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
    }
}
