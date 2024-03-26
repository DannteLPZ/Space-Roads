using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;

    private void Start()
    {
        StartCoroutine(EnemyAttack());
    }

    private IEnumerator EnemyAttack()
    {
        int randomAttackTime = Random.Range(enemyType.minFireRate,enemyType.maxFireRate);
        
        yield return new WaitForSeconds(randomAttackTime);

        //Iniciar sonido de ataque.
        AudioManager.Instance.Play("SFX_EnemyShot");
        Instantiate(enemyType.laserPrefab, transform.position, transform.rotation);

        StartCoroutine(EnemyAttack());
    }

}
