using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerHelper : MonoBehaviour
{
    public void LoadGameScene(int buildIndex)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LoadGameScene(buildIndex);
    }

    public void IncreaseLevel()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.IncreaseLevel();
    }
}
