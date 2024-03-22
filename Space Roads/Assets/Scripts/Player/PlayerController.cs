using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed;
    [SerializeField] private float limitY;
    [SerializeField] private float offsetBoundX;

    private float verticalInput;
    private float horizotalInput;
    private float boundX;

    private Vector2 boundsY;
    private Vector2 screenSize;
    private Vector2 limitPoint;

    private void Start()
    {
        screenSize = new(Screen.width, Screen.height);
        limitPoint = Camera.main.ScreenToWorldPoint(screenSize);

        boundX = limitPoint.x;
        boundsY = new Vector2(limitY, -limitPoint.y);
    }

    void Update()
    {
        Boundary();
        MovePlayer();
    }

    void MovePlayer()
    {
        horizotalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(horizotalInput, verticalInput).normalized;

        transform.Translate(speed * Time.deltaTime * direction);
    }

    private void Boundary()
    {
        //Limit position in Y axis
        if(transform.position.y > boundsY.x)
            transform.position = new Vector2(transform.position.x, boundsY.x);
        
        if(transform.position.y < boundsY.y)
            transform.position = new Vector2(transform.position.x, boundsY.y);


        if(transform.position.x > boundX + offsetBoundX || transform.position.x < - boundX - offsetBoundX)
            transform.position = new Vector2( - Mathf.Sign(transform.position.x)  * boundX, transform.position.y);
    }
}
