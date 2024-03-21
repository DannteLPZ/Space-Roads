using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapManager : MonoBehaviour
{
    [SerializeField] private UIMapGenerator _generator;
    public void SelectIcon(GameObject selectedIcon)
    {
        _generator.SelectedIcon = selectedIcon;
        GameManager.Instance.IncreaseLevel();
        _generator.ActivatePaths(GameManager.Instance.CurrentLevel);
    }
}
