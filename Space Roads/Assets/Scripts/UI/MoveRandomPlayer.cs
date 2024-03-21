using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomPlayer : MonoBehaviour
{
    [SerializeField] private float speedMove;
    [SerializeField] private Transform[] pointsMove;
    [SerializeField] private float distanceMin;

    private int numRandom;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        numRandom = Random.Range(0, pointsMove.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, pointsMove[numRandom].position, speedMove * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, 0.4f));

        if (Vector2.Distance(transform.position, pointsMove[numRandom].position) < distanceMin)
        {
            numRandom = Random.Range(0, pointsMove.Length);
            Girar();
        }
    }

    private void Girar()
    {
        if (transform.position.x < pointsMove[numRandom].position.x)
        {
            spriteRenderer.flipY = true;


        }
        else
        {
            spriteRenderer.flipY = false;
        }
    }
}
