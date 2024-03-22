using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiItchMenu : MonoBehaviour
{
    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }


  /*  [SerializeField] Button danielTItchIoButton;
    [SerializeField] Button danielLItchIoButton;
    [SerializeField] Button emmanuelItchIoButton;
    [SerializeField] Button johanItchIoButton;
    [SerializeField] Button sofiaItchIoButton;
    private string danielTItchIoURL = "https://danieltolo1.itch.io/";
    private string danielLItchIoURL = "https://dannte9804.itch.io/";
    private string emmanuelItchIoURL = "https://emadrigalv.itch.io/";
    private string johanItchIoURL = "https://johan-fonseca-sanchez.itch.io/";
    private string sofiaItchIoURL = "https://sofi-cu.itch.io/";
  
    private void Start()
    {
        danielTItchIoButton.onClick.AddListener(OpenItchioDanielT);
        danielLItchIoButton.onClick.AddListener(OpenItchioDanielL);
        emmanuelItchIoButton.onClick.AddListener(OpenItchioEmmanuel);
        johanItchIoButton.onClick.AddListener(OpenItchioJohan);
        sofiaItchIoButton.onClick.AddListener(OpenItchioSofia);
    }

    private void OpenItchioDanielT()
    {
        Application.OpenURL(danielTItchIoURL);
    }

    private void OpenItchioDanielL()
    {
        Application.OpenURL(danielLItchIoURL);
    }

    private void OpenItchioEmmanuel()
    {
        Application.OpenURL(emmanuelItchIoURL);
    }

    private void OpenItchioJohan()
    {
        Application.OpenURL(johanItchIoURL);
    }

    private void OpenItchioSofia()
    {
        Application.OpenURL(sofiaItchIoURL);
    }*/
}
