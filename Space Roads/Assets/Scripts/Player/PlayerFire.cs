using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float fireRate;
    [SerializeField] private int _maxProjectiles;
    [SerializeField] private Vector2 _projectileOffset;

    private float fireTimer;

    private int projectileCount;
    public int ProjectileCount => projectileCount;

    private void Start()
    {
        fireTimer = 1.0f / fireRate;
        projectileCount = 1;
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;
        if(Input.GetMouseButton(0) == true && fireTimer <= 0)
        {
            Fire(); 
            fireTimer = 1.0f / fireRate;
        }    
    }

    private void Fire()
    {
        for (int i = 0; i < _maxProjectiles; i++)
        {
            if(projectileCount == 1)
                if (i == 0 || i == 2) continue;
            if (projectileCount == 2)
                if (i == 1) continue;
            float posX = (i - 1) * _projectileOffset.x;
            float posY = -Mathf.Pow(i - 1, 2) * _projectileOffset.y;
            Instantiate(playerBullet, (Vector2)firingPoint.position + new Vector2(posX, posY), transform.rotation);
        }    
    }

    public void SetProjectileCount(int count)
    {
        if(count> _maxProjectiles)
            projectileCount = _maxProjectiles;
        else if (count<1)
            projectileCount = 1;
        else
            projectileCount =count;
    }

}
