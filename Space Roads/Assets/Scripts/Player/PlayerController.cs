using UnityEngine;

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

    private Vector2 initialPos;

    private void Start()
    {
        screenSize = new(Screen.width, Screen.height);
        limitPoint = Camera.main.ScreenToWorldPoint(screenSize);

        boundX = limitPoint.x;
        boundsY = new Vector2(limitY, -limitPoint.y);

        initialPos = transform.position;
    }

    void Update()
    {
        Boundary();
        MovePlayer();
    }

    void MovePlayer()
    {
        horizotalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

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


    public void ModifySpeed(float multiplier) => speed*=multiplier;
    
}
