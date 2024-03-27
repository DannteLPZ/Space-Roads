using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class EnemySwarmBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();

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

    private void Start() => speedMultiplier = 0.0f;

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

        if (transform.position.y <= (tileSize * (rows - 0.5f)))
        {
            if (swarmSpeed.y < 0) swarmSpeed.y *= -1.0f;

            transform.position = new Vector2(transform.position.x, (tileSize * (rows - 0.5f)));
        }

        else if(transform.position.y >= (limitPoint.y - tileSize / 2.0f))
        {
            if (swarmSpeed.y > 0) swarmSpeed.y *= -1.0f;

            transform.position = new Vector2(transform.position.x, (limitPoint.y - tileSize / 2.0f));
        }

        transform.Translate((swarmSpeed * speedMultiplier) * Time.deltaTime);

    }

    public void GenerateSwarm(int columnsNumber, int rowsNumber)
    {
        columns = columnsNumber; rows = rowsNumber;

        int levelDifficulty = GameManager.Instance.CurrentLevel + 1;

        enemyCount = 0;

        if(columns % 2 == 0) columns--;

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

        Vector2 screenSize = new(Screen.width, Screen.height);
        
        limitPoint = Camera.main.ScreenToWorldPoint(screenSize);

        float initialX = tileSize * ((1 - columns) / 2.0f); 
        float initialY = limitPoint.y - tileSize;
        spawnPoint = new Vector2(initialX, initialY);

        transform.position = spawnPoint;

        InitializeMovement(levelDifficulty);
    }

    public void GenerateBoss() //Instantiate boss and start boss movement
    {
        enemyCount = 0;
        tileSize = enemyList[enemyList.Count - 1].transform.localScale.x;
        rows = 1;
        columns = 1;

        GameObject enemy = Instantiate(enemyList[enemyList.Count - 1], transform);

        enemyCount++;

        float posX = columns * tileSize;
        float posY = rows * -tileSize;

        enemy.transform.localPosition = new Vector2(posX, posY);

        Vector2 screenSize = new(Screen.width, Screen.height);

        limitPoint = Camera.main.ScreenToWorldPoint(screenSize);

        float initialX = tileSize * ((1 - columns) / 2.0f);
        float initialY = limitPoint.y - tileSize;
        spawnPoint = new Vector2(initialX, initialY);

        transform.position = spawnPoint;

        InitializeMovement(3); //if levelDifficulty increase in boss create a new case for Boss speed multiplier.
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

    private void SetSwarmSpeed(int levelDifficulty)
    {
        switch(levelDifficulty)
        {
            case 1:
                speedMultiplier = 1.0f; break;

            case 2:
                speedMultiplier = 1.5f; break;

            case 3:
                speedMultiplier = 2.0f; break;
        }
    }

    private void InitializeMovement(int levelDifficulty)
    {
        StartCoroutine(StartMovementRoutine(levelDifficulty));
    }

    private IEnumerator StartMovementRoutine(int levelDifficulty)
    {
        yield return new WaitForSeconds(1.0f);
        SetSwarmSpeed(levelDifficulty);
    }
}
