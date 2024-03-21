using System.Collections;
using UnityEngine;

public class EnemyLaserBehaviour : MonoBehaviour
{

    [SerializeField] private float laserSpeed;
    
    public int AttackDamage;

    private void Start()
    {
        StartCoroutine(LiveTimeRoutine());
    }

    private void Update()
    {
        transform.Translate( - 1.0f * Vector2.up * laserSpeed * Time.deltaTime);
    }

    private IEnumerator LiveTimeRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }

}
