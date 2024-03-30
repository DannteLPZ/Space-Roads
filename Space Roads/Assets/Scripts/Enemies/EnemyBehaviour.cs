using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    private float _attackTimer;

    private void Start()
    {
        _attackTimer = Random.Range(enemyType.MinFireRate, enemyType.MaxFireRate);
    }

    private void Update()
    {
        _attackTimer -= Time.deltaTime;
        if( _attackTimer <= 0.0f)
        {
            for (int i = 0; i < enemyType.Projectiles; i++)
            {    
                float posX = (2.0f * enemyType.ProjectileOffset.x * i) - (enemyType.ProjectileOffset.x * (enemyType.Projectiles - 1));
                float posY = -Mathf.Pow(i - 1, 2) * enemyType.ProjectileOffset.y;
                GameObject bullet = Instantiate(enemyType.LaserPrefab, transform.position, transform.rotation, transform);
                bullet.transform.localPosition = new Vector3(posX, posY);
                bullet.transform.SetParent(null);
            }
            AudioManager.Instance.Play("SFX_EnemyShot");
            _attackTimer = Random.Range(enemyType.MinFireRate, enemyType.MaxFireRate);
        }
    }
}
