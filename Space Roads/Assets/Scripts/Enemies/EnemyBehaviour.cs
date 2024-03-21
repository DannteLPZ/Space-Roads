using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject enemyLaserPrefab;

    [SerializeField] private int enemyLive;


    private void Start()
    {
        StartCoroutine(EnemyAttack());
    }

    private IEnumerator EnemyAttack()
    {
        int randomAttackTime = Random.Range(2,7);
        
        yield return new WaitForSeconds(randomAttackTime);

        //Iniciar sonido de ataque.

        Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity);

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
