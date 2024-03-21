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

    //pause _Menu
    [SerializeField] Button pauseButton;

    [SerializeField] GameObject pauseMenu;

    public static bool gamePaused = false;

    //slide vida
    public Slider slider;

    //aqui cuando tengamos el player
    //public Player player;


    //valores para pruebas
    public int currentLife=10;
    public int maxLife =10;

    //score text
    public TextMeshProUGUI scoreText;

    //valores para pruebas
    public int Score =0;

    private void Start() {
        //aqui cuando tengamos el player
        //slider.maxValue = player.maxLife;

        //valores para pruebas
        slider.maxValue = maxLife;

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
    }

    private void UpdateSliderValue()
    {
        //aqui cuando tengamos el player
        //slider.value = player.currentLife;


        //valores para pruebas
        slider.value = currentLife;

        if(currentLife>0){
            slider.value = currentLife;
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
}
