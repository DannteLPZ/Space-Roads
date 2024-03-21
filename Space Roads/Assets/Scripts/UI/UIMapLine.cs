using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapLine : MonoBehaviour
{
    [SerializeField] private Gradient _activatedGradient;
    [SerializeField] private Gradient _deactivatedGradient;

    private Material _material;
    private LineRenderer _lineRenderer;

    [HideInInspector]
    public GameObject ParentIcon;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _material = _lineRenderer.material;
    }

    public void ToggleBehaviour(bool active)
    {
        _lineRenderer.colorGradient = active ? _activatedGradient : _deactivatedGradient;
        _material.SetFloat("_Speed", active ? -0.5f : 0.0f);
    }
}
