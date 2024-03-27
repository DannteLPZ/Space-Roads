using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiCanvasGame : MonoBehaviour
{

    [Header("Menu")]
    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private CanvasGroup optionsMenu;
    [SerializeField] private CanvasGroup gameOverMenu;
    [SerializeField] private CanvasGroup winMissionMenu;
    [SerializeField] private CanvasGroup winMenu;

    [Header("Score Texts")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalText;

    [Header("Life Slider")]
    [SerializeField] private Slider sliderLife;

    [Header("Point Images")]
    [SerializeField] private Image maxPoints;
    [SerializeField] private Image midPoints;
    [SerializeField] private Image minPoints;

    [Header("Events")]
    [SerializeField] private GameEvent _onMissionContinue;

    private bool gamePaused = false;

    private GameObject player;


    private void Start() 
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        UIUpdateHealth();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
                Resume();
            else
                Pause();
        }
    }


    public void WinMissionMenu(){
        ShowCanvasGroup(winMissionMenu);
    }

    public void WinMenu(){
        int finalScore = GameManager.Instance.Score;
        if (finalScore < 250)
            minPoints.enabled = true;
        if (finalScore >= 250 && finalScore < 750)
            midPoints.enabled = true;
        if (finalScore >= 750)
            maxPoints.enabled = true;
        finalText.SetText(finalScore.ToString());
        ShowCanvasGroup(winMenu);
    }

    public void UIUpdateHealth()
    {
        if(player!=null){
            player.TryGetComponent(out IHealth playerHealth);
            if (playerHealth != null)
                sliderLife.value = (float)playerHealth.CurrentHealth / playerHealth.MaxHealth;
        }
    }

    public void GameOver()
    {
        ShowCanvasGroup(gameOverMenu);
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + GameManager.Instance.Score;
        finalText.text = "Score: " + GameManager.Instance.Score + " /1000";
    }

    public void Pause()
    {
        gamePaused = true;
        Time.timeScale = 0f;
        ShowCanvasGroup(pauseMenu);
    }

    public void Resume()
    {
        HideCanvasGroup(pauseMenu);
        HideCanvasGroup(optionsMenu);
        gamePaused = false;
        Time.timeScale = 1f;
    }

    public void ShowCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void HideCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public void MissionContinue() => _onMissionContinue.Invoke();

}
