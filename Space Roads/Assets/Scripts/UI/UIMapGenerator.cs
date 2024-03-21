using System.Collections.Generic;
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

    private void Start()
    {
        ObtainDimensions();
        GenerateMap();
    }

    private void GenerateMap()
    {
        //Get available dimensions
        float realWidth = _mapWidth - (_margins.x * 2.0f);
        float realHeight = _mapHeight - (_margins.x * 2.0f);

        //Define starting node which is always the same
        float deltaX = (Screen.width - _mapWidth)/ 2.0f;
        float posX = ((_mapWidth - GetRectTransform(_startingIcon).sizeDelta.x) / 2.0f) + deltaX; 
        float posY = _margins.y + (realHeight / (_levels * 2.0f));

        //Create Initial Node
        CreateNode(_startingIcon, posX, posY);

        _currentLevelIcons.AddRange(_nextLevelIcons);
        _nextLevelIcons.Clear();

        //Define in-between levels
        for (int a = 0; a < _levels - 2; a++)
        {
            //Define level structure;
            for (int i = 0; i < _currentLevelIcons.Count; i++)
            {
                RectTransform currentRect = GetRectTransform(_currentLevelIcons[i]); //Reference current rect for positioning
                int randomNodeIndex = Random.Range(1, _maxBranches + 1); //Max 3 branches per node
                float offset = realWidth / (_currentLevelIcons.Count * randomNodeIndex);

                //Create branches on current node
                for (int j = 0; j < randomNodeIndex; j++)
                {
                    int randomIconIndex = Random.Range(0, _levelIcons.Count); //Select random icon
                    posX = currentRect.anchoredPosition.x + (((1 - randomNodeIndex) * (offset / 2.0f)) + (offset * j));
                    posY = currentRect.anchoredPosition.y + (realHeight / _levels);

                    //Create Node & Draw Line
                    CreateNode(_levelIcons[randomIconIndex], posX, posY);
                    LineRenderer lineRenderer = Instantiate(_lineObject, _lineCanvas.transform).GetComponent<LineRenderer>();
                    Vector2 lineOffset = GetRectTransform(_levelIcons[randomIconIndex]).sizeDelta / 2.0f;
                    lineRenderer.SetPosition(0, new Vector2(posX, posY) + lineOffset);
                    lineRenderer.SetPosition(1, currentRect.anchoredPosition + lineOffset);
                    lineRenderer.enabled = true;
                }
            }
            _currentLevelIcons.Clear();
            _currentLevelIcons.AddRange(_nextLevelIcons);
            _nextLevelIcons.Clear();
        }

        //Define final node which is always the same
        posX = ((_mapWidth - GetRectTransform(_finalIcon).sizeDelta.x) / 2.0f) + deltaX;
        posY = realHeight - _margins.y;
        GameObject finalNode = CreateNode(_finalIcon, posX, posY);
        for (int i = 0; i < _currentLevelIcons.Count; i++)
        {
            LineRenderer finalRenderer = Instantiate(_lineObject, _lineCanvas.transform).GetComponent<LineRenderer>();
            Vector2 lineOffset = GetRectTransform(_currentLevelIcons[i]).sizeDelta / 2.0f;
            finalRenderer.SetPosition(0, GetRectTransform(finalNode).anchoredPosition + lineOffset);
            finalRenderer.SetPosition(1, GetRectTransform(_currentLevelIcons[i]).anchoredPosition + lineOffset);
            finalRenderer.enabled = true;
        }     
        _currentLevelIcons.Clear();
        _nextLevelIcons.Clear();
    }

    private GameObject CreateNode(GameObject icon, float posX, float posY)
    {
        GameObject node = Instantiate(icon, _iconCanvas.transform);
        _nextLevelIcons.Add(node);
        RectTransform rectTransform = GetRectTransform(node);
        rectTransform.anchorMax = Vector2.zero;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.pivot = Vector2.zero;
        rectTransform.anchoredPosition = new Vector2(posX, posY);
        return node;
    }

    private RectTransform GetRectTransform(GameObject icon) => icon.GetComponent<RectTransform>();

    private void ObtainDimensions()
    {
        //Get Map width and height
        _mapRect = GetRectTransform(_mapObject);
        _mapWidth = _mapRect.rect.width;
        _mapHeight = _mapRect.rect.height;
    }
}
