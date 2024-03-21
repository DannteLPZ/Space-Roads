using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;

    [SerializeField] private int enemyLive;


    private void Start()
    {
        StartCoroutine(EnemyAttack());
    }

    private IEnumerator EnemyAttack()
    {
        int randomAttackTime = Random.Range(enemyType.minFireRate,enemyType.maxFireRate);
        
        yield return new WaitForSeconds(randomAttackTime);

        //Iniciar sonido de ataque.

        Instantiate(enemyType.laserPrefab, transform.position, Quaternion.identity);

        StartCoroutine(EnemyAttack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Revisar cuando el jugador acerta un disparo
        //Update score, Restar vida, iniciar sonido
        //Si vida es menor a cero, destroy
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
