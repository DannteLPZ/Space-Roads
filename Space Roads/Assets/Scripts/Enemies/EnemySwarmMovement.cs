using System.Collections;
using UnityEngine;

public class EnemySwarmMovement : MonoBehaviour
{

    [SerializeField] private float swarmSpeed;

    private Rigidbody2D swarmRb;

    private float swarmSpeedX;
    private float swarmSpeedY; 

    private void Start()
    {
        swarmRb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartMovingY());
        StartCoroutine(StartMovingX());
        Movement();
    }

    private void FixedUpdate()
    {
        swarmRb.velocity = new Vector2(swarmSpeedX, swarmSpeedY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WBoundary"))
        {
            swarmSpeedX *= -1;
        }
        if (collision.CompareTag("HBoundary"))
        {
            swarmSpeedY *= -1;
        }
    }

    private void Movement()
    {
        Vector2 screenSize = new(Screen.width, Screen.height);
        Vector2 limitPoint = Camera.main.ScreenToWorldPoint(screenSize);
        Debug.Log(limitPoint);
    }

    private IEnumerator StartMovingY()
    {
        yield return new WaitForSeconds(10);
        swarmSpeedY = swarmSpeed;
    }

    private IEnumerator StartMovingX()
    {
        yield return new WaitForSeconds(1.5f);
        swarmSpeedX = swarmSpeed;
    }
}
