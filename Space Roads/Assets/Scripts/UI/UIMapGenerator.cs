using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIMapGenerator : MonoBehaviour
{
    [Header("Map Generation")]
    [Min(3)][SerializeField] private int _levels;
    [Min(1)][SerializeField] private int _maxBranches;
    [SerializeField] private GameObject _startingIcon;
    [SerializeField] private List<GameObject> _levelIcons;
    [SerializeField] private GameObject _finalIcon;
    [SerializeField] private Vector2 _margins;

    [SerializeField] private GameObject _lineObject;

    [Header("Canvas Sorting")]
    [SerializeField] private GameObject _mapObject;
    [SerializeField] private GameObject _iconCanvas;
    [SerializeField] private GameObject _lineCanvas;

    private RectTransform _mapRect;
    private float _mapWidth;
    private float _mapHeight;

    private List<GameObject> _currentLevelIcons = new();
    private List<GameObject> _nextLevelIcons = new();
    private List<List<UIMapIcon>> _allMapIcons = new();
    private List<List<UIMapLine>> _allMapLines = new();

    [HideInInspector]
    public GameObject SelectedIcon;

    private void Awake()
    {
        ObtainDimensions();
        GenerateMap();
    }

    private void Start()
    {
        ActivatePaths(0);
    }
    #region Map Generation
    private void GenerateMap()
    {
        //Setup lists and dimension
        #region Initial Setup
        //Populate lists of icons and lines with lists per level
        for (int i = 0; i < _levels; i++)
        {
            _allMapIcons.Add(new List<UIMapIcon>());
            _allMapLines.Add(new List<UIMapLine>());
        }  

        //Get available dimensions
        float realWidth = _mapWidth - (_margins.x * 2.0f);
        float realHeight = _mapHeight - (_margins.x * 2.0f);
        #endregion

        //Define starting node which is always the same
        #region Initial Node
        float deltaX = (Screen.width - _mapWidth)/ 2.0f;
        float posX = ((_mapWidth - GetRectTransform(_startingIcon).sizeDelta.x) / 2.0f) + deltaX; 
        float posY = _margins.y + (realHeight / (_levels * 2.0f));

        //Create Initial Node
        GameObject initialNode = CreateNode(_startingIcon, new(posX, posY), null);
        initialNode.GetComponent<UIMapIcon>().ToggleBehaviour(true);
        _allMapIcons[0].Add(initialNode.GetComponent<UIMapIcon>());
        SelectedIcon = initialNode;

        //Assign next level to current level to re iterate
        _currentLevelIcons.AddRange(_nextLevelIcons);
        _nextLevelIcons.Clear();
        #endregion

        //Define in-between levels which are random
        #region In-between levels
        for (int a = 0; a < _levels - 2; a++)
        {
            //Define level structure;
            for (int i = 0; i < _currentLevelIcons.Count; i++)
            {
                GameObject currentNode = _currentLevelIcons[i];
                RectTransform currentRect = GetRectTransform(currentNode); //Reference current rect for positioning
                int randomNodeIndex = Random.Range(1, _maxBranches + 1); //Max 3 branches per node
                float offset = realWidth / (_currentLevelIcons.Count * randomNodeIndex);

                //Create branches on current node
                for (int j = 0; j < randomNodeIndex; j++)
                {
                    int randomIconIndex = Random.Range(0, _levelIcons.Count); //Select random icon
                    posX = currentRect.anchoredPosition.x + (((1 - randomNodeIndex) * (offset / 2.0f)) + (offset * j));
                    posY = currentRect.anchoredPosition.y + (realHeight / _levels);

                    //Create Node & Draw Line
                    GameObject node = CreateNode(_levelIcons[randomIconIndex], new(posX, posY), currentNode);
                    UIMapIcon icon = node.GetComponent<UIMapIcon>();
                    _allMapIcons[a + 1].Add(icon);
                    UIMapLine line = CreateLine(new(posX, posY), currentRect.anchoredPosition,_levelIcons[randomIconIndex], node);
                    _allMapLines[a + 1].Add(line);
                }
            }
            //Assign next level to current level to re iterate
            _currentLevelIcons.Clear();
            _currentLevelIcons.AddRange(_nextLevelIcons);
            _nextLevelIcons.Clear();
        }
        #endregion

        //Define final node which is always the same
        #region Final Node
        posX = ((_mapWidth - GetRectTransform(_finalIcon).sizeDelta.x) / 2.0f) + deltaX;
        posY = realHeight - _margins.y;
        GameObject finalNode = CreateNode(_finalIcon, new(posX,posY), null);
        UIMapIcon finalIcon = finalNode.GetComponent<UIMapIcon>();
        _allMapIcons[_levels - 1].Add(finalIcon);

        for (int i = 0; i < _currentLevelIcons.Count; i++)
        {
            UIMapLine line = CreateLine(GetRectTransform(finalNode).anchoredPosition, 
                GetRectTransform(_currentLevelIcons[i]).anchoredPosition, finalNode, _currentLevelIcons[i]);
            _allMapLines[_allMapLines.Count - 1].Add(line);
        }     
        _currentLevelIcons.Clear();
        _nextLevelIcons.Clear();
        #endregion
    }
    #endregion

    #region Node and Line Creation
    private GameObject CreateNode(GameObject icon, Vector2 position, GameObject parentIcon)
    {
        //Instantiate object and get the UIMapIcon component to assign to list
        GameObject node = Instantiate(icon, _iconCanvas.transform);
        UIMapIcon uiIcon = node.GetComponent<UIMapIcon>();
        uiIcon.ParentIcon = parentIcon;
        uiIcon.Generator = this;
        _nextLevelIcons.Add(node);

        //Position according to vectors given
        RectTransform rectTransform = GetRectTransform(node);
        rectTransform.anchorMax = Vector2.zero;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.pivot = Vector2.zero;
        rectTransform.anchoredPosition = position;
        return node;
    }

    private UIMapLine CreateLine(Vector2 initialPoint, Vector2 finalPoint, GameObject currentIcon, GameObject parentIcon)
    {
        //Instantiate object and get the UIMapLine component to assign to list
        GameObject lineObject = Instantiate(_lineObject, _lineCanvas.transform);
        UIMapLine uIMapLine = lineObject.GetComponent<UIMapLine>();
        uIMapLine.ParentIcon = parentIcon;
        LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();

        //Position according to vectors given
        Vector2 lineOffset = GetRectTransform(currentIcon).sizeDelta / 2.0f;
        lineRenderer.SetPosition(0, initialPoint + lineOffset);
        lineRenderer.SetPosition(1, finalPoint + lineOffset);
        lineRenderer.enabled = true;

        return uIMapLine;
    }
    #endregion
    
    #region Path activation
    public void ActivatePaths(int level)
    {
        //If the selected level is the one before the last assign parents to activate corresponding icon and line
        if (level == _levels - 2)
        {
            _allMapIcons.Last()[0].ParentIcon = SelectedIcon;
            _allMapLines[level + 1].First(p => p.ParentIcon == SelectedIcon).ParentIcon = _allMapIcons.Last()[0].gameObject;
        }

        //Deactivate non-selected icons
        UIMapIcon[] iconsToDeactivate = _allMapIcons[level].Where(p => p.gameObject != SelectedIcon).ToArray();
        foreach (UIMapIcon icon in iconsToDeactivate)
            icon.ToggleBehaviour(false);

        //Activate available icons
        UIMapIcon[] iconsToActivate = _allMapIcons[level + 1].Where(p => p.ParentIcon == SelectedIcon).ToArray();
        foreach (UIMapIcon icon in iconsToActivate)
            icon.ToggleBehaviour(true);

        //Deactivate non-selected lines
        UIMapLine[] linesToDeactivate = _allMapLines[level].Where(p => p.ParentIcon != SelectedIcon).ToArray();
        foreach (UIMapLine line in linesToDeactivate)
            line.ToggleBehaviour(false);

        //Activate available lines
        UIMapLine[] linesToActivate = _allMapLines[level + 1]
                                        .Where(p => p.ParentIcon.GetComponent<UIMapIcon>().ParentIcon == SelectedIcon)
                                        .ToArray();
        foreach (UIMapLine line in linesToActivate)
            line.ToggleBehaviour(true);
    }
    #endregion

    #region Misc functions
    private RectTransform GetRectTransform(GameObject icon) => icon.GetComponent<RectTransform>();

    private void ObtainDimensions()
    {
        //Get Map width and height
        _mapRect = GetRectTransform(_mapObject);
        _mapWidth = _mapRect.rect.width;
        _mapHeight = _mapRect.rect.height;
    }
    #endregion
}
