using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerSinglePlayer : MonoBehaviour 
{
    #region Public Variables
    public float moveSpeed;
    public float slowSpeed;
    public float regularSpeed;
    public float powerUpSpeed;
    public float clampMax;
    //public GameObject miniShark;
    public GameObject pickUpFX;
    //public GameObject pickUpFX2;
    public float spDuration;
    //public bool hasSharkSeekPowerUp;
    
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
        //the problem here is that if I set the timer to <= 0, then the player will always stay at regular speed even through the whirlpool
        timer -=Time.deltaTime;
        if(timer <= 0f )
        {
            moveSpeed  = regularSpeed;
            timer = 5;       
        }
        //if (hasSharkSeekPowerUp == true && Input.GetKeyDown(KeyCode.E))
        //{
        //    Instantiate(miniShark, myRB.position, myRB.rotation);
        //}
    }

    void FixedUpdate()
    {
        float MoveHorizontal = Input.GetAxisRaw("Horizontal");
        float MoveVertical = Input.GetAxisRaw("Vertical");

        movementInput = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
        if (movementInput != Vector3.zero)
        {
            myRB.rotation = Quaternion.Slerp(myRB.rotation,Quaternion.LookRotation(movementInput), 0.15f);
            
            movementInput = Vector3.ClampMagnitude(movementInput, clampMax);
            myRB.AddForce (movementInput * moveSpeed, ForceMode.Impulse);
        }
           
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
        if (other.tag == "Whirlpool")
        {
            moveSpeed = slowSpeed;
        }
        //if(other.tag == "SharkSeekPowerUp")
        //{
        //    hasSharkSeekPowerUp = true;
        //    other.gameObject.SetActive(false);
        //    Instantiate(pickUpFX2, myRB.position, myRB.rotation);
        //}
    }
    
    void OnTriggerExit(Collider other)
    {
        moveSpeed = regularSpeed;
    }

}
