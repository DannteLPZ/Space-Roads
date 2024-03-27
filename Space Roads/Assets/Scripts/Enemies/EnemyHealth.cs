using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [Header("Health")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _score;
    [SerializeField] private Gradient _healthGradient;
    [SerializeField] private Image _healthImage;

    [Header("Game Events")]
    [SerializeField] private GameEvent _onEnemyDestroyed;

    private int _currentHealth;

    public int MaxHealth => _maxHealth;

    public int CurrentHealth => _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
        _healthImage.fillAmount = 1.0f;
        _healthImage.color = _healthGradient.Evaluate(1.0f);
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
        float ratio = (float)_currentHealth / _maxHealth;
        _healthImage.fillAmount = ratio;
        _healthImage.color = _healthGradient.Evaluate(ratio);
        //Efectos y sonido de daño

        if (_currentHealth <= 0 ) Die();
    }

    public void RestoreHealth(int heal)
    {
        _currentHealth += heal;

        if (_currentHealth > _maxHealth ) _currentHealth = _maxHealth;
    }

}
