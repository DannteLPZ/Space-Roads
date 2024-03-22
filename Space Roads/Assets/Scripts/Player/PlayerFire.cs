
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float fireRate;

    private float fireTimer;

    private void Start()
    {
        fireTimer = 1.0f / fireRate;
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;
        if(Input.GetMouseButton(0) == true && fireTimer <= 0)
        {
            Instantiate(playerBullet, firingPoint.position, transform.rotation);
            fireTimer = 1.0f / fireRate;
        }    
    }

}
