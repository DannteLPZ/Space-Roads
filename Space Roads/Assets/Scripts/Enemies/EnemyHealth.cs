using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _score;

    private int _currentHealth;

    public int MaxHealth => _maxHealth;

    public int CurrentHealth => _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Die()
    {
        GameManager.Instance.AddScore(_score);

        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0 ) Die();
    }

    public void RestoreHealth(int heal)
    {
        _currentHealth += heal;

        if (_currentHealth > _maxHealth ) _currentHealth = _maxHealth;
    }

}
