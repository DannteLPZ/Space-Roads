using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float inputVertical;
    private float horizotalVertical;
    public GameObject projectilePrefab;
    public Vector2 offsetProjectile;
    private int live = 5;
    private bool gameOver;
    public float score = 0.0f;
    private int multScore = 5;


    public bool powerUp; 
     
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
        //100 && score%9==0
        if (score > multScore && score%multScore == 0)
        {
            powerUp = true;
            StartCoroutine(PowerupCountdownRoutine(3));            
        }

    }

    void FirePlayer()
    {
        offsetProjectile = new Vector2(0, 2);
        if (Input.GetMouseButtonDown(0) && !powerUp)
        {
            
            Vector2 originProjectile= new Vector2(transform.position.x, transform.position.y) + offsetProjectile;
            projectilePrefab.GetComponent<MoveUp>().speed = 5.0f;
            Instantiate(projectilePrefab, originProjectile, projectilePrefab.transform.rotation);

        }
        else if (Input.GetMouseButtonDown(0) && powerUp)
        {
            Vector2 originProjectile = new Vector2(transform.position.x, transform.position.y) + offsetProjectile;
            projectilePrefab.GetComponent<MoveUp>().speed = 10.0f;
            Instantiate(projectilePrefab, originProjectile, Quaternion.Euler(0, 0, -45));
            Instantiate(projectilePrefab, originProjectile, Quaternion.Euler(0, 0, 45));
        }
        else if (Input.GetKeyDown(KeyCode.Space) && powerUp)
        {
            Vector2 originProjectile = new Vector2(transform.position.x, transform.position.y) + offsetProjectile;
            projectilePrefab.GetComponent<MoveUp>().speed = 15.0f;
            Instantiate(projectilePrefab, originProjectile, projectilePrefab.transform.rotation);
            Instantiate(projectilePrefab, originProjectile, Quaternion.Euler(0, 0, -15));
            Instantiate(projectilePrefab, originProjectile, Quaternion.Euler(0, 0, 15));
        }
    }

    void MovePlayer()
    {
        horizotalVertical = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        transform.Translate(Vector2.up * 5 * inputVertical * Time.deltaTime);
        transform.Translate(Vector2.right * 5 * horizotalVertical * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (live > 0)
        {
            live -= 1;
            Debug.Log("Lives: " + live);
        }else if(live == 0)
        {
            gameOver = true;
            Debug.Log("GAMEOVER");
            Debug.Log("Lives: " + live);
        }

        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
        }

    }

    IEnumerator PowerupCountdownRoutine(int timePowerUp)
    {        
        yield return new WaitForSeconds(timePowerUp);
        powerUp = false;
    }
}
