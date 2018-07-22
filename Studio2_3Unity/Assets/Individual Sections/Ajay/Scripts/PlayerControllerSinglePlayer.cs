using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSinglePlayer : MonoBehaviour 
{
    #region Public Variables
    public float moveSpeed;
    public float slowSpeed;
    public float regularSpeed;
    public float powerUpSpeed;
    public float clampMax;
    public GameObject pickUpFX;
    public float spDuration;
    
    #endregion

    #region Private Variables
    private Rigidbody myRB;
    private Vector3 movementInput;
    [SerializeField]
    private float timer;
    
    #endregion

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        
    }


    void Update()
    {
        timer -=Time.deltaTime;
        if(timer == 0)
        {
            moveSpeed  = regularSpeed;
        }
       
    }

    void FixedUpdate()
    {
        float MoveHorizontal = Input.GetAxisRaw("Horizontal");
        float MoveVertical = Input.GetAxisRaw("Vertical");

        movementInput = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
        myRB.rotation = Quaternion.Slerp(myRB.rotation,Quaternion.LookRotation(movementInput), 0.15f);

        movementInput = Vector3.ClampMagnitude(movementInput, clampMax);
        myRB.AddForce (movementInput * moveSpeed);
        
        //transform.Translate (MovementInput * MoveSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "SpeedPowerUp")
        {
            moveSpeed = powerUpSpeed;
            other.gameObject.SetActive(false);
            Instantiate(pickUpFX, myRB.position, myRB.rotation);
            timer = spDuration;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Whirlpool")
        {
            moveSpeed = slowSpeed;
        }
    }

    void OnTriggerExit(Collider other)
    {
        moveSpeed = regularSpeed;
    }
}
