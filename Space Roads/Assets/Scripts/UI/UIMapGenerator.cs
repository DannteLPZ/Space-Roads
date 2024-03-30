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
    [SerializeField] private Canvas _mapCanvas;
    [SerializeField] private GameObject _mapObject;
    [SerializeField] private GameObject _iconCanvas;
    [SerializeField] private GameObject _lineCanvas;

    [Header("Player Icon")]
    [SerializeField] private UIMapPlayerIcon _mapPlayerIcon;

    private RectTransform _mapRect;
    private Vector2 _mapSize;
    private Vector2 _camSize;
    private float _mapScale;

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
        ActivatePaths();
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
        float realWidth = (_mapSize.x - _margins.x) * 2.0f;
        #endregion

        float deltaY = ((_camSize.y - _mapSize.y) / 2.0f);

        //Define final node which is always the same
        #region Final Node
        float  posX = 0.0f;
        float  posY = _camSize.y - deltaY - _margins.y;
        GameObject finalNode = CreateNode(_finalIcon, new(posX, posY), null);
        UIMapIcon finalIcon = finalNode.GetComponent<UIMapIcon>();
        _allMapIcons[_levels - 1].Add(finalIcon);
        _nextLevelIcons.Clear();
        #endregion

        //Define starting node which is always the same
        #region Initial Node      
        posX = 0.0f;
        posY = -_camSize.y + deltaY + _margins.y;

        //Create Initial Node
        GameObject initialNode = CreateNode(_startingIcon, new(posX, posY), null);
        _mapPlayerIcon.SetIconPosition((Vector2)initialNode.transform.position);
        UIMapIcon initialIcon = initialNode.GetComponent<UIMapIcon>();
        initialIcon.ToggleBehaviour(true);
        _allMapIcons[0].Add(initialIcon);
        SelectedIcon = initialNode;

        //Assign next level to current level to re iterate
        _currentLevelIcons.AddRange(_nextLevelIcons);
        _nextLevelIcons.Clear();
        #endregion

        //Define in-between levels which are random
        #region In-between levels
        deltaY = (_allMapIcons[_levels - 1][0].gameObject.transform.position.y -
                _allMapIcons[0][0].gameObject.transform.position.y) / (_levels - 1);

        for (int a = 0; a < _levels - 2; a++)
        {
            //Define level structure;
            for (int i = 0; i < _currentLevelIcons.Count; i++)
            {
                GameObject currentNode = _currentLevelIcons[i];
                int randomNodeIndex = Random.Range(1, _maxBranches + 1); //Max 2 branches per node
                float offset = (realWidth / (_currentLevelIcons.Count * randomNodeIndex)) / 1.0f;

                //Create branches on current node
                for (int j = 0; j < randomNodeIndex; j++)
                {
                    int randomIconIndex = Random.Range(0, _levelIcons.Count); //Select random icon
                    posX = currentNode.transform.position.x + (2.0f * offset * j) - (offset * (randomNodeIndex - 1));
                    posY = currentNode.transform.position.y + deltaY;

                    //Create Node & Draw Line
                    GameObject node = CreateNode(_levelIcons[randomIconIndex], new(posX, posY), currentNode);
                    UIMapIcon icon = node.GetComponent<UIMapIcon>();
                    _allMapIcons[a + 1].Add(icon);

                    UIMapLine line = CreateLine(new(posX, posY), currentNode.transform.position, node);
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
        #region Final Lines
        for (int i = 0; i < _currentLevelIcons.Count; i++)
        {
            UIMapLine line = CreateLine(finalNode.transform.position,
                _currentLevelIcons[i].gameObject.transform.position, _currentLevelIcons[i]);
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
        rectTransform.pivot = 0.5f * Vector2.one;
        node.transform.position = position;
        return node;
    }

    private UIMapLine CreateLine(Vector2 initialPoint, Vector2 finalPoint, GameObject parentIcon)
    {
        //Instantiate object and get the UIMapLine component to assign to list
        GameObject lineObject = Instantiate(_lineObject, _lineCanvas.transform);
        UIMapLine uIMapLine = lineObject.GetComponent<UIMapLine>();
        uIMapLine.ParentIcon = parentIcon;
        LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, initialPoint);
        lineRenderer.SetPosition(1, finalPoint);
        lineRenderer.enabled = true;

        return uIMapLine;
    }
    #endregion
    
    #region Path activation
    public void ActivatePaths()
    {
        int level = GameManager.Instance.CurrentLevel;
        //If the selected level is the one before the last assign parents to activate corresponding icon and line
        if (level == _levels - 2)
        {
            _allMapIcons.Last()[0].ParentIcon = SelectedIcon;
            _allMapLines[level + 1].First(p => p.ParentIcon == SelectedIcon).ParentIcon = _allMapIcons.Last()[0].gameObject;
        }
        else if (level == _levels - 1) return;

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
        _mapScale =_mapCanvas.scaleFactor;
        _camSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        _mapSize = Camera.main.ScreenToWorldPoint(new Vector3(_mapRect.sizeDelta.x, _mapRect.sizeDelta.y) * _mapScale);
    }
    #endregion

    public void SelectNewIcon(GameObject icon)
    {
        SelectedIcon = icon;
        _mapPlayerIcon.TravelToPoint((Vector2)SelectedIcon.transform.position);
    }

    public void ResolveNewIcon() => SelectedIcon.GetComponent<UIMapIcon>().InvokeEvent();
}
