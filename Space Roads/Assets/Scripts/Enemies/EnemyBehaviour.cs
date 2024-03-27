using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;

    private float _attackTimer;

    private void Start()
    {
        _attackTimer = Random.Range(enemyType.minFireRate, enemyType.maxFireRate);
    }

    private void Update()
    {
        _attackTimer -= Time.deltaTime;
        if( _attackTimer <= 0.0f)
        {
            AudioManager.Instance.Play("SFX_EnemyShot");
            Instantiate(enemyType.laserPrefab, transform.position, transform.rotation);
            _attackTimer = Random.Range(enemyType.minFireRate, enemyType.maxFireRate);
        }
    }
}
