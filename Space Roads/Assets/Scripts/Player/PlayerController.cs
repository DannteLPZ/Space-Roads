using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float inputVertical;
    private float horizotalVertical;
    public GameObject projectilePrefab;
    public bool powerUp = true;
    private GameObject capsule;
    private MoveUp oo;

    private GameObject capsule1;





    // Start is called before the first frame update
    void Start()
    {
        //capsule = GameObject.Find("Capsule");

        //velocifda = FindObjectsOfType<MoveUp>().ge
        //capsule = FindObjectsOfType<MoveUp>()
        //GameObject.Find("Capsule").GetComponent<MoveUp>().speed = 500;

        //oo = FindObjectsOfType<MoveUp>();

        

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        FirePlayer();
        ///oo.speed = 400;
     }

    void FirePlayer()
    {
        if (Input.GetMouseButtonDown(0) && !powerUp)
        {
            projectilePrefab.GetComponent<MoveUp>().speed = 5.0f;
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);

        }
        else if (Input.GetMouseButtonDown(0) && powerUp)
        {
            projectilePrefab.GetComponent<MoveUp>().speed = 10.0f;
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, -45));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 45));
        }
        else if (Input.GetKeyDown(KeyCode.Space) && powerUp)
        {
            projectilePrefab.GetComponent<MoveUp>().speed = 15.0f;
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, -15));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 15));
        }
    }

    void MovePlayer()
    {
        horizotalVertical = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        transform.Translate(Vector2.up * 5 * inputVertical * Time.deltaTime);
        transform.Translate(Vector2.right * 5 * horizotalVertical * Time.deltaTime);
    }
}
