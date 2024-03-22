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

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Die()
    {

        //Efectos y sonidos de muerte

        _onDead.Invoke();

        Debug.Log("A mimir");
        //Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        _onHealthChange.Invoke();

        //Efectos y sonido de daño

        if (_currentHealth <= 0) Die();
    }

    public void RestoreHealth(int heal)
    {
        _currentHealth += heal;

        _onHealthChange.Invoke();

        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
    }
}
