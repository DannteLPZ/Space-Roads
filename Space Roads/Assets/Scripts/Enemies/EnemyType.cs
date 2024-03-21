using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "EnemyType")]
public class EnemyType : ScriptableObject
{
    public GameObject laserPrefab;

    public int minFireRate;

    public int maxFireRate;
}
