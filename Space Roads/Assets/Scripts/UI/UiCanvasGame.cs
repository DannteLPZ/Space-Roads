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

    [Header("Point Images")]
    [SerializeField] private Image maxPoints;
    [SerializeField] private Image midPoints;
    [SerializeField] private Image minPoints;

    public static bool gamePaused = false;

    public static bool gameOver = false;

    //slide vida
    public Slider sliderLife;

    //valores para pruebas
    public int currentLife=10;
    public int maxLife =10;

    //score text
    public TMP_Text scoreText;
    public TMP_Text finalText;

    //valores para pruebas
    public int Score = 0;

    public bool winMission = false;
    public bool finalWin = false;


    private void Start() {
        //aqui cuando tengamos el player
        //sliderLife.maxValue = player.maxLife;

        //valores para pruebas
        sliderLife.maxValue = maxLife;

        UpdateSliderValue();
    }


    private void Update()
    {
        UpdateSliderValue();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if ( winMission == true){
            WinMissionMenu();
        }

        if (finalWin == true){
            WinMenu();
        }
    }


    public void WinMissionMenu(){

        gamePaused = true;
        Time.timeScale = 0f;
        ShowCanvasGroup(winMissionMenu);

        //Llamar mapa al continuar
    }

    public void WinMenu(){
        
        gamePaused = true;
        Time.timeScale = 0f;
        
        if (Score <250){
            minPoints.enabled = true;
        }
        if (Score >=250 && Score <750){
            midPoints.enabled = true;
        }
        if (Score >=750){
            maxPoints.enabled = true;
        }

        ShowCanvasGroup(winMenu);
    }

    private void UpdateSliderValue()
    {
        //aqui cuando tengamos el player
        //sliderLife.value = player.currentLife;


        //valores para pruebas
        sliderLife.value = currentLife;

        if(currentLife > 0){
            sliderLife.value = currentLife;
        }
        else{
            GameOver();
        }
    }

    public void GameOver(){
        
        gameOver=true;
        Time.timeScale = 0f;
        ShowCanvasGroup(gameOverMenu);
    }

    public void UpdateScoreText()
    {
        //aqui cuando tengamos el GameManager
        //scoreText.text = "Score: " + GameManager.instance.score; // Suponiendo que GameManager.instance.score es el puntaje del jugador

        //valores para pruebas
        scoreText.text = "Score: " + Score + " /1000";
        finalText.text = "Score: " + Score + " /1000";
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

}
