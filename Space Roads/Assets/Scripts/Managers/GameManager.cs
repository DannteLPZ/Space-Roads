using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Eventos")]
    [SerializeField] private GameEvent _updateScoreEvent;


    public static GameManager Instance;

    private int _currentLevel;
    public int CurrentLevel => _currentLevel;

    private int _score;
    public int Score => _score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _currentLevel = 0;
    }

    public void IncreaseLevel() => _currentLevel++;

    public void LoadGameScene(int buildIndex)
    {
        _currentLevel = 0;
        SceneManager.LoadScene(buildIndex);

    }

    public void AddScore(int score)
    {
        _score += score;

        _updateScoreEvent.Invoke();
    }
}
