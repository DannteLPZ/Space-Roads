using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundPlayer : MonoBehaviour
{
    private int lowBound = 4;
    private int upperBound = 1;
    private int leftBound = -13;
    private int rightBound = 13;
    //private Rigidbody2D playerRb;

    
         

    // Start is called before the first frame update
    void Start()
    {
        //playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -lowBound)
        {            
            transform.position = new Vector2(transform.position.x, -lowBound);
        }
        if (transform.position.y >= -upperBound)
        {
            //playerRb.AddForce(Vector2.down * 1, ForceMode2D.Impulse);
            transform.position = new Vector2(transform.position.x, -upperBound);
        }
        if (transform.position.x < leftBound)
        {
            transform.position = new Vector2(12, transform.position.y);
            
        }
        if (transform.position.x > rightBound)
        {
         transform.position = new Vector2(-13, transform.position.y);
            
        }


    }

  
}
