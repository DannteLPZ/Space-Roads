using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Scene Loading")]
    [SerializeField] private Animator _loadingScreenAnimator;

    [Header("Eventos")]
    [SerializeField] private GameEvent _onScoreUpdated;
    [SerializeField] private GameEvent _onLevelIncreased;

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

    public void IncreaseLevel()
    {
        _currentLevel++;
        _onLevelIncreased.Invoke();
    }

    public void LoadGameScene(int buildIndex)
    {
        _currentLevel = 0;
        _score = 0;
        _loadingScreenAnimator.SetBool("Show", true);
        Slider progressSlider = _loadingScreenAnimator.GetComponentInChildren<Slider>();
        progressSlider.value = 0.0f;
        StartCoroutine(LoadingBar(progressSlider, buildIndex));
    }

    private IEnumerator LoadingBar(Slider slider, int buildIndex)
    {
        yield return new WaitForSeconds(0.5f);
        slider.value += Random.Range(0.1f, 0.4f);
        if(slider.value < 0.9f) StartCoroutine(LoadingBar(slider, buildIndex));
        else
        {
            SceneManager.LoadScene(buildIndex);
            _loadingScreenAnimator.SetBool("Show", false);
        }
    }

    public void AddScore(int score)
    {
        _score += score;
        _onScoreUpdated.Invoke();
    }
}
