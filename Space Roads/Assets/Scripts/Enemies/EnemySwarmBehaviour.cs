using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class EnemySwarmBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList = new();

    [SerializeField] private int difficultyIndex; //Auxiliar variable for testing
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private float tileSize;
    [SerializeField] private float limitMovementY;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Vector2 swarmSpeed;

    [Header("Game Events")]
    [SerializeField] private GameEvent _onMissionComplete;

    private Vector2 spawnPoint;
    private Vector2 limitPoint;

    [HideInInspector]
    public int enemyCount;

    private void Start()
    {
        speedMultiplier = 0.0f;
        Vector2 screenSize = new(Screen.width, Screen.height);
        limitPoint = Camera.main.ScreenToWorldPoint(screenSize);
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

        if (transform.position.y - (tileSize * (rows - 0.5f)) <= -limitMovementY)
        {
            if (swarmSpeed.y < 0) swarmSpeed.y *= -1.0f;

            transform.position = new Vector2(transform.position.x, -limitMovementY + (tileSize * (rows - 0.5f)));
        }

        else if(transform.position.y >= (limitPoint.y - tileSize / 2.0f) - limitMovementY)
        {
            if (swarmSpeed.y > 0) swarmSpeed.y *= -1.0f;

            transform.position = new Vector2(transform.position.x, (limitPoint.y - tileSize / 2.0f) - limitMovementY);
        }

        transform.Translate((swarmSpeed * speedMultiplier) * Time.deltaTime);

    }

    public void GenerateRandomSwarm()
    {
        int randomColumns  = Random.Range(8, 13);
        int randomRows = Random.Range(2, 4);
        GenerateSwarm(randomColumns, randomRows);
    }

    public void GenerateSwarm(int columnsNumber, int rowsNumber)
    {
        columns = columnsNumber; rows = rowsNumber;
        enemyCount = 0;
        int levelDifficulty = GameManager.Instance.CurrentLevel + 1;

        if (columns % 2 == 0) columns--;

        for (int row = 0; row < rows; row++) 
        {
            for (int col = 0; col < columns; col++)
            {

                if((row % 2 == 0 && col % 2 == 0) || (row % 2 != 0 && col % 2 != 0))
                {
                    int randomEnemyType = Random.Range(0, levelDifficulty);
                    GameObject enemy = Instantiate(enemyList[randomEnemyType], transform);

                    enemyCount++;

                    float posX = col * tileSize;
                    float posY = row * - tileSize;

                    enemy.transform.localPosition = new Vector2(posX, posY);
                }
            }
        }

        float initialX = tileSize * ((1 - columns) / 2.0f); 
        float initialY = limitPoint.y - tileSize;
        spawnPoint = new Vector2(initialX, initialY);

        transform.position = spawnPoint;

        StartCoroutine(InitializeMovement());
    }

    public void GenerateBoss() //Instantiate boss and start boss movement
    { 
        enemyCount = 0;
        tileSize = enemyList[^1].transform.localScale.x;
        rows = 1;
        columns = 1;

        GameObject enemy = Instantiate(enemyList[^1], transform);
        enemy.transform.localPosition = Vector3.zero;
        enemyCount++;

        transform.position = Vector3.zero;

        StartCoroutine(InitializeMovement());
    }

    public void ReduceEnemyCount()
    {
        enemyCount--;

        if(enemyCount <= 0)
        {
            speedMultiplier = 0.0f;
            _onMissionComplete.Invoke();
        }
    }

    private IEnumerator InitializeMovement()
    {
        yield return new WaitForSeconds(1.0f);
        SetSwarmSpeed();
    }
    private void SetSwarmSpeed()
    {
        int levelDifficulty = GameManager.Instance.CurrentLevel + 1;
        speedMultiplier = (0.5f * levelDifficulty) + 0.5f;
        if (levelDifficulty == 4) speedMultiplier *= 3.0f;
    }
}
