
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float fireRate;

    private float fireTimer;
    private int projectileCount;
    public int ProjectileCount => projectileCount;

    private void Start()
    {
        fireTimer = 1.0f / fireRate;
        projectileCount=1;
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

    public void SetProjectileCount(int count)
    {
        if(count>3)
            projectileCount = 3;
        else if (count<1)
            projectileCount = 1;
        else
            projectileCount =count;
    }

}
