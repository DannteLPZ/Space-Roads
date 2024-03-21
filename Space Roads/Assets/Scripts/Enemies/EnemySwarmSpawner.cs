using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class EnemySwarmSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private float tileSize;
    [SerializeField] private float limitMovementY;
    [SerializeField] private Vector2 swarmSpeed;

    private Vector2 spawnPoint;
    private Vector2 limitPoint;

    private float speedMultiplier;

    private void Start()
    {
        speedMultiplier = 0.0f;
        GenerateSwarm();
        Invoke(nameof(InitializeMovement), 2.0f);
    }

    private void Update()
    {
        if (transform.position.x <= ((tileSize / 2.0f) - limitPoint.x))
        {
            if(swarmSpeed.x < 0) swarmSpeed.x *= -1.0f;

            transform.position = new Vector2(((tileSize / 2.0f) - limitPoint.x), transform.position.y);
        }

        else if (transform.position.x >= (limitPoint.x - tileSize * (columns - 0.5f)))
        { 
            if(swarmSpeed.x > 0) swarmSpeed.x *= -1.0f;

            transform.position = new Vector2((limitPoint.x - tileSize * (columns - 0.5f)), transform.position.y);
        }

        if (transform.position.y <= (tileSize * rows - 0.5f))
        {
            if (swarmSpeed.y < 0) swarmSpeed.y *= -1.0f;

            transform.position = new Vector2(transform.position.x, (tileSize * rows - 0.5f));
        }

        else if(transform.position.y >= (limitPoint.y - tileSize / 2.0f))
        {
            if (swarmSpeed.y > 0) swarmSpeed.y *= -1.0f;

            transform.position = new Vector2(transform.position.x, (limitPoint.y - tileSize / 2.0f));
        }

        transform.Translate(swarmSpeed * speedMultiplier * Time.deltaTime);

    }

    private void InitializeMovement() => speedMultiplier = 1.0f;

    private void GenerateSwarm()
    {
        if(columns % 2 == 0) columns--;

        for (int row = 0; row < rows; row++) 
        {
            for (int col = 0; col < columns; col++)
            {

                if((row % 2 == 0 && col % 2 == 0) || (row % 2 != 0 && col % 2 != 0))
                {
                    //numero aleatorio 
                    GameObject enemy = Instantiate(enemyPrefab, transform);

                    float posX = col * tileSize;
                    float posY = row * -tileSize;

                    enemy.transform.localPosition = new Vector2(posX, posY);
                }
            }
        }

        Vector2 screenSize = new(Screen.width, Screen.height);
        
        limitPoint = Camera.main.ScreenToWorldPoint(screenSize);

        float initialX = tileSize * ((1 - columns) / 2.0f); 
        float initialY = limitPoint.y - tileSize;
        spawnPoint = new Vector2(initialX, initialY);

        transform.position = spawnPoint;

    }

}
