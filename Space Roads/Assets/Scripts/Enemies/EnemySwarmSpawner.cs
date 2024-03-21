using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwarmSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private float tileSize;
    [SerializeField] private float swarmSpeed;

    private Rigidbody2D swarmRb;

    private float swarmSpeedX;
    private float swarmSpeedY;

    private void Start()
    {
        swarmRb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartMovingY());
        StartCoroutine(StartMovingX());
        GenerateSwarm();
    }

    private void GenerateSwarm()
    {
        for (int row = 0; row < rows; row++) 
        {
            for (int col = 0; col < columns; col++)
            {
                if((row % 2 == 0 && col % 2 == 0) || (row % 2 != 0 && col % 2 != 0 && col < columns - 1)) 
                {
                    GameObject enemy = Instantiate(enemyPrefab, transform);

                    float posX = col * tileSize;
                    float posY = row * -tileSize;

                    enemy.transform.position = new Vector2(posX, posY);
                }
            }
        }

        Vector2 screenSize = new(Screen.width, Screen.height);
        Vector2 limitPoint = Camera.main.ScreenToWorldPoint(screenSize);

        float initialX = tileSize * ((1 - columns) / 2.0f); 
        float initialY = limitPoint.y - tileSize;

        transform.position = new Vector2(initialX, initialY);

        float gridW = columns * tileSize;
        float gridH = rows * tileSize;

        if (columns % 2 == 0) { gridW = (columns - 1) * tileSize; }

        //swarmCollider.size = new Vector2(gridW, gridH);
        //swarmCollider.offset = new Vector2((swarmCollider.size.x/2 - tileSize/2), (- swarmCollider.size.y/2 + tileSize/2));
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
