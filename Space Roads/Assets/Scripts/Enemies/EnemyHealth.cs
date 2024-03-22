using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _score;

    [Header("Game Events")]
    [SerializeField] private GameEvent _onEnemyDestroyed;

    private int _currentHealth;

    public int MaxHealth => _maxHealth;

    public int CurrentHealth => _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Die()
    {
        if(GameManager.Instance != null)
            GameManager.Instance.AddScore(_score);

        _onEnemyDestroyed.Invoke();
        //Efectos y sonidos de muerte

        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        //Efectos y sonido de daño

        if (_currentHealth <= 0 ) Die();
    }

    public void RestoreHealth(int heal)
    {
        _currentHealth += heal;

        if (_currentHealth > _maxHealth ) _currentHealth = _maxHealth;
    }

}
