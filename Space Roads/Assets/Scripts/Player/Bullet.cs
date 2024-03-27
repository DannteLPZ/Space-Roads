using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    [SerializeField] private Rigidbody2D bulletRb;
    [SerializeField] private int bulletDamage;
    [SerializeField] private GameObject bulletParticles;


    private void Start()
    {
        bulletRb.AddForce(speed * transform.up, ForceMode2D.Impulse);
        Destroy(gameObject, 4.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != gameObject.layer)
        {
            collision.TryGetComponent(out IHealth health);

            health?.TakeDamage(bulletDamage);

            DestroyBullet();
        }      
    }

    public void DestroyBullet() => Destroy(gameObject);

    private void OnDestroy() => Instantiate(bulletParticles, transform.position, Quaternion.identity);
}
