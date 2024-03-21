using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UiCanvasGame : MonoBehaviour
{
    //game over 
    [SerializeField] GameObject gameOverMenu;
    public static bool gameOver = false;

    //active options menu

    [SerializeField] GameObject optionsMenu;

    //pause _Menu
    [SerializeField] Button pauseButton;

    [SerializeField] GameObject pauseMenu;

    public static bool gamePaused = false;

    //slide vida
    public Slider sliderLife;

    //aqui cuando tengamos el player
    //public Player player;


    //valores para pruebas
    public int currentLife=10;
    public int maxLife =10;

    //score text
    public TextMeshProUGUI scoreText;

    //valores para pruebas
    public int Score =0;


    //win
    [SerializeField] GameObject winMissionMenu;
    [SerializeField] GameObject winMenu;

    public bool winMission = false;
    public bool finalWin = false;

    [SerializeField] GameObject maxPoints;
    [SerializeField] GameObject midPoints;
    [SerializeField] GameObject minPoints;

    public TextMeshProUGUI finalText;




    private void Start() {
        //aqui cuando tengamos el player
        //sliderLife.maxValue = player.maxLife;

        //valores para pruebas
        sliderLife.maxValue = maxLife;

        UpdateSliderValue();
    }


    private void Update()
    {
        UpdateScoreText();
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gamePaused = true;
        Time.timeScale = 0f;
        winMissionMenu.SetActive(true);
    }

    public void WinMenu(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gamePaused = true;
        Time.timeScale = 0f;
        winMenu.SetActive(true);

        if (Score <250){
            minPoints.SetActive(true);
        }
        if (Score >=250 && Score <750){
            midPoints.SetActive(true);
        }
        if (Score >=750){
            maxPoints.SetActive(true);
        }

        finalText.text = "Score: " + Score + " /1000";        
    }

    private void UpdateSliderValue()
    {
        //aqui cuando tengamos el player
        //sliderLife.value = player.currentLife;


        //valores para pruebas
        sliderLife.value = currentLife;

        if(currentLife>0){
            sliderLife.value = currentLife;
        }
        else{
            GameOver();
        }
    }

    public void GameOver(){
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameOver=true;
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }

    public void UpdateScoreText()
    {
        //aqui cuando tengamos el GameManager
        //scoreText.text = "Score: " + GameManager.instance.score; // Suponiendo que GameManager.instance.score es el puntaje del jugador

        //valores para pruebas
        scoreText.text = "Score: " + Score + " /1000";
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gamePaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void MenuOptions()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gamePaused = true;
        Time.timeScale = 0f;
        optionsMenu.SetActive(true);
    }
}
