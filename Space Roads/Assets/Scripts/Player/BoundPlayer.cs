using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundPlayer : MonoBehaviour
{
    private int lowBound = 4;
    private int upperBound = 0;
    private int leftBound = -13;
    private int rightBound = 13;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -lowBound)
        {            
            transform.position = new Vector2(transform.position.x, -lowBound);
        }
        if(transform.position.y >= -upperBound)
        {
            transform.position = new Vector2(transform.position.x, upperBound);
        }
        if (transform.position.x < leftBound)
        {
            transform.position = new Vector2(12, transform.position.y);
            Debug.Log("Menor que: " + leftBound);
        }
        if (transform.position.x > rightBound)
        {
         transform.position = new Vector2(-13, transform.position.y);
            Debug.Log("Mayor que: " + rightBound);
        }


    }
}
