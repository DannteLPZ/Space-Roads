using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    private GameObject playerController;
    private int upperBound = 6;
    private float pointsScore = 5.0f;
    //GameObject.Find("Capsule").GetComponent<MoveUp>().speed = 500;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Triangle");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > upperBound)
        {
            Destroy(gameObject);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       Destroy(gameObject);
       Destroy(collision.gameObject);
       playerController.GetComponent<PlayerController>().score += pointsScore;
    }
}
