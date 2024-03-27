using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "EnemyType")]
public class EnemyType : ScriptableObject
{
    public GameObject LaserPrefab;

    public float MinFireRate;

    public float MaxFireRate;

    public float Projectiles;

    public Vector2 ProjectileOffset;
}
